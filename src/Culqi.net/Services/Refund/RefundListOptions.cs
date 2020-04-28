using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class RefundListOptions : ListOptions
    {
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
