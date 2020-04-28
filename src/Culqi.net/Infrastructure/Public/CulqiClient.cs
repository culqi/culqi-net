using Culqi.Entities;
using Culqi.Entities.Base;
using Culqi.Entities.Interfaces;
using Culqi.Exceptions;
using Culqi.Infrastructure.Interfaces;
using Culqi.Services.Base;
using Culqi.Services.Common;
using Culqi.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqi.Infrastructure.Public
{
    public class CulqiClient : ICulqiClient
    {
        public CulqiClient(string apiKey = null, ICulqiHttpClient httpClient = null, string apiBase = null)
        {
            if (apiKey != null && apiKey.Length == 0)
            {
                throw new ArgumentException("API key cannot be the empty string.", nameof(apiKey));
            }

            if (apiKey != null && StringUtils.ContainsWhitespace(apiKey))
            {
                throw new ArgumentException("API key cannot contain whitespace.", nameof(apiKey));
            }

            ApiKey = apiKey;
            
            HttpClient = httpClient ?? BuildDefaultHttpClient();

            ApiBase = apiBase ?? ((ApiKey.StartsWith("pk")) ? "https://secure.culqi.com/v2" : DefaultApiBase);
        }

        public static string DefaultApiBase => "https://api.culqi.com/v2";
        public string ApiBase { get; }
        public string ApiKey { get; }
        public ICulqiHttpClient HttpClient { get; }

        public async Task<T> RequestAsync<T>(
            HttpMethod method, 
            string path, 
            BaseOptions options,
            RequestOptions requestOptions,
            CancellationToken cancellationToken = default) where T : ICulqiEntity
        {
            var request = new CulqiRequest(this, method, path, options, requestOptions);
            var response = await HttpClient.MakeRequestAsync(request, cancellationToken)
                .ConfigureAwait(false);
            return ProcessResponse<T>(response);
        }

        private static ICulqiHttpClient BuildDefaultHttpClient()
        {
            return new CulqiHttpClient();
        }

        private static T ProcessResponse<T>(CulqiResponse response) where T : ICulqiEntity
        {
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created )
            {
                throw BuildCulqiException(response);
            }

            T obj;
            try
            {
                obj = CulqiEntity.FromJson<T>(response.Content);
            }
            catch (Newtonsoft.Json.JsonException)
            {
                throw BuildInvalidResponseException(response);
            }

            obj.CulqiResponse = response;
            return obj;
        }

        private static CulqiException BuildCulqiException(CulqiResponse response)
        {
            CulqiError error = null;

            try
            {
                error = JsonConvert.DeserializeObject<CulqiError>(response.Content);
            }
            catch (Newtonsoft.Json.JsonException)
            {
                return BuildInvalidResponseException(response);
            }


            if (error == null)
            {
                return BuildInvalidResponseException(response);
            }

            var culqiError = CulqiError.FromJson(response.Content);

            culqiError.CulqiResponse = response;

            return new CulqiException(response.StatusCode, culqiError, culqiError.Message ?? culqiError.UserMessage)
            {
                CulqiResponse = response
            };
        }

        private static CulqiException BuildInvalidResponseException(CulqiResponse response)
        {
            return new CulqiException(response.StatusCode, null, $"Invalid response object from API: \"{response.Content}\"")
            {
                CulqiResponse = response,
            };
        }
    }
}
