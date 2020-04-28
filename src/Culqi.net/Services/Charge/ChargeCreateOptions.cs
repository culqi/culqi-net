using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class ChargeCreateOptions : BaseOptions
    {
        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("source_id")]
        public string SourceId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
