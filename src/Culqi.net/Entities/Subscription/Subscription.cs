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
    public class Subscription : CulqiEntity<Subscription>, IHasId, IHasMetadata, IHasObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("creation_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreationDate { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("current_period")]
        public long? CurrentPeriod { get; set; }

        [JsonProperty("total_periods")]
        public long? TotalPeriods { get; set; }

        [JsonProperty("current_period_start")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CurrentPeriodStart { get; set; }

        [JsonProperty("current_period_end")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CurrentPeriodEnd { get; set; }

        [JsonProperty("cancel_at_period_end")]
        public bool CancelAtPeriodEnd { get; set; }

        [JsonProperty("cancel_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CancelAt { get; set; }

        [JsonProperty("ended_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? EndedAt { get; set; }

        [JsonProperty("next_billing_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? NextBillingDate { get; set; }

        [JsonProperty("trial_start")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? TrialStart { get; set; }

        [JsonProperty("trial_end")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? TrialEnd { get; set; }

        [JsonProperty("charges")]
        public CulqiList<Charge> Charges { get; set; }

        [JsonProperty("plan")]
        public Plan Plan { get; set; }

        [JsonProperty("card")]
        public Card Card { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
