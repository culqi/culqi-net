using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class PlanListOptions : ListOptions
    {
        [JsonProperty("amount")]
        public long? Amount { get; set; }
    }
}
