using Culqi.Infrastructure.Public;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Culqi.Infrastructure
{
    public class CulqiObjectConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException("CulqiObjectConverter should only be used while deserializing.");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var jsonObject = JObject.Load(reader);
            var objectValue = (string)jsonObject["object"];

            Type concreteType = CulqiTypeRegistry.GetConcreteType(objectType, objectValue);
            if (concreteType == null)
            {
                // Couldn't find a concrete type to instantiate, return null.
                return null;
            }

            var value = Activator.CreateInstance(concreteType);

            using (var subReader = jsonObject.CreateReader())
            {
                serializer.Populate(subReader, value);
            }

            return value;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetTypeInfo().IsInterface;
        }
    }
}
