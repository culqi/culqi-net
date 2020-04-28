using Culqi.Entities;
using Culqi.Infrastructure.Interfaces;
using Culqi.Services.Base;
using Culqi.Services.Common;
using Culqi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqi
{
    public class OrderService : Service<Order>,
        ICreatable<Order, OrderCreateOptions>,
        IRetrievable<Order, OrderGetOptions>,
        IListable<Order, OrderListOptions>,
        IUpdatable<Order, OrderUpdateOptions>,
        IDeletable<Order, OrderDeleteOptions>
    {
        public OrderService() : base(null)
        {
        }

        public OrderService(ICulqiClient client) : base(client)
        {
        }

        protected override string BasePath => "/orders";

        public virtual Task<Order> Create(OrderCreateOptions options, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return CreateEntity(options, requestOptions, cancellationToken);
        }

        public virtual Task<Order> Confirm(string orderId, OrderConfirmOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return Request(HttpMethod.Post, $"{InstanceUrl(orderId)}/confirm", options, requestOptions, cancellationToken);
        }

        public virtual Task<Order> Get(string planId, OrderGetOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return GetEntity(planId, options, requestOptions, cancellationToken);
        }

        public virtual Task<CulqiList<Order>> List(OrderListOptions listOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return ListEntities(listOptions, requestOptions, cancellationToken);
        }

        public virtual Task<Order> Update(string planId, OrderUpdateOptions updateOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return UpdateEntity(planId, updateOptions, requestOptions, cancellationToken);
        }

        public virtual Task<Order> Delete(string planId, OrderDeleteOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return DeleteEntity(planId, options, requestOptions, cancellationToken);
        }       
    }
}
