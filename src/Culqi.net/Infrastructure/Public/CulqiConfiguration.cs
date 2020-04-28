using Culqi.Exceptions;
using Culqi.Infrastructure;
using Culqi.Infrastructure.Interfaces;
using Culqi.Infrastructure.Public;
using Culqi.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;

namespace Culqi
{
    public class CulqiConfiguration
    {
        private static string apiKey;

        private static ICulqiClient culqiClient;

        static CulqiConfiguration()
        {
            CulqiNetVersion = new AssemblyName(typeof(CulqiConfiguration).GetTypeInfo().Assembly.FullName).Version.ToString(3);
        }

        public static string ApiKey
        {
            get
            {
                if (string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["CulqiApiKey"]))
                {
                    apiKey = ConfigurationManager.AppSettings["CulqiApiKey"];
                }

                return apiKey;
            }

            set
            {
                if (value != apiKey)
                {
                    CulqiClient = null;
                }

                apiKey = value;
            }
        }        

        public static JsonSerializerSettings SerializerSettings { get; set; } = DefaultSerializerSettings();

        public static ICulqiClient CulqiClient
        {
            get
            {
                if (culqiClient == null)
                {
                    culqiClient = BuildDefaultCulqiClient();
                }

                return culqiClient;
            }

            set => culqiClient = value;
        }

        public static string CulqiNetVersion { get; }

        private static JsonSerializerSettings DefaultSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new CulqiObjectConverter(),
                },
                DateParseHandling = DateParseHandling.None,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        private static CulqiClient BuildDefaultCulqiClient()
        {
            if (ApiKey != null && ApiKey.Length == 0)
            {
                var message = "Your Public API key is invalid, as it is an empty string. You can "
                    + "double-check your API key from the Culqi Dashboard. See "
                    + "https://www.culqi.com/api/#/autenticacion for details or contact support "
                    + "at https://culqi.zendesk.com/hc/es/requests/new if you have any questions.";
                throw new CulqiException(message);
            }

            if (ApiKey != null && StringUtils.ContainsWhitespace(ApiKey))
            {
                var message = "Your Public API key is invalid, as it contains whitespace. You can "
                    + "double-check your API key from the Culqi Dashboard. See "
                    + "https://www.culqi.com/api/#/autenticacion for details or contact support "
                    + "at https://culqi.zendesk.com/hc/es/requests/new if you have any questions.";
                throw new CulqiException(message);
            }

            return new CulqiClient(ApiKey, new CulqiHttpClient(httpClient: null));
        }
    }
}
