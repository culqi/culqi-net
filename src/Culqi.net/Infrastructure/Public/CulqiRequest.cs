using Culqi.Exceptions;
using Culqi.Infrastructure.FormEncoding;
using Culqi.Infrastructure.Interfaces;
using Culqi.Services.Base;
using Culqi.Services.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Culqi.Infrastructure.Public
{
    public class CulqiRequest
    {
        private readonly BaseOptions options;
        public CulqiRequest(ICulqiClient client, HttpMethod method, string path, BaseOptions options, RequestOptions requestOptions)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this.options = options;
            Method = method;
            Uri = BuildUri(client, method, path, options, requestOptions);
            AuthorizationHeader = BuildAuthorizationHeader(client, method, path);
        }

        public HttpMethod Method { get; }
        public Uri Uri { get; }
        public AuthenticationHeaderValue AuthorizationHeader { get; }
        public HttpContent Content => BuildContent(Method, options);

        public override string ToString()
        {
            return string.Format("<{0} Method={1} Uri={2}>", GetType().FullName, Method, Uri.ToString());
        }

        private static Uri BuildUri(ICulqiClient client, HttpMethod method, string path, BaseOptions options, RequestOptions requestOptions)
        {
            var b = new StringBuilder();

            b.Append(requestOptions?.BaseUrl ?? client.ApiBase);
            b.Append(path);

            if ((method != HttpMethod.Post) && (options != null))
            {
                var queryString = FormEncoder.CreateQueryString(options);
                if (!string.IsNullOrEmpty(queryString))
                {
                    b.Append("?");
                    b.Append(queryString);
                }
            }

            return new Uri(b.ToString());
        }

        private static AuthenticationHeaderValue BuildAuthorizationHeader(ICulqiClient client, HttpMethod method, string path)
        {
            if (client.ApiKey == null)
            {
                var message = "No API key provided. Set your public API key using "
                    + "`CulqiConfiguration.ApiKey = \"<API-KEY>\"`. You can generate API keys "
                    + "from the Culqi Dashboard. See "
                    + "https://www.culqi.com/api/#/autenticacion for details or contact support "
                    + "at https://culqi.zendesk.com/hc/es/requests/new if you have any questions.";
                throw new CulqiException(message);
            }

            return new AuthenticationHeaderValue("Bearer", client.ApiKey);
        }

        private static HttpContent BuildContent(HttpMethod method, BaseOptions options)
        {
            if (method != HttpMethod.Post && method.ToString() != "PATCH")
            {
                return null;
            }

            return FormEncoder.CreateHttpContent(options);
        }
    }
}
