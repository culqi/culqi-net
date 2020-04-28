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
    public class PlanService : Service<Plan>,
        ICreatable<Plan, PlanCreateOptions>,
        IRetrievable<Plan, PlanGetOptions>,
        IListable<Plan, PlanListOptions>,
        IUpdatable<Plan, PlanUpdateOptions>,
        IDeletable<Plan, PlanDeleteOptions>
    {
        public PlanService() : base(null)
        {
        }

        public PlanService(ICulqiClient client) : base(client)
        {
        }

        protected override string BasePath => "/plans";

        public virtual Task<Plan> Create(PlanCreateOptions options, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return CreateEntity(options, requestOptions, cancellationToken);
        }

        public virtual Task<Plan> Get(string planId, PlanGetOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return GetEntity(planId, options, requestOptions, cancellationToken);
        }

        public virtual Task<CulqiList<Plan>> List(PlanListOptions listOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return ListEntities(listOptions, requestOptions, cancellationToken);
        }

        public virtual Task<Plan> Update(string planId, PlanUpdateOptions updateOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return UpdateEntity(planId, updateOptions, requestOptions, cancellationToken);
        }

        public virtual Task<Plan> Delete(string planId, PlanDeleteOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return DeleteEntity(planId, options, requestOptions, cancellationToken);
        }
    }
}
