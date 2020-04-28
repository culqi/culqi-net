using Culqi.Entities;
using Culqi.Entities.Base;
using Culqi.Entities.Interfaces;
using Culqi.Infrastructure;
using Culqi.Infrastructure.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi
{
    public class Card : CulqiEntity<Card>, IHasObject, IHasId, IHasMetadata
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("creation_date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreationDate { get; set; }

        [JsonIgnore]
        public string CustomerId
        {
            get => InternalCustomer?.Id;
            set => InternalCustomer = SetExpandableFieldId(value, InternalCustomer);
        }

        [JsonIgnore]
        public Customer Customer
        {
            get => InternalCustomer?.ExpandedObject;
            set => InternalCustomer = SetExpandableFieldObject(value, InternalCustomer);
        }

        [JsonProperty("customer_id")]
        [JsonConverter(typeof(ExpandableFieldConverter<Customer>))]
        internal ExpandableField<Customer> InternalCustomer { get; set; }

        [JsonProperty("source")]
        [JsonConverter(typeof(CulqiObjectConverter))]
        public object Source { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
