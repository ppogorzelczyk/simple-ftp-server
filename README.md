# FTP Server (C) oraz Klient (C#)

## Opis

Prosty serwer usługi FTP zgodny ze specyfikacją RFC 959 w zakresie komend: `ascii`, `binary`, `mkdir`, `rmdir`, `put` oraz `get`. Serwer obsługuje jednocześnie wielu klientów. Klient jest aplikacją Windows Forms, która łączy się z serwerem i pozwala na wykonywanie komend. Klient jest przystosowany do systemu Windows. Serwer jest przystosowany do systemu Linux.

### Opis protokołu komunikacyjnego

1. Klient łączy się z serwerem
2. Klient wysyła komendę do serwera
3. Serwer odbiera komendę i wykonuje ją
4. Serwer wysyła odpowiedź do klienta
5. Klient odbiera odpowiedź i wyświetla ją w terminalu
6. Klient wysyła kolejną komendę do serwera
7. Powtarzamy kroki 3-6 aż do komendy `exit`

Każda komenda jest wysyłana w buforze o rozmiarze 5*1024 bajtów.

Każda odpowiedź jest wysyłana w buforze o rozmiarze 5*1024 bajtów.

#### Foldery

- Server - Kod serwera napisany w C
- Klient - Aplikacja WinForms klienta napisana w C#

### Opis implementacji serwera

Serwer jest napisany w języku C. Do komunikacji z klientem wykorzystywane jest gniazdo TCP. Serwer obsługuje jednocześnie wielu klientów. Każdy klient jest obsługiwany w osobnym wątku. Serwer jest przystosowany do systemu Linux. W głównej funkcji serwera tworzony jest socket, który nasłuchuje na porcie 8090 (w przypadku korzystania z VirtualBox należy pamiętać o ustawieniu funkcji przekierowywania portów). Po nawiązaniu połączenia z klientem tworzony jest nowy wątek, który obsługuje klienta. Wątek klienta odbiera komendę, wykonuje ją i wysyła odpowiedź. Po wykonaniu komendy klient może wysłać kolejną komendę lub zakończyć połączenie. W przypadku zakończenia połączenia wątek klienta kończy działanie.

Kod klienta jest napisany w języku C#. Klient jest aplikacją Windows Forms. Implementacja klienta oparta jest o wzorzec projektowy [Command](https://refactoring.guru/design-patterns/command). Do komunikacji z serwerem użyta została paczka System.Net.Sockets.Socket.  



## Jak używać

Do uruchomienia serwera potrzebny jest kompilator GCC. Serwer jest przystosowany do systemu Linux.

By uruchomić serwer należy uruchomić komendę `gcc server-ftp.c -o server-ftp -lpthread -Wall` w folderze `Server`. Następnie należy uruchomić serwer komendą `./server-ftp`.

By uruchomić klienta należy uruchomić plik `FTP.ClientGUI.exe`. Jeśli z plikiem będzie problem, należy uruchomić projekt w Visual Studio i skompilować go.


### Dostępne komendy

1. mkdir - tworzy folder w wybranej lokalizacji
    ```bash
    mkdir -- folder/do/utworzenia
    ```
2. rmdir - usuwa folder z wybranej lokalizacji
    ```bash
    rmdir -- folder/do/usuniecia
    ```
3. put - zapisuje plik na serwerze
    ```bash
    put -- folder/nazwapliku.ext -- folder/do/zapisania/tumozebycinnanazwa.ext
    ```
4. ascii - ustawia tryb ascii
    ```bash
    ascii
    ```
5. binary - ustawia tryb binarny
    ```bash
    binary
    ```
6. get - pobiera plik z serwera
    - Jeśli ustawiony jest tryb binarny, plik zostanie zapisany na dysku
    - Jeśli ustawiony jest tryb ascii, plik zostanie wyświetlony w terminalu
    ```bash
    get -- sciezka/do/pliku.txt
    ```
7. exit - rozłącza z serwerem i zamyka aplikację
    ```bash
    exit
    ```


## Autor

Patryk Pogorzelczyk, Politechnika Poznańska, Wydział Informatyki, Informatyka
144362
-   [LinkedIn](https://www.linkedin.com/in/patryk-pogorzelczyk/)
