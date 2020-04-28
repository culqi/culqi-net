using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class SubscriptionListOptions : ListOptions
    {
        [JsonProperty("amount")]
        public long? Amount { get; set; }

        [JsonProperty("interval")]
        public string Interval { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
