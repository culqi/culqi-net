using Culqi.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqi.Infrastructure.Public
{
    public class CulqiHttpClient : ICulqiHttpClient
    {

        private static readonly Lazy<HttpClient> LazyDefaultHttpClient = new Lazy<HttpClient>(BuildDefaultSystemNetHttpClient);
        private readonly HttpClient httpClient;

        public CulqiHttpClient(HttpClient httpClient = null)
        {
#if NET45
            // With .NET Framework 4.5, it's necessary to manually enable support for TLS 1.2.
            ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol |
                SecurityProtocolType.Tls12;
#endif

            this.httpClient = httpClient ?? LazyDefaultHttpClient.Value;
        }

        public static TimeSpan DefaultHttpTimeout => TimeSpan.FromSeconds(80);

        public static HttpClient BuildDefaultSystemNetHttpClient()
        {
            return new HttpClient
            {
                Timeout = DefaultHttpTimeout,
            };
        }

        public async Task<CulqiResponse> MakeRequestAsync(CulqiRequest request, CancellationToken cancellationToken = default)
        {
            Exception requestException = null;
            HttpResponseMessage response = null;
            requestException = null;

            var httpRequest = BuildRequestMessage(request);

            try
            {
                response = await httpClient.SendAsync(httpRequest, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (HttpRequestException exception)
            {
                requestException = exception;
            }
            catch (OperationCanceledException exception)
                when (!cancellationToken.IsCancellationRequested)
            {
                requestException = exception;
            }

            if (requestException != null)
            {
                throw requestException;
            }

            string contentResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new CulqiResponse(response.StatusCode, response.Headers, contentResponse);
        }

        private HttpRequestMessage BuildRequestMessage(CulqiRequest request)
        {
            var requestMessage = new HttpRequestMessage(request.Method, request.Uri);
            requestMessage.Headers.Authorization = request.AuthorizationHeader;
            requestMessage.Content = request.Content;
            return requestMessage;
        }
    }
}
