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
        /// <param name="args">
        /// 0 - Output path
        /// 1 - Export name
        /// 2 - Export type
        /// 3 - Date range (days)
        /// </param>
        /// <returns>
        /// 0 - Success
        /// -1 - Failure
        /// -2 - Missing arguments
        /// -3 - Invalud arguments
        /// </returns>
        static int Main(string[] args)
        {
            // Create variables
            Logger logger = new Logger();
            int retVal = 0;
            try
            {
                if (args.Length != 4)
                {
                    // Invalid number of arguments
                    logger.Add($"Expected 4 arguments, got {args.Length}");
                    retVal = -2;
                }
                else
                {
                    // Get argument check values
                    string exportPath = Path.GetFullPath(args[0]);
                    string exportName = args[1];
                    bool exportTypeFlag = int.TryParse(args[2], out int exportType);
                    bool dateRangeFlag = int.TryParse(args[3], out int dateRange);

                    // Check argument values
                    if (!string.IsNullOrEmpty(exportPath) && exportTypeFlag && dateRangeFlag && exportType >= 0 && exportType < 3)
                    {
                        // Create client and make request
                        Client client = new Client(exportPath, exportName, exportType);
                        client.Request(logger, DateTime.Now.AddDays(-dateRange), DateTime.Now);
                    }
                    else
                    {
                        // Print error message
                        logger.Add("Invalid arguments");
                        retVal = -3;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                retVal = -1;
                logger.Add(ex.Message);
            }

            // Save logs and output response code
            logger.Save();
            return retVal;
        }
    }
}