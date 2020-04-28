using Culqi.Entities.Interfaces;
using Culqi.Services.Base;
using Culqi.Services.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqi.Infrastructure.Interfaces
{
    public interface ICulqiClient
    {
        string ApiBase { get; }
        string ApiKey { get; }
        Task<T> RequestAsync<T>(
            HttpMethod method, 
            string path,
            BaseOptions options,
            RequestOptions requestOptions, 
            CancellationToken cancellationToken = default) where T : ICulqiEntity;
    }
}
