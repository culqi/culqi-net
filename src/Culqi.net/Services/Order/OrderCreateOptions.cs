using Culqi.Infrastructure;
using Culqi.Infrastructure.JsonConverters;
using Culqi.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class OrderCreateOptions : BaseOptions
    {
        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }

        [JsonProperty("client_details")]
        [JsonConverter(typeof(AnyOfConverter))]
        public AnyOf<string, CustomerCreateOptions> Client { get; set; }

        [JsonProperty("expiration_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExpirationDate { get; set; }
    }
}
