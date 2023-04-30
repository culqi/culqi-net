using System;
using System.Collections.Generic;
using culqinet.util;
using Newtonsoft.Json;

namespace culqi.net
{
	public class Token : Generic
	{
		const string URL = "/tokens/";

        public Token (Security security) : base(security, URL)
        {
           
        }

    }
}
