using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Services.Base
{
    public class ListOptions : BaseOptions
    {
        [JsonProperty("creation_date")]
        public DateTime? CreationDate { get; set; }

        [JsonProperty("limit")]
        public long? Limit { get; set; }

        [JsonProperty("after")]
        public string After { get; set; }

        [JsonProperty("before")]
        public string Before { get; set; }
    }
}
