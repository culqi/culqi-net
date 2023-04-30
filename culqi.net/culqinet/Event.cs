using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Event : Generic
    {	
		const string URL = "/events/";

		public Event(Security security) : base(security, URL)
        {
			
		}

	}
}
