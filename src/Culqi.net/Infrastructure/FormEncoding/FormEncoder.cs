using Culqi.Infrastructure.Interfaces;
using Culqi.Services.Base;
using Culqi.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using IAnyOf = Culqi.Services.Interfaces.IAnyOf;

namespace Culqi.Infrastructure.FormEncoding
{
    internal static class FormEncoder
    {
        public static HttpContent CreateHttpContent(BaseOptions options)
        {
            // If options is null, we create an empty FormUrlEncodedContent because we still
            // want to send the Content-Type header.
            if (options == null)
            {
                return new StringContent(string.Empty);
            }

            var flatParams = FlattenParamsValue(options, null);

            // If all parameters have been encoded as strings, then the content can be represented
            // with application/json encoding. Otherwise, use
            // multipart/form-data encoding.
            if (flatParams.All(kvp => kvp.Value is string))
            {
                var content = JsonConvert.SerializeObject(options);
                return new StringContent(content, Encoding.UTF8, "application/json");
            }
            else
            {
                return new MultipartFormDataContent(flatParams);
            }
        }

        public static string CreateQueryString(BaseOptions options)
        {
            var flatParams = FlattenParamsValue(options, null)
                .Where(kvp => kvp.Value is string)
                .Select(kvp => new KeyValuePair<string, string>(
                    kvp.Key,
                    kvp.Value as string));
            return CreateQueryString(flatParams);
        }

        public static string CreateQueryString(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            var parameters = nameValueCollection.ToList();
            parameters.Remove(parameters.Where(x => x.Key == "creation_date" && x.Value == "0").FirstOrDefault());
            return string.Join(
                "&",
                parameters.Select(kvp => $"{UrlEncode(kvp.Key)}={UrlEncode(kvp.Value)}"));
        }

        private static string UrlEncode(string value)
        {
            return WebUtility.UrlEncode(value)
                /* Don't use strict form encoding by changing the square bracket control
                 * characters back to their literals. This is fine by the server, and
                 * makes these parameter strings easier to read. */
                .Replace("%5B", "[")
                .Replace("%5D", "]");
        }

        private static List<KeyValuePair<string, object>> FlattenParamsValue(object value, string keyPrefix)
        {
            List<KeyValuePair<string, object>> flatParams = null;

            switch (value)
            {
                case null:
                    flatParams = SingleParam(keyPrefix, string.Empty);
                    break;

                case IAnyOf anyOf:
                    flatParams = FlattenParamsAnyOf(anyOf, keyPrefix);
                    break;

                case INestedOptions options:
                    flatParams = FlattenParamsOptions(options, keyPrefix);
                    break;

                case IDictionary dictionary:
                    flatParams = FlattenParamsDictionary(dictionary, keyPrefix);
                    break;

                case string s:
                    flatParams = SingleParam(keyPrefix, s);
                    break;

                case Stream s:
                    flatParams = SingleParam(keyPrefix, s);
                    break;

                case IEnumerable enumerable:
                    flatParams = FlattenParamsList(enumerable, keyPrefix);
                    break;

                case DateTime dateTime:
                    flatParams = SingleParam(
                    keyPrefix,
                    dateTime.ConvertDateTimeToEpoch().ToString(CultureInfo.InvariantCulture));
                    break;

                case Enum e:
                    flatParams = SingleParam(keyPrefix, JsonUtils.SerializeObject(e).Trim('"'));
                    break;

                default:
                    flatParams = SingleParam(
                        keyPrefix,
                        string.Format(CultureInfo.InvariantCulture, "{0}", value));
                    break;
            }

            return flatParams;
        }

        private static List<KeyValuePair<string, object>> FlattenParamsAnyOf(IAnyOf anyOf, string keyPrefix)
        {
            List<KeyValuePair<string, object>> flatParams = new List<KeyValuePair<string, object>>();

            // If the value contained within the `AnyOf` instance is null, we don't encode it in the
            // request. We do this to mimic the behavior of regular (non-`AnyOf`) properties in
            // options classes, which are skipped by the encoder when they have a null value
            // because it's the default value (cf. FlattenParamsOptions).
            if (anyOf.Value == null)
            {
                return flatParams;
            }

            flatParams.AddRange(FlattenParamsValue(anyOf.Value, keyPrefix));

            return flatParams;
        }

        private static List<KeyValuePair<string, object>> FlattenParamsOptions(INestedOptions options, string keyPrefix)
        {
            List<KeyValuePair<string, object>> flatParams = new List<KeyValuePair<string, object>>();
            if (options == null)
            {
                return flatParams;
            }

            foreach (var property in options.GetType().GetRuntimeProperties())
            {
                // `[JsonExtensionData]` tells the serializer to write the values contained in
                // the collection as if they were class properties.
                var extensionAttribute = property.GetCustomAttribute<JsonExtensionDataAttribute>();
                if (extensionAttribute != null)
                {
                    var extensionValue = property.GetValue(options, null) as IDictionary;

                    flatParams.AddRange(FlattenParamsDictionary(extensionValue, keyPrefix));
                    continue;
                }

                // Skip properties not annotated with `[JsonProperty]`
                var attribute = property.GetCustomAttribute<JsonPropertyAttribute>();
                if (attribute == null)
                {
                    continue;
                }

                var value = property.GetValue(options, null);

                // Fields on a class which are never set by the user will contain null values (for
                // reference types), so skip those to avoid encoding them in the request.
                if (value == null)
                {
                    continue;
                }

                string key = attribute.PropertyName;
                string newPrefix = NewPrefix(key, keyPrefix);

                flatParams.AddRange(FlattenParamsValue(value, newPrefix));
            }

            return flatParams;
        }

        private static List<KeyValuePair<string, object>> FlattenParamsDictionary(IDictionary dictionary, string keyPrefix)
        {
            List<KeyValuePair<string, object>> flatParams = new List<KeyValuePair<string, object>>();
            if (dictionary == null)
            {
                return flatParams;
            }

            foreach (DictionaryEntry entry in dictionary)
            {
                string key = string.Format(CultureInfo.InvariantCulture, "{0}", entry.Key);
                object value = entry.Value;

                string newPrefix = NewPrefix(key, keyPrefix);

                flatParams.AddRange(FlattenParamsValue(value, newPrefix));
            }

            return flatParams;
        }

        private static List<KeyValuePair<string, object>> FlattenParamsList(IEnumerable list, string keyPrefix)
        {
            List<KeyValuePair<string, object>> flatParams = new List<KeyValuePair<string, object>>();
            if (list == null)
            {
                return flatParams;
            }

            int index = 0;
            foreach (object value in list)
            {
                string newPrefix = $"{keyPrefix}[{index}]";
                flatParams.AddRange(FlattenParamsValue(value, newPrefix));
                index += 1;
            }

            /* Because application/x-www-form-urlencoded cannot represent an empty list, convention
             * is to take the list parameter and just set it to an empty string. (E.g. A regular
             * list might look like `a[0]=1&b[1]=2`. Emptying it would look like `a=`.) */
            if (!flatParams.Any())
            {
                flatParams.Add(new KeyValuePair<string, object>(keyPrefix, string.Empty));
            }

            return flatParams;
        }

        private static List<KeyValuePair<string, object>> SingleParam(string key, object value)
        {
            List<KeyValuePair<string, object>> flatParams = new List<KeyValuePair<string, object>>();
            flatParams.Add(new KeyValuePair<string, object>(key, value));
            return flatParams;
        }

        private static string NewPrefix(string key, string keyPrefix)
        {
            if (string.IsNullOrEmpty(keyPrefix))
            {
                return key;
            }

            int i = key.IndexOf("[", StringComparison.Ordinal);
            if (i == -1)
            {
                return $"{keyPrefix}[{key}]";
            }
            else
            {
                return $"{keyPrefix}[{key.Substring(0, i)}]{key.Substring(i)}";
            }
        }
    }
}
