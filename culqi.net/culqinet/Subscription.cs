using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Subscription : Generic
    {	

		const string URL = "/recurrent/subscriptions/";

		public Subscription(Security security) : base(security, URL)
        {
			
		}

	}
}
