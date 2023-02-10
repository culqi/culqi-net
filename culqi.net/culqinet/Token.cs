using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Token
	{
		const string URL = "/tokens/";

		Security security { get; set; }

		public Token(Security security)
		{
			this.security = security;
		}

		public string List(Dictionary<string, object> query_params)
		{
			return new Util().Request(query_params, URL, security.secret_key, "get");
		}

		public string Create(Dictionary<string, object> body)
		{
			return new Util().Request(body, URL, security.public_key, "post");
		}

        public string CreateYape(Dictionary<string, object> body)
        {
            return new Util().Request(body, URL+"yape", security.public_key, "post");
        }

        public string Get(String id)
		{
			return new Util().Request(null, URL + id + "/", security.secret_key, "get");
		}

	}
}
