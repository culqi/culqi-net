using Culqi.Entities.Interfaces;
using Culqi.Services.Base;
using Culqi.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqi.Services.Interfaces
{
    public interface ICreatable<TEntity, TOptions>where TEntity : ICulqiEntity where TOptions : BaseOptions, new()
    {
        Task<TEntity> Create(TOptions createOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);
    }
}
