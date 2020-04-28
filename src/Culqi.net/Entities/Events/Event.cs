using Culqi.Entities.Base;
using Culqi.Entities.Events;
using Culqi.Entities.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class Event : CulqiEntity<Event>, IHasId, IHasObject
    {
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public EventData Data { get; set; }        
    }
}
