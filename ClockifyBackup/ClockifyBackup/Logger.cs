using System;
using System.Collections.Generic;
using System.IO;

namespace ClockifyBackup
{
    /// <summary>
    /// Logging object
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Log file path
        /// </summary>
        private string Path { get; set; }

        /// <summary>
        /// List of messages
        /// </summary>
        private List<string> Messages { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Logger()
        {
            Path = $"{Config.LogOutput}/ClockifyBackup-{DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}.log";
            Messages = new List<string>
            {
                "---------------------------------------------",
                DateTime.UtcNow.ToString("G")
            };
        }

        /// <summary>
        /// Add message to list
        /// </summary>
        /// <param name="message">Message to add to list of logs</param>
        public void Add(string message)
        {
            Messages.Add($"{DateTime.UtcNow:HH:mm:ss.fff} | {message}");
        }

        /// <summary>
        /// Save logs to file
        /// </summary>
        public void Save()
        {
            if (File.Exists(Config.LogOutput))
                File.AppendAllLines(Path, Messages);
            else
                File.WriteAllLines(Path, Messages);
        }
    }
}