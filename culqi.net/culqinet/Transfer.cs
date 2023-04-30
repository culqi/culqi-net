using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Transfer : Generic
    {	
		const string URL = "/transfers/";

		public Transfer(Security security) : base(security, URL)
        {
			
		}

	}
}
