using Culqi.Entities.Interfaces;
using Culqi.Infrastructure;
using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class OrderUpdateOptions : BaseOptions, IHasMetadata
    {
        [JsonProperty("expiration_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExpirationDate { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
