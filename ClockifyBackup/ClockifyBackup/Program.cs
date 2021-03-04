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

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-api-key", ConfigurationManager.AppSettings.Get("ApiKey"));
            request.AddParameter("application/json", "{\r\n  \"dateRangeStart\": \"2021-01-01T00:00:00.000\",\r\n  \"dateRangeEnd\": \"2021-01-05T23:59:59.000\",\r\n  \"detailedFilter\": {\r\n    \"page\": 1,\r\n    \"pageSize\": 50\r\n  },\r\n  \"exportType\": \"JSON\"\r\n}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}
