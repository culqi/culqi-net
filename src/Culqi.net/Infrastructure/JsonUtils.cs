using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Culqi.Infrastructure
{
    internal static class JsonUtils
    {
        public static T DeserializeObject<T>(
            string value,
            JsonSerializerSettings settings = null)
        {
            return (T)DeserializeObject(value, typeof(T), settings);
        }

        public static object DeserializeObject(
            string value,
            Type type,
            JsonSerializerSettings settings = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            JsonSerializer jsonSerializer = JsonSerializer.Create(settings);

            using (JsonTextReader reader = new JsonTextReader(new StringReader(value)))
            {
                return jsonSerializer.Deserialize(reader, type);
            }
        }

        public static string SerializeObject(
            object value,
            Formatting formatting = Formatting.None,
            JsonSerializerSettings settings = null)
        {
            JsonSerializer jsonSerializer = JsonSerializer.Create(settings);
            jsonSerializer.Formatting = formatting;

            StringBuilder sb = new StringBuilder(256);
            StringWriter sw = new StringWriter(sb, CultureInfo.InvariantCulture);
            using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.Formatting = jsonSerializer.Formatting;

                jsonSerializer.Serialize(jsonWriter, value);
            }

            return sw.ToString();
        }
    }
}
