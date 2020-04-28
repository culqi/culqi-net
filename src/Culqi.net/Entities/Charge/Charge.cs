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
    public class Charge : CulqiEntity<Charge>, IHasId, IHasObject, IHasMetadata
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("creation_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreationDate { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("amount_refunded")]
        public long AmountRefunded { get; set; }

        [JsonProperty("current_amount")]
        public long CurrentAmount { get; set; }

        [JsonProperty("installments")]
        public int Installments { get; set; }

        [JsonProperty("installments_amount")]
        public int InstallmentsAmount { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("source")]
        [JsonConverter(typeof(CulqiObjectConverter))]
        public object Source { get; set; }

        [JsonProperty("client")]
        public Client Client { get; set; }

        [JsonProperty("outcome")]
        public ChargeOutcome Outcome { get; set; }

        [JsonProperty("fraud_score")]
        public float FraudScore { get; set; }

        [JsonProperty("antifraud_details")]
        public AntiFraudDetails AntiFraudDetails { get; set; }

        [JsonProperty("dispute")]
        public bool? Dispute { get; set; }

        [JsonProperty("capture")]
        public bool? Captured { get; set; }

        [JsonProperty("reference_code")]
        public string ReferenceCode { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }

        [JsonProperty("total_fee")]
        public long TotalFee { get; set; }

        [JsonProperty("fee_details")]
        public FeeDetails FeeDetails { get; set; }

        [JsonProperty("paid")]
        public bool Paid { get; set; }

        [JsonProperty("statement_descriptor")]
        public string StatementDescriptor { get; set; }

        [JsonProperty("total_fee_taxes")]
        public int TotalFeeTaxes { get; set; }

        [JsonProperty("transfer_amount")]
        public long TransferAmount { get; set; }

        [JsonProperty("duplicated")]
        public bool Duplicated { get; set; }
    }    
}
