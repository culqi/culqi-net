using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Card : Generic
    {	
		const string URL = "/cards/";

		public Card(Security security) : base(security, URL)
		{
			
		}
	}
}
