using Culqi.Entities.Base;
using Culqi.Entities.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Entities
{
    public class Iin : CulqiEntity<Iin>, IHasObject
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("bin")]
        public string Bin { get; set; }

        [JsonProperty("card_brand")]
        public string CardBrand { get; set; }

        [JsonProperty("card_type")]
        public string CardType { get; set; }

        [JsonProperty("card_category")]
        public string CardCategory { get; set; }

        [JsonProperty("issuer")]
        public Issuer Issuer { get; set; }

        [JsonProperty("installments_allowed")]
        public List<int> InstallmentsAllowed { get; set; }
    }
}
