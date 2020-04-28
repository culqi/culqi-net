using Culqi.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Services.Base
{
    // Base class for Culqi options classes, i.e. classes representing parameters for Culqi API requests.
    [JsonObject(MemberSerialization.OptIn)]
    public class BaseOptions : INestedOptions
    {
        [JsonProperty("expand", NullValueHandling = NullValueHandling.Ignore)]
        protected List<string> Expand { get; set; }

        [JsonExtensionData]
        protected IDictionary<string, object> ExtraParams { get; set; }
            = new Dictionary<string, object>();        
    }
}
