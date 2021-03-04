using System;
using System.IO;

namespace ClockifyBackup
{
    /// <summary>
    /// Main class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">Comand line arguments</param>
        static void Main(string[] args)
        {
            if (args.Length != 4)
                // Invalid number of arguments
                Console.WriteLine($"Expected 4 arguments, got {args.Length}");
            else
            {
                // Get argument check values
                string args0 = Path.GetDirectoryName(args[0]);
                bool args2 = int.TryParse(args[2], out int exportType);
                bool args3 = int.TryParse(args[3], out int dateRange);

                // Check argument values
                if (!string.IsNullOrEmpty(args0) && args2 && exportType >= 0 && exportType < 3 && args3)
                {
                    // Create client and make request
                    Client client = new Client(args[0], args[1], exportType);
                    client.Request(DateTime.Now.AddDays(-dateRange), DateTime.Now);
                }
                else
                    // Print error message
                    Console.WriteLine("Invalid arguments");
            }

            // Print success message
            Console.WriteLine("SUCCESS");
        }
    }
}