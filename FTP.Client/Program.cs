using System;

namespace FTP.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj adres serwera [enter dla domyślnej wartości localhost]: ");
            var host = Console.ReadLine();
            if (string.IsNullOrEmpty(host))
            {
                host = "127.0.0.1";
            }
            var client = new Client(8090,host);

            client.Run();
        }
    }
}