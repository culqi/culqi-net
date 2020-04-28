using Culqi.Entities;
using Culqi.Entities.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Culqi.Infrastructure.JsonConverters
{
    public class ExpandableFieldConverter<T> : CulqiObjectConverter where T : IHasId
    {
        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch (value)
            {
                case null:
                    serializer.Serialize(writer, null);
                    break;

                case IExpandableField expandableField:
                    var valueToSerialize = expandableField.IsExpanded
                        ? expandableField.ExpandedObject
                        : expandableField.Id;
                    serializer.Serialize(writer, valueToSerialize);
                    break;

                default:
                    throw new JsonSerializationException(string.Format(
                        "Unexpected value when converting ExpandableField. Expected IExpandableField, got {0}.",
                        value.GetType()));
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = new ExpandableField<T>();

            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    // Do nothing
                    break;

                case JsonToken.String:
                    value.Id = (string)reader.Value;
                    break;

                case JsonToken.StartObject:
                    var jObject = JObject.Load(reader);

                    using (var subReader = jObject.CreateReader())
                    {
                        value.ExpandedObject = (T)base.ReadJson(
                            subReader, typeof(T), value.ExpandedObject, serializer);
                    }

                    // If we failed to deserialize the expanded object (e.g. because of an unknown
                    // type), make a last ditch attempt at getting the ID.
                    if (value.ExpandedObject == null)
                    {
                        value.Id = (string)jObject["id"];
                    }

                    break;

                default:
                    throw new JsonSerializationException(string.Format(
                        "Unexpected token when converting ExpandableField: {0}.",
                        reader.TokenType.ToString()));
            }

            return value;
        }
        public override bool CanConvert(Type objectType)
        {
            return typeof(IExpandableField).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}
