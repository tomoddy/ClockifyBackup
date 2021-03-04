using System;
using System.IO;

namespace ClockifyBackup
{
    class Program
    {
        const int ARGS_LENGTH = 4;

        static void Main(string[] args)
        {
            if (args.Length != ARGS_LENGTH)
                Console.WriteLine($"Expected {ARGS_LENGTH} arguments, got {args.Length}");
            else
            {
                string args0 = Path.GetDirectoryName(args[0]);
                bool args2 = int.TryParse(args[2], out int exportType);
                bool args3 = int.TryParse(args[3], out int dateRange);

                if (!string.IsNullOrEmpty(args0) && args2 && exportType >= 0 && exportType < 3 && args3)
                {
                    Client client = new Client(args[0], args[1], exportType);
                    client.Request(DateTime.Now.AddDays(-dateRange), DateTime.Now);
                }
                else
                    Console.WriteLine("Invalid arguments");
            }
        }
    }
}
