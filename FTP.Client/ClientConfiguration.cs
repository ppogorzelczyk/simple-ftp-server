using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FTP.Client
{
    public partial class Client
    {
        private readonly int _port;
        private readonly string _hostEntry;
        private IPHostEntry _host;
        private IPAddress _ipAddress;
        private IPEndPoint _endPoint;
        private Socket _sender;
        private bool _ascii = false;
        private bool _binary = true;
        
        public bool Binary
        {
            get => _binary;
            set
            {
                _binary = value;
                _ascii = !value;
            }
        }
        
        public bool Ascii
        {
            get => _ascii;
            set
            {
                _ascii = value;
                _binary = !value;
            }
        }

        private void ActivateConnection()
        {
            ConsoleMethods.WriteLineWithColor("Łączenie z serwerem...", ConsoleColor.Cyan);

            try
            {
                _sender.Connect(_endPoint);
            }
            catch
            {
                ConsoleMethods.WriteLineWithColor("Nie można połączyć z serwerem.", ConsoleColor.Red);
                throw;
            }
            
            ConsoleMethods.WriteLineWithColor($"Połączono z {_sender.RemoteEndPoint}", ConsoleColor.Green);
        }

        private Socket NewSender()
        {
            var sender = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = 5000,
                SendTimeout = 5000
            };
            return sender;
        }

        private void ConfigureClient()
        {
            _host = Dns.GetHostEntry(_hostEntry);
            _ipAddress = _host.AddressList.First();
            _endPoint = new IPEndPoint(_ipAddress, _port);
        }

        private void HandleConnectionTimeout()
        {
            Thread.Sleep(1 * 1000);
            ConsoleMethods.WriteLineWithColor("Próba ponownego połaczenia za 5s.", ConsoleColor.DarkYellow);
            ConsoleMethods.WriteLineWithColor("Oczekiwanie na serwer...", ConsoleColor.DarkYellow);
            Thread.Sleep(5 * 1000);
            Run();
        }

        private void EndConnection()
        {
            _sender.Shutdown(SocketShutdown.Both);
            _sender.Close();
        }
    }
}