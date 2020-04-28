using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class ChargeListOptions : ListOptions
    {

        [JsonProperty("amount")]
        public long? Amount { get; set; }

        [JsonProperty("duplicated")]
        public bool Duplicated { get; set; }

        [JsonProperty("installments")]
        public int Installments { get; set; }

        [JsonProperty("min_installments")]
        public int MinInstallments { get; set; }

        [JsonProperty("max_installments")]
        public int MaxInstallments { get; set; }
    }
}
