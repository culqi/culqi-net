using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Services.Events
{
    public class EventListOptions : ListOptions
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
