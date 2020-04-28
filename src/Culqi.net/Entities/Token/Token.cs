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
    public class Token : CulqiEntity<Token>, IHasId, IHasObject, IHasMetadata
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("creation_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreationDate { get; set; }

        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [JsonProperty("last_four")]
        public string LastFour { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("iin")]
        public Iin Iin { get; set; }

        [JsonProperty("client")]
        public Client Client { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
