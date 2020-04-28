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
    public class CardService : Service<Card>,
        ICreatable<Card, CardCreateOptions>,
        IRetrievable<Card, CardGetOptions>,
        IListable<Card, CardListOptions>,
        IUpdatable<Card, CardUpdateOptions>,
        IDeletable<Card, CardDeleteOptions>
    {
        public CardService() : base(null)
        {
        }

        public CardService(ICulqiClient client) : base(client)
        {
        }

        protected override string BasePath => "/cards";

        public virtual Task<Card> Create(CardCreateOptions options, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return CreateEntity(options, requestOptions, cancellationToken);
        }

        public virtual Task<Card> Get(string cardId, CardGetOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return GetEntity(cardId, options, requestOptions, cancellationToken);
        }

        public virtual Task<CulqiList<Card>> List(CardListOptions listOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return ListEntities(listOptions, requestOptions, cancellationToken);
        }

        public virtual Task<Card> Update(string cardId, CardUpdateOptions updateOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return UpdateEntity(cardId, updateOptions, requestOptions, cancellationToken);
        }

        public virtual Task<Card> Delete(string cardId, CardDeleteOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return DeleteEntity(cardId, options, requestOptions, cancellationToken);
        }
    }
}
