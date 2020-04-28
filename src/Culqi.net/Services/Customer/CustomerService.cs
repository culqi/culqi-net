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
    public class CustomerService : Service<Customer>,
        ICreatable<Customer, CustomerCreateOptions>,
        IRetrievable<Customer, CustomerGetOptions>,
        IListable<Customer, CustomerListOptions>,
        IUpdatable<Customer, CustomerUpdateOptions>,
        IDeletable<Customer, CustomerDeleteOptions>
    {
        public CustomerService() : base(null)
        {
        }

        public CustomerService(ICulqiClient client) : base(client)
        {
        }

        protected override string BasePath => "/customers";

        public virtual Task<Customer> Create(CustomerCreateOptions options, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return CreateEntity(options, requestOptions, cancellationToken);
        }

        public virtual Task<Customer> Get(string customerId, CustomerGetOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return GetEntity(customerId, options, requestOptions, cancellationToken);
        }

        public virtual Task<CulqiList<Customer>> List(CustomerListOptions listOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return ListEntities(listOptions, requestOptions, cancellationToken);
        }

        public virtual Task<Customer> Update(string customerId, CustomerUpdateOptions updateOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return UpdateEntity(customerId, updateOptions, requestOptions, cancellationToken);
        }

        public virtual Task<Customer> Delete(string customerId, CustomerDeleteOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return DeleteEntity(customerId, options, requestOptions, cancellationToken);
        }
    }
}
