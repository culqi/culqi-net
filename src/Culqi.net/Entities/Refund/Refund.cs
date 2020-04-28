using Culqi.Entities.Base;
using Culqi.Entities.Interfaces;
using Culqi.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class Refund : CulqiEntity<Refund>, IHasId, IHasObject, IHasMetadata
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("charge_id")]
        public string ChargeId { get; set; }

        [JsonProperty("creation_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreationDate { get; set; }

        [JsonProperty("amount")]
        public long? Amount { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
