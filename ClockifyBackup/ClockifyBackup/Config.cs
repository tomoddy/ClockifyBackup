using System.Configuration;

namespace ClockifyBackup
{
    /// <summary>
    /// Project configuration values
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Log file output
        /// </summary>
        public static string LogOutput
        {
            get { return ConfigurationManager.AppSettings.Get("LogOutput"); }
        }

        /// <summary>
        /// Clockify api key
        /// </summary>
        public static string ApiKey
        {
            get { return ConfigurationManager.AppSettings.Get("ApiKey"); }
        }

        /// <summary>
        /// Clockify workspace id
        /// </summary>WorkspaceId
        public static string WorkspaceId
        {
            get { return ConfigurationManager.AppSettings.Get("WorkspaceId"); }
        }
    }
}