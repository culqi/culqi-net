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
    public class SubscriptionService : Service<Subscription>,
        ICreatable<Subscription, SubscriptionCreateOptions>,
        IRetrievable<Subscription, SubscriptionGetOptions>,
        IListable<Subscription, SubscriptionListOptions>,
        IUpdatable<Subscription, SubscriptionUpdateOptions>,
        IDeletable<Subscription, SubscriptionDeleteOptions>
    {
        public SubscriptionService() : base(null)
        {
        }

        public SubscriptionService(ICulqiClient client) : base(client)
        {
        }

        protected override string BasePath => "/subscriptions";

        public virtual Task<Subscription> Create(SubscriptionCreateOptions options, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return CreateEntity(options, requestOptions, cancellationToken);
        }

        public virtual Task<Subscription> Get(string subscriptionId, SubscriptionGetOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return GetEntity(subscriptionId, options, requestOptions, cancellationToken);
        }

        public virtual Task<CulqiList<Subscription>> List(SubscriptionListOptions listOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return ListEntities(listOptions, requestOptions, cancellationToken);
        }

        public virtual Task<Subscription> Update(string subscriptionId, SubscriptionUpdateOptions updateOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return UpdateEntity(subscriptionId, updateOptions, requestOptions, cancellationToken);
        }

        public virtual Task<Subscription> Delete(string subscriptionId, SubscriptionDeleteOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return DeleteEntity(subscriptionId, options, requestOptions, cancellationToken);
        }
    }
}
