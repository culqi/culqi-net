using Culqi.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Culqi.Infrastructure.JsonConverters
{
    public class AnyOfConverter : JsonConverter
    {
        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch (value)
            {
                case null:
                    serializer.Serialize(writer, null);
                    break;

                case IAnyOf anyOf:
                    serializer.Serialize(writer, anyOf.Value);
                    break;

                default:
                    throw new JsonSerializationException(string.Format(
                        "Unexpected value when converting AnyOf. Expected IAnyOf, got {0}.",
                        value.GetType()));
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            object o = null;

            // Try to deserialize with each possible type
            var jToken = JToken.Load(reader);
            foreach (var type in objectType.GenericTypeArguments)
            {
                try
                {
                    using (var subReader = jToken.CreateReader())
                    {
                        o = serializer.Deserialize(subReader, type);
                    }

                    // If deserialization succeeds, stop
                    break;
                }
                catch (JsonException)
                {
                    // Do nothing, just try the next type
                }
            }

            if (o == null)
            {
                throw new JsonSerializationException(string.Format(
                    "Cannot deserialize the current JSON object into any of the expected types ({0}).",
                    string.Join(", ", objectType.GenericTypeArguments.Select(t => t.FullName))));
            }

            return Activator.CreateInstance(objectType, o);
        }
        public override bool CanConvert(Type objectType)
        {
            return typeof(IAnyOf).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}
