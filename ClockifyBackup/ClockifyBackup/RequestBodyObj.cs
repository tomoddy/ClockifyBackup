using Newtonsoft.Json;
using System;

namespace ClockifyBackup
{
    public class RequestBodyObj
    {
        [JsonProperty("dateRangeStart")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-ddTHH:mm:ss.fff")]
        public DateTime StartDate { get; set; }

        [JsonProperty("dateRangeEnd")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-ddTHH:mm:ss.fff")]
        public DateTime EndDate { get; set; }

        [JsonProperty("detailedFilter")]
        public DetailedFilterObj DetailedFilter { get; set; }

        [JsonProperty("exportType")]
        public string ExportType { get; set; }

        public class DetailedFilterObj
        {
            [JsonProperty("page")]
            public int Page { get; set; }

            [JsonProperty("pageSize")]
            public int PageSize { get; set; }
        }
    }
}
