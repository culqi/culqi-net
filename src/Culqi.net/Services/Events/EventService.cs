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

namespace Culqi.Services.Events
{
    public class EventService : Service<Event>,
        IListable<Event, EventListOptions>,
        IRetrievable<Event, EventGetOptions>
    {
        public EventService() : base(null)
        {
        }

        public EventService(ICulqiClient client) : base(client)
        {
        }

        protected override string BasePath => "/events";

        public virtual Task<Event> Get(string eventId, EventGetOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return GetEntity(eventId, options, requestOptions, cancellationToken);
        }

        public virtual Task<CulqiList<Event>> List(EventListOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return ListEntities(options, requestOptions, cancellationToken);
        }
    }
}
