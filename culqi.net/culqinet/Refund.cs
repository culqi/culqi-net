using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Refund : Generic
    {	

		const string URL = "/refunds/";

		public Refund(Security security) : base(security, URL)
        {
			
		}
	}
}
