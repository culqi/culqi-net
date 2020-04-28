using Culqi.Infrastructure.Public;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culqi.Infrastructure.Interfaces
{
    public interface ICulqiHttpClient
    {
        Task<CulqiResponse> MakeRequestAsync(CulqiRequest request, CancellationToken cancellationToken = default);
    }
}
