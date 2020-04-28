using Culqi.Entities;
using Culqi.Infrastructure.Interfaces;
using Culqi.Services.Base;
using Culqi.Services.Common;
using Culqi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqi
{
    public class TokenService : Service<Token>,
        ICreatable<Token, TokenCreateOptions>, 
        IRetrievable<Token, TokenGetOptions>, 
        IListable<Token, TokenListOptions>,
        IUpdatable<Token, TokenUpdateOptions>
    {
        public TokenService() : base(null)
        {
        }

        public TokenService(ICulqiClient client) : base(client)
        {
        }

        protected override string BasePath => "/tokens";

        public virtual Task<Token> Create(TokenCreateOptions options, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return CreateEntity(options, requestOptions, cancellationToken);
        }

        public virtual Task<Token> Get(string tokenId, TokenGetOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return GetEntity(tokenId, options, requestOptions, cancellationToken);
        }

        public virtual Task<CulqiList<Token>> List(TokenListOptions listOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return ListEntities(listOptions, requestOptions, cancellationToken);
        }

        public virtual Task<Token> Update(string tokenId, TokenUpdateOptions updateOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return UpdateEntity(tokenId, updateOptions, requestOptions, cancellationToken);
        }
    }
}
