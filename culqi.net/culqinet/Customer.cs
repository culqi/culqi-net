using System;
using System.Collections.Generic;
namespace culqi.net
{
	public class Customer : Generic
	{	
		const string URL = "/customers/";

        public Customer(Security security) : base(security, URL)
        {
  
        }

    }
}
