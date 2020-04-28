using Culqi.Entities;
using Culqi.Entities.Interfaces;
using Culqi.Infrastructure.Interfaces;
using Culqi.Services.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
#if !NET45
using System.Runtime.CompilerServices;
#endif
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqi.Services.Base
{
    public abstract class Service<TEntityReturned> where TEntityReturned : ICulqiEntity
    {
        private ICulqiClient client;

        protected Service()
        {
        }

        protected Service(ICulqiClient client)
        {
            this.client = client;
        }

        protected abstract string BasePath { get; }

        protected virtual string BaseUrl => Client.ApiBase;

        protected ICulqiClient Client
        {
            get => client ?? CulqiConfiguration.CulqiClient;
            set => client = value;
        }

        protected Task<TEntityReturned> CreateEntity(BaseOptions options, RequestOptions requestOptions, CancellationToken cancellationToken)
        {
            return Request(HttpMethod.Post, ClassUrl(), options, requestOptions, cancellationToken);
        }
        protected Task<TEntityReturned> DeleteEntity(string id, BaseOptions options, RequestOptions requestOptions, CancellationToken cancellationToken)
        {
            return Request(HttpMethod.Delete, InstanceUrl(id), options, requestOptions, cancellationToken);
        }       

        protected Task<TEntityReturned> GetEntity(string id, BaseOptions options, RequestOptions requestOptions, CancellationToken cancellationToken)
        {
            return Request(HttpMethod.Get, InstanceUrl(id), options, requestOptions, cancellationToken);
        }

        protected Task<CulqiList<TEntityReturned>> ListEntities(ListOptions options, RequestOptions requestOptions, CancellationToken cancellationToken)
        {
            return Request<CulqiList<TEntityReturned>>(HttpMethod.Get, ClassUrl(), options, requestOptions, cancellationToken);
        }     

        protected Task<TEntityReturned> UpdateEntity(string id, BaseOptions options, RequestOptions requestOptions, CancellationToken cancellationToken)
        {
            return Request(new HttpMethod("PATCH"), InstanceUrl(id), options, requestOptions, cancellationToken);
        }

        protected Task<TEntityReturned> Request(HttpMethod method, string path, BaseOptions options, RequestOptions requestOptions, CancellationToken cancellationToken = default)
        {
            return Request<TEntityReturned>(method, path, options, requestOptions, cancellationToken);
        }

        protected async Task<T> Request<T>(HttpMethod method, string path, BaseOptions options, RequestOptions requestOptions, CancellationToken cancellationToken = default) where T : ICulqiEntity
        {
            requestOptions = SetupRequestOptions(requestOptions);
            return await Client.RequestAsync<T>(method, path, options, requestOptions, cancellationToken).ConfigureAwait(false);
        }        
        protected RequestOptions SetupRequestOptions(RequestOptions requestOptions)
        {
            if (requestOptions == null)
            {
                requestOptions = new RequestOptions();
            }

            requestOptions.BaseUrl = requestOptions.BaseUrl ?? BaseUrl;

            return requestOptions;
        }

        protected virtual string ClassUrl()
        {
            return BasePath;
        }
        protected virtual string InstanceUrl(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(
                    "The resource ID cannot be null or whitespace.",
                    nameof(id));
            }

            return $"{ClassUrl()}/{WebUtility.UrlEncode(id)}";
        }
    }
}
