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
    public interface IUpdatable<TEntity, TOptions> where TEntity : ICulqiEntity, IHasId where TOptions : BaseOptions, new()
    {
        Task<TEntity> Update(string id, TOptions updateOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);
    }
}
