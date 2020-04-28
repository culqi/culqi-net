using Culqi.Entities.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Entities
{
    public class FeeDetails : CulqiEntity<FeeDetails>
    {
        [JsonProperty("fixed_fee")]
        public FixedFee FixedFee { get; set; }

        [JsonProperty("variable_fee")]
        public VariableFee VariableFee { get; set; }
    }

    public class FixedFee : CulqiEntity<FixedFee>
    {
    }

    public class VariableFee : CulqiEntity<VariableFee>
    {
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("commision")]
        public float Commision { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
