using System;
using System.Linq;

namespace FTP.Client
{
    public static class ConsoleMethods
    {
        public static void WriteLineWithColor(string text, ConsoleColor color)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = defaultColor;
        }

        public static void DisplayCommands()
        {
            var options = Enum.GetValues(typeof(ControlCommands))
                .Cast<ControlCommands>()
                .ToDictionary(t => (int)t, t => t.ToString() );

            WriteLineWithColor("Komenda z parametrami musi zawierać -- pomiędzy wszystkimi parametrami", ConsoleColor.Yellow);
            foreach (var (key, value) in options)
            {
                Console.WriteLine($"- {value}");
            }

            Console.WriteLine("Twoja komenda:");
        }
    }
}