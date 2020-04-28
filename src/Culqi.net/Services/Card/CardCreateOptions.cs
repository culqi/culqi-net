using Culqi.Entities.Interfaces;
using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class CardCreateOptions : BaseOptions, IHasMetadata
    {
        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("token_id")]
        public string TokenId { get; set; }

        [JsonProperty("validate")]
        public bool? Validate { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
