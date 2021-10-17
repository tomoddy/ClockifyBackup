using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;

namespace ClockifyBackup
{
    /// <summary>
    /// Client class
    /// </summary>
    class Client
    {

        /// <summary>
        /// Rest client
        /// </summary>
        public RestClient RestClient { get; set; }

        /// <summary>
        /// Export path
        /// </summary>
        public string ExportPath { get; set; }

        /// <summary>
        /// Export name
        /// </summary>
        public string ExportName { get; set; }

        /// <summary>
        /// Export type
        /// </summary>
        public ExportTypes ExportType { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="path">Output path</param>
        /// <param name="name">Output file name</param>
        /// <param name="type">Output file type</param>
        public Client(string path, string name, int type)
        {
            RestClient = new RestClient($"https://reports.api.clockify.me/v1/workspaces/{Config.WorkspaceId}/reports/detailed") { Timeout = -1 };
            ExportPath = path;
            ExportName = name;
            ExportType = (ExportTypes)type;
        }

        /// <summary>
        /// Make request
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        public void Request(Logger logger, DateTime startDate, DateTime endDate)
        {
            // Create request body
            logger.Add("Creating request body");
            RequestBodyObj requestBody = new RequestBodyObj(startDate, endDate, ExportType.ToString());

            // Create requst
            logger.Add("Creating request");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-api-key", Config.ApiKey);
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

            // Execute request
            logger.Add("Executing request");
            IRestResponse response = RestClient.Execute(request);

            // Write result to file
            logger.Add("Writing to file");
            string exportName = $"{ExportName}{DateTime.Now.ToString($"-yyyy-MM-dd-HH-mm-ss")}";
            Directory.CreateDirectory($"{ExportPath}/{exportName}");
            File.WriteAllBytes($"{ExportPath}/{exportName}/{exportName}.{ExportType.ToString().ToLower()}", response.RawBytes);
            logger.Add("Backup created and saved");
        }
    }
}