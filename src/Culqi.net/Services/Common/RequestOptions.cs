using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Services.Common
{
    public class RequestOptions
    {
        public string ApiKey { get; set; }
        internal string BaseUrl { get; set; }
        internal string CulqiVersion { get; set; }
    }
}
