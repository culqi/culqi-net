using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class TokenListOptions : ListOptions
    {
        [JsonProperty("bin")]
        public long? Bin { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("card_brand")]
        public string CardBrand { get; set; }

        [JsonProperty("card_type")]
        public string CardType { get; set; }

        [JsonProperty("device_type")]
        public string DeviceType { get; set; }
    }
}
