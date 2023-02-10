using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Iin
	{	
		const string URL = "/iins/";

		Security security { get; set; }

		public Iin(Security security)
		{
			this.security = security;
		}

		public string List(Dictionary<string, object> query_params)
		{
			return new Util().Request(query_params, URL, security.secret_key, "get");
		}

		public string Get(String id)
		{
			return new Util().Request(null, URL + id + "/", security.secret_key, "get");
		}

	}
}
