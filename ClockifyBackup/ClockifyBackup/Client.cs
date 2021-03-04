using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;
using System.IO;

namespace ClockifyBackup
{
    /// <summary>
    /// Client class
    /// </summary>
    class Client
    {
        // Enums
        public enum ExportTypes
        {
            JSON,
            CSV,
            XLSX
        }

        // Properties
        public string ApiKey { get; set; }
        public RestClient RestClient { get; set; }
        public string ExportPath { get; set; }
        public string ExportName { get; set; }
        public ExportTypes ExportType { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="path">Output path</param>
        /// <param name="name">Output file name</param>
        /// <param name="type">Output file type</param>
        public Client(string path, string name, int type)
        {
            ApiKey = ConfigurationManager.AppSettings.Get("ApiKey");
            RestClient = new RestClient($"https://reports.api.clockify.me/v1/workspaces/{ConfigurationManager.AppSettings.Get("WorkspaceId")}/reports/detailed") { Timeout = -1 };
            ExportPath = path;
            ExportName = name;
            ExportType = (ExportTypes)type;
        }

        /// <summary>
        /// Make request
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        public void Request(DateTime startDate, DateTime endDate)
        {
            // Create request body
            Console.WriteLine("Creating request body");
            RequestBodyObj requestBody = new RequestBodyObj(startDate, endDate, ExportType.ToString());

            // Create requst
            Console.WriteLine("Creating request");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-api-key", ApiKey);
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

            // Execute request
            Console.WriteLine("Executing request");
            IRestResponse response = RestClient.Execute(request);

            // Write result to file
            Console.WriteLine("Writing to file");
            File.WriteAllBytes($"{ExportPath}/{ExportName}{DateTime.Now.ToString($"-yyyy-MM-dd-HH-mm-ss")}.{ExportType.ToString().ToLower()}", response.RawBytes);
        }
    }
}