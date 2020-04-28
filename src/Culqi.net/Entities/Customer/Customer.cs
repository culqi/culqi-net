using Culqi.Entities;
using Culqi.Entities.Base;
using Culqi.Entities.Interfaces;
using Culqi.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class Customer : CulqiEntity<Customer>, IHasId, IHasObject, IHasMetadata
    {
        [JsonProperty("object")]
        public string Object { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("creation_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreationDate { get; set; }

        [JsonProperty("antifraud_details")]
        public AntiFraudDetails AntiFraudDetails { get; set; }

        [JsonProperty("metada")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
