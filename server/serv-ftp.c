#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <netinet/in.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <pthread.h>
#include <stdint.h>
#include <arpa/inet.h>

#define MAX_CONNECTIONS 5
#define FTP_PORT 8090
#define BUFFER_SIZE 5 * 1024
#define COMMAND_SIZE 4
#define MAX_RESPONSE_SIZE 1024
#define DATA_POSITION 1024 - 1
#define BASE_PATH "FTP_FILES"

typedef enum
{
    Mkdir = 1,
    Rmdir = 2,
    Put = 3,
    Get = 4,
    Exit = 7
} ControlCommands;

void *handleClient(void *arg);
char *handleDirCommand(ControlCommands command, const uint8_t *bytes);
char *handlePutCommand(const uint8_t *bytes, size_t total_size);
char *handleGetCommand(const uint8_t *bytes, uint8_t **fileData, size_t *fileDataSize);

// wywołanie:
//   gcc serv-ftp.c -o serv-ftp -lpthread -Wall
//  ./serv-pthread
//  lub
//  strace ./serv-pthread
int main()
{
    int server_fd, new_socket, on = 1;
    struct sockaddr_in address;
    int addrlen = sizeof(address);

    server_fd = socket(AF_INET, SOCK_STREAM, 0);

    address.sin_family = AF_INET;
    address.sin_addr.s_addr = INADDR_ANY; // dowolny adres ip ktory mamy skonfigurowany
    address.sin_port = htons(FTP_PORT);   //"host to network short"

    // trzeba uwazac na odpalanie serwera kilka razy - port moze byc zajety, bo zwalnia sie po dopiero okolo 4 minutach - dlatego przydaje sie ponizsza linia:
    setsockopt(server_fd, SOL_SOCKET, SO_REUSEADDR, (char *)&on, sizeof(on));

    bind(server_fd, (struct sockaddr *)&address, sizeof(address));

    listen(server_fd, MAX_CONNECTIONS);

    printf("FTP Server started on port %d\n", FTP_PORT);

    while (1)
    {
        if ((new_socket = accept(server_fd, (struct sockaddr *)&address, (socklen_t *)&addrlen)) < 0)
        {
            perror("accept");
            continue;
        }

        pthread_t thread_id;
        if (pthread_create(&thread_id, NULL, handleClient, (void *)&new_socket) != 0)
        {
            perror("pthread_create");
        }
    }

    return 0;
}

// pobiera dane o podanej wielkości bufora
int _read(int cfd, uint8_t *buf, int buf_size)
{
    int x = 0;
    while (x < buf_size)
    {
        int j = read(cfd, buf + x, buf_size - x);
        if (j == 0)
        {
            break;
        }
        x = x + j;
    }
    return x;
}

// pobiera plik
char *handleGetCommand(const uint8_t *bytes, uint8_t **fileData, size_t *fileDataSize)
{
    static char response[1024];

    // przygotowanie sciezki
    size_t path_length = DATA_POSITION - COMMAND_SIZE - 1;

    // wydzielenie podanej przez uzytkownika sciezki
    uint8_t location[path_length + 1]; // +1 - null-termination
    memcpy(location, bytes + COMMAND_SIZE, path_length);
    location[path_length] = '\0'; // Null-termination

    // ustawienie sciezki
    char path[1024];
    snprintf(path, sizeof(path), "%s/%s", BASE_PATH, location);

    // Sprobuj otworzyc plik o podanej sciezce
    FILE *file = fopen(path, "rb");
    if (file)
    {
        // Pobierz rozmiar pliku
        fseek(file, 0, SEEK_END);
        *fileDataSize = ftell(file);
        fseek(file, 0, SEEK_SET);

        // alokuj pamięć na dane z pliku
        *fileData = (uint8_t *)malloc(*fileDataSize);
        if (*fileData)
        {
            fread(*fileData, 1, *fileDataSize, file);
            snprintf(response, sizeof(response), "Success\n");
        }
        else
        {
            *fileDataSize = 0;
            snprintf(response, sizeof(response), "Memory allocation error");
        }
        fclose(file);
    }
    else
    {
        *fileData = NULL;
        *fileDataSize = 0;
        snprintf(response, sizeof(response), "Error opening file");
    }

    return response;
}

// zapisuje plik
char *handlePutCommand(const uint8_t *bytes, size_t total_size)
{
    static char response[1024];

    size_t path_length = DATA_POSITION - COMMAND_SIZE - 1;
    size_t data_length = total_size - DATA_POSITION;

    // wydziel sciezke
    uint8_t location[path_length + 1]; // +1 - null-termination
    memcpy(location, bytes + COMMAND_SIZE, path_length);
    location[path_length] = '\0'; // Null-termination

    // pobierz dane
    uint8_t *data = (uint8_t *)malloc(data_length);
    if (!data)
    {
        strcpy(response, "Memory allocation error");
        return response;
    }
    memcpy(data, bytes + DATA_POSITION, data_length);

    // przygotuj sciezke do zapisu
    char path[1024];
    snprintf(path, sizeof(path), "%s/%s", BASE_PATH, location);

    // zapisz plik
    FILE *file = fopen(path, "wb");
    if (file)
    {
        if (fwrite(data, 1, data_length, file) == data_length)
        {
            snprintf(response, sizeof(response), "Successfully saved file");
        }
        else
        {
            snprintf(response, sizeof(response), "Error writing to file");
        }
        fclose(file);
    }
    else
    {
        snprintf(response, sizeof(response), "Error opening file");
    }

    free(data); // zwolnij zaalokowana pamiec
    return response;
}

// dodaje/usuwa folder
char *handleDirCommand(ControlCommands command, const uint8_t *bytes)
{
    static char response[MAX_RESPONSE_SIZE];

    size_t data_length = DATA_POSITION - COMMAND_SIZE - 1;

    uint8_t location[data_length + 1];
    memcpy(location, bytes + COMMAND_SIZE, data_length);
    location[data_length] = '\0';

    // przygotuj sciezke
    char path[MAX_RESPONSE_SIZE];
    snprintf(path, sizeof(path), "%s/%s", BASE_PATH, location);

    // dodaj lub usun w zaleznosci od podanej komendy
    switch (command)
    {
    case Mkdir:
        if (mkdir(path, 0777) == 0)
        {
            snprintf(response, sizeof(response), "Successfully created directory %s", path + strlen(BASE_PATH));
        }
        else
        {
            perror("mkdir failed");
            snprintf(response, sizeof(response), "Error creating directory");
        }
        break;
    case Rmdir:
        if (rmdir(path) == 0)
        {
            snprintf(response, sizeof(response), "Successfully deleted directory %s", path + strlen(BASE_PATH));
        }
        else
        {
            perror("rmdir failed");
            snprintf(response, sizeof(response), "Error deleting directory");
        }
        break;
    default:
        strcpy(response, "Invalid command");
        break;
    }

    return response;
}

// command handler
char *handleCommand(const uint8_t *data, size_t data_size, uint8_t **response_data, size_t *response_data_size)
{
    static char response[MAX_RESPONSE_SIZE];
    memset(response, 0, MAX_RESPONSE_SIZE);

    if (data_size < COMMAND_SIZE)
    {
        strcpy(response, "Invalid command size");
        return response;
    }

    ControlCommands command = (ControlCommands) * (int32_t *)data;

    switch (command)
    {
    case Exit:
        strcpy(response, "Connection terminated");
        break;
    case Mkdir:
    case Rmdir:
        strcpy(response, handleDirCommand(command, data));
        break;
    case Put:
        strcpy(response, handlePutCommand(data, data_size));
        break;
    case Get:
        uint8_t *fileData = NULL;
        size_t fileDataSize = 0;
        char *get_response = handleGetCommand(data, &fileData, &fileDataSize);
        strcpy(response, get_response);
        *response_data = fileData;
        *response_data_size = fileDataSize;
        break;
    default:
        strcpy(response, "Unknown command");
        break;
    }

    return response;
}

void *handleClient(void *socket)
{
    int sock = *(int *)socket;
    uint8_t buffer[BUFFER_SIZE] = {0};
    ssize_t valread;
    printf("New connection: %d\n", sock);
    while ((valread = _read(sock, buffer, BUFFER_SIZE)) > 0)
    {
        uint8_t *response_data = NULL;
        size_t response_data_size = 0;
        char *response = handleCommand(buffer, (size_t)valread, &response_data, &response_data_size);

        ControlCommands command = (ControlCommands) * (int32_t *)buffer;
        if (command == Get)
        {
            // przygotuj odpowiedz
            uint8_t full_response[BUFFER_SIZE] = {0};
            if (response != NULL)
            {
                strncpy((char *)full_response, response, DATA_POSITION);
            }
            if (response_data != NULL && response_data_size > 0)
            {
                memcpy(full_response + DATA_POSITION, response_data, response_data_size);
                free(response_data); // zwolnij zaaloowana pamiec
                response_data = NULL;
                response_data_size = 0;
            }
            // wyslij response
            send(sock, full_response, BUFFER_SIZE, 0);
            // wyczysc bufer
            memset(buffer, 0, BUFFER_SIZE);
        }
        else
        {
            // wyslij response
            send(sock, response, strlen(response), 0);

            // wyczysc bufer
            memset(buffer, 0, BUFFER_SIZE);
        }
    }

    close(sock);
    return NULL;
}
