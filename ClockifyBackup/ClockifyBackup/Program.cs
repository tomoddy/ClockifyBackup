using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;

namespace ClockifyBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            string workspaceId = ConfigurationManager.AppSettings.Get("WorkspaceId");
            RestClient client = new RestClient($"https://reports.api.clockify.me/v1/workspaces/{workspaceId}/reports/detailed")
            {
                Timeout = -1
            };

            RestRequest request = new RestRequest(Method.POST);

            RequestBodyObj requestBody = new RequestBodyObj
            {
                StartDate = new DateTime(2021, 1, 1, 0, 0, 0),
                EndDate = new DateTime(2021, 1, 5, 23, 23, 59),
                DetailedFilter = new RequestBodyObj.DetailedFilterObj
                {
                    Page = 1,
                    PageSize = 50
                },
                ExportType = "JSON"
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-api-key", ConfigurationManager.AppSettings.Get("ApiKey"));
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}
