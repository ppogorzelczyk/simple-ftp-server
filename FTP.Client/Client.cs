using System;
using System.Net.Sockets;

namespace FTP.Client
{
    public partial class Client
    {
        public Client(int port, string hostEntry = "localhost")
        {
            _port = port;
            _hostEntry = hostEntry;
            Console.CancelKeyPress += delegate
            {
                EndConnection();
            };
        }

        public bool Run()
        {
            ConfigureClient();
            try
            {
                _sender = NewSender();
                ActivateConnection();
                //while (!shouldExit)
                //{
                //    ConsoleMethods.DisplayCommands();
                //    shouldExit = GetUserCommand();
                //}
            }
            catch (SocketException e)
            {
                if (e.ErrorCode == 61)
                {
                    HandleConnectionTimeout();
                }
                else
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            IsConnected = true;
            return true;
        }


        private bool GetUserCommand()
        {
            var input = Console.ReadLine()?.Split("--");
            if (input is null)
            {
                ConsoleMethods.WriteLineWithColor("\nWystąpił błąd w trakcie odczytywania komendy.", ConsoleColor.Red);
                return false;
            }
            
            var command = input[0].Trim().ToLower();

            switch (command)
            {
                case "ascii":
                    Ascii = true;
                    ConsoleMethods.WriteLineWithColor("Włączono tryb ASCII", ConsoleColor.Yellow);
                    break;
                case "binary":
                    Binary = true;
                    ConsoleMethods.WriteLineWithColor("Włączono tryb BINARY", ConsoleColor.Yellow);
                    break;
                case "mkdir":
                    if (input.Length == 2)
                        HandleDirCommand(ControlCommands.Mkdir, input[1].Trim());
                    else
                        ConsoleMethods.WriteLineWithColor("komenda MKDIR musi być wywołana z 1 parametrem!", ConsoleColor.Red);
                    break;
                case "rmdir":
                    if (input.Length == 2)
                        HandleDirCommand(ControlCommands.Rmdir, input[1].Trim());
                    else
                        ConsoleMethods.WriteLineWithColor("komenda RMDIR musi być wywołana z 1 parametrem!", ConsoleColor.Red);
                    break;
                case "put":
                    if (input.Length > 2)
                        HandlePutCommand(input[1].Trim(), input[2].Trim());
                    else
                        ConsoleMethods.WriteLineWithColor("komenda PUT musi być wywołana z 2 parametrami!", ConsoleColor.Red);
                    break;
                case "get":
                    if (input.Length == 2)
                        HandleGetCommand(input[1].Trim());
                    else
                        ConsoleMethods.WriteLineWithColor("komenda GET musi być wywołana z 1 parametrem!", ConsoleColor.Red);
                    break;
                case "exit":
                    return true;
                default:
                    ConsoleMethods.WriteLineWithColor("Nierozpoznana komenda", ConsoleColor.Red);
                    break;
            }
            
            return false;
        }
    }
}