using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Plan : Generic
    {	
		const string URL = "/plans/";

		public Plan(Security security) : base(security, URL)
        {
			
		}

	}
}
