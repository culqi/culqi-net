using Culqi.Entities.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Entities
{
    public class Client : CulqiEntity<Client>
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("ip_country")]
        public string IpCountry { get; set; }

        [JsonProperty("ip_country_code")]
        public string IpCountryCode { get; set; }

        [JsonProperty("browser")]
        public string Browser { get; set; }

        [JsonProperty("device_fingerprint")]
        public string DeviceFingerprint { get; set; }

        [JsonProperty("device_type")]
        public string DeviceType { get; set; }
    }
}
