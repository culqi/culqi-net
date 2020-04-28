using Culqi.Entities.Interfaces;
using Culqi.Infrastructure.JsonConverters;
using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class TokenCreateOptions : BaseOptions, IHasMetadata
    {
        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [JsonProperty("cvv")]
        public string Cvv { get; set; }

        [JsonProperty("expiration_month")]
        public string ExpirationMonth { get; set; }

        [JsonProperty("expiration_year")]
        public string ExpirationYear { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
