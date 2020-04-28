using Culqi.Entities.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Entities
{
    public class ChargeOutcome : CulqiEntity<ChargeOutcome>
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("merchant_message")]
        public string MerchantMessage { get; set; }

        [JsonProperty("user_message")]
        public string UserMessage { get; set; }
    }
}
