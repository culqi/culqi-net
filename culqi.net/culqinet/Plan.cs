using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Plan : Generic
    {	
		const string URL = "/recurrent/plans/";

		public Plan(Security security) : base(security, URL)
        {
			
		}

	}
}
