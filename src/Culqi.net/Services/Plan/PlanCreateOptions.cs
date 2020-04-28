using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class PlanCreateOptions : BaseOptions
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("amount")]
        public long? Amount { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("interval_count")]
        public long IntervalCount { get; set; }

        [JsonProperty("interval")]
        public string Interval { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }
    }
}
