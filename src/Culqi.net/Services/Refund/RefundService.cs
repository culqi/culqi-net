using Culqi.Entities;
using Culqi.Infrastructure.Interfaces;
using Culqi.Services.Base;
using Culqi.Services.Common;
using Culqi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqi
{
    public class RefundService : Service<Refund>,
        ICreatable<Refund, RefundCreateOptions>,
        IRetrievable<Refund, RefundGetOptions>,
        IListable<Refund, RefundListOptions>,
        IUpdatable<Refund, RefundUpdateOptions>
    {
        public RefundService() : base(null)
        {
        }

        public RefundService(ICulqiClient client) : base(client)
        {
        }

        protected override string BasePath => "/refunds";

        public virtual Task<Refund> Create(RefundCreateOptions options, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return CreateEntity(options, requestOptions, cancellationToken);
        }

        public virtual Task<Refund> Get(string refundId, RefundGetOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return GetEntity(refundId, options, requestOptions, cancellationToken);
        }

        public virtual Task<CulqiList<Refund>> List(RefundListOptions listOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return ListEntities(listOptions, requestOptions, cancellationToken);
        }

        public virtual Task<Refund> Update(string refundId, RefundUpdateOptions updateOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return UpdateEntity(refundId, updateOptions, requestOptions, cancellationToken);
        }
    }
}
