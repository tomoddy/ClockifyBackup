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
            // Create log directory variable
            string args1 = null;
            try
            {

                if (args.Length != 5)
                    // Invalid number of arguments
                    Console.WriteLine($"Expected 4 arguments, got {args.Length}");
                else
                {
                    // Get argument check values
                    string args0 = Path.GetFullPath(args[0]);
                    args1 = Path.GetFullPath(args[1]);
                    bool args3 = int.TryParse(args[3], out int exportType);
                    bool args4 = int.TryParse(args[4], out int dateRange);

                    // Create directories
                    Directory.CreateDirectory(args0);
                    Directory.CreateDirectory(args1);

                    // Check argument values
                    if (!string.IsNullOrEmpty(args0) && !string.IsNullOrEmpty(args1) && args3 && args4 && exportType >= 0 && exportType < 3)
                    {
                        // Create client and make request
                        Client client = new Client(args[0], args[2], exportType);
                        client.Request(DateTime.Now.AddDays(-dateRange), DateTime.Now);
                    }
                    else
                        // Print error message
                        Console.WriteLine("Invalid arguments");
                }

                // Print success message
                Console.WriteLine("SUCCESS");
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine(ex);

                // Write to log file
                if (args1 != null)
                    File.AppendAllText($"{args1}/clockify-backup-log{ DateTime.Now.ToString($"-yyyy-MM-dd")}.txt", ex.ToString());
                else
                    Console.WriteLine("Could not log error");
            }
        }
    }
}