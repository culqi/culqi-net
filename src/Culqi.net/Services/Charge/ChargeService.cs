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
    public class ChargeService : Service<Charge>,
        ICreatable<Charge, ChargeCreateOptions>,
        IListable<Charge, ChargeListOptions>,
        IRetrievable<Charge, ChargeGetOptions>,
        IUpdatable<Charge, ChargeUpdateOptions>
    {

        public ChargeService() : base(null)
        {
        }

        public ChargeService(ICulqiClient client) : base(client)
        {
        }

        protected override string BasePath => "/charges";


        public virtual Task<Charge> Capture(string chargeId, ChargeCaptureOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return Request(HttpMethod.Post, $"{InstanceUrl(chargeId)}/capture", options, requestOptions, cancellationToken);
        }

        public virtual Task<Charge> Create(ChargeCreateOptions options, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return CreateEntity(options, requestOptions, cancellationToken);
        }

        public virtual Task<Charge> Get(string chargeId, ChargeGetOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return GetEntity(chargeId, options, requestOptions, cancellationToken);
        }

        public virtual Task<CulqiList<Charge>> List(ChargeListOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return ListEntities(options, requestOptions, cancellationToken);
        }

        public virtual Task<Charge> Update(string chargeId, ChargeUpdateOptions options, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return UpdateEntity(chargeId, options, requestOptions, cancellationToken);
        }
    }
}
