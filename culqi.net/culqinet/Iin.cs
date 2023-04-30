using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Iin : Generic
    {	
		const string URL = "/iins/";

		public Iin(Security security) : base(security, URL)
        {
		
		}

	}
}
