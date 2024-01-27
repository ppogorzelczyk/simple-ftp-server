using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FTP.Client
{
    public partial class Client
    {
        private void HandleDirCommand(ControlCommands command, string directory)
        {
            var data = new byte[Consts.BUFFER_SIZE];
            var commandBytes = BitConverter.GetBytes((int)command);
            var directoryBytes = Encoding.UTF8.GetBytes(directory);

            Array.Copy(commandBytes, data, commandBytes.Length);
            Array.Copy(directoryBytes, 0, data, commandBytes.Length, directoryBytes.Length);
            _sender.Send(data);
            
            var response = new byte[Consts.BUFFER_SIZE];
            _sender.Receive(response);
            ConsoleMethods.WriteLineWithColor(Encoding.UTF8.GetString(response.Where(r => r != 0).ToArray()), ConsoleColor.Green);
        }

        private void HandleGetCommand(string fileToDownloadPath)
        {
            var data = new byte[Consts.BUFFER_SIZE];
            var commandBytes = BitConverter.GetBytes((int)ControlCommands.Get);
            var locationBytes = Encoding.UTF8.GetBytes(fileToDownloadPath);
            
            Array.Copy(commandBytes, data, commandBytes.Length);
            Array.Copy(locationBytes, 0, data, commandBytes.Length, locationBytes.Length);

            _sender.Send(data);
            
            var response = new byte[Consts.BUFFER_SIZE];
            _sender.Receive(response);
            byte[] message = response.Take(Consts.DATA_POSITION).ToArray();
            byte[] fileData = response.Skip(Consts.DATA_POSITION).ToArray();

            if (fileData.All(r => r == 0))
            {
                ConsoleMethods.WriteLineWithColor(Encoding.UTF8.GetString(message.Where(r => r != 0).ToArray()), ConsoleColor.Red);
                return;
            };
            
            if (Binary)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(fileToDownloadPath));
                File.WriteAllBytes(path, fileData);
                Console.WriteLine($"Plik został zapisany w {path}");
            }
            else if (Ascii)
            {
                var fileContent = Encoding.UTF8.GetString(fileData);
                ConsoleMethods.WriteLineWithColor("Zawartość pliku:", ConsoleColor.Green);
                Console.WriteLine(fileContent);
            }

            ConsoleMethods.WriteLineWithColor(Encoding.UTF8.GetString(message.Where(r => r != 0).ToArray()), ConsoleColor.Green);
        }

        private void HandlePutCommand(string filePath, string locationToSave)
        {
            if (string.IsNullOrWhiteSpace(locationToSave))
            {
                locationToSave = Path.GetFileName(filePath);
            }
            
            var data = new byte[Consts.BUFFER_SIZE];
            var commandBytes = BitConverter.GetBytes((int)ControlCommands.Put);
            var locationBytes = Encoding.UTF8.GetBytes(locationToSave);
            var file = File.ReadAllBytes(filePath);
            
            Array.Copy(commandBytes, data, commandBytes.Length);
            Array.Copy(locationBytes, 0, data, commandBytes.Length, locationBytes.Length);
            Array.Copy(file, 0, data, Consts.DATA_POSITION, file.Length);

            _sender.Send(data);
            
            var response = new byte[Consts.BUFFER_SIZE];
            _sender.Receive(response);
            ConsoleMethods.WriteLineWithColor(Encoding.UTF8.GetString(response.Where(r => r != 0).ToArray()), ConsoleColor.Green);
        }
    }
}