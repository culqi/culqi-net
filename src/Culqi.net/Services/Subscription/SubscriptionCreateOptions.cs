using Culqi.Entities.Interfaces;
using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class SubscriptionCreateOptions : BaseOptions, IHasMetadata
    {
        [JsonProperty("card_id")]
        public string CardId { get; set; }
        [JsonProperty("plan_id")]
        public string PlanId { get; set; }
        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
