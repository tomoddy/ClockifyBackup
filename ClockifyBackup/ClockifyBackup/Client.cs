using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;
using System.IO;

namespace ClockifyBackup
{
    class Client
    {
        public enum ExportTypes
        {
            JSON,
            CSV,
            XLSX
        }

        public string ApiKey { get; set; }
        public RestClient RestClient { get; set; }
        public string ExportPath { get; set; }
        public string ExportName { get; set; }
        public ExportTypes ExportType { get; set; }

        public Client(string path, string name, int type)
        {
            ApiKey = ConfigurationManager.AppSettings.Get("ApiKey");
            RestClient = new RestClient($"https://reports.api.clockify.me/v1/workspaces/{ConfigurationManager.AppSettings.Get("WorkspaceId")}/reports/detailed") { Timeout = -1 };
            ExportPath = path;
            ExportName = name;
            ExportType = (ExportTypes)type;
        }

        public void Request(DateTime startDate, DateTime endDate)
        { 
            RequestBodyObj requestBody = new RequestBodyObj(startDate, endDate, ExportType.ToString());

            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-api-key", ApiKey);
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

            IRestResponse response = RestClient.Execute(request);
            File.WriteAllBytes($"{ExportPath}/{ExportName}{DateTime.Now.ToString($"-yyyy-MM-dd-HH-mm-ss")}.{ExportType}", response.RawBytes);
        }
    }
}
