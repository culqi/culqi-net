using Culqi.Entities.Base;
using Culqi.Entities.Interfaces;
using Culqi.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Entities.Events
{
    public class EventData : CulqiEntity<EventData>
    {
        [JsonProperty("object")]
        [JsonConverter(typeof(CulqiObjectConverter))]
        public IHasObject Object { get; set; }

        [JsonIgnore]
        public dynamic RawObject { get; set; }
    }
}
