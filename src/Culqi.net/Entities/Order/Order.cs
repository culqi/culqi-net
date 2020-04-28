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
    public class Order : CulqiEntity<Order>, IHasObject, IHasId, IHasMetadata
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("payment_code")]
        public string PaymentCode { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("total_fee")]
        public long? TotalFee { get; set; }

        [JsonProperty("net_amount")]
        public long? NetAmount { get; set; }

        [JsonProperty("fee_details")]
        public FeeDetails FeeDetails { get; set; }

        [JsonProperty("creation_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreationDate { get; set; }

        [JsonProperty("expiration_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExpirationDate { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("paid_at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? PaidAt { get; set; }

        [JsonProperty("available_on")]
        public string AvailableOn { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
