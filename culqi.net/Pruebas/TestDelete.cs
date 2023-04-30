using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace culqi.net
{	
	[TestFixture]
	public class TestDelete
	{

		CulqiCRUD culqiCRUD = new CulqiCRUD();

		
		// Eliminar Recursos

		[Test]
		public void Test01_DeleteSubscription()
		{
			string data = culqiCRUD.CreateSubscription();
			var json_object = JObject.Parse(data);
			string subscription = culqiCRUD.DeleteSubscription((string)json_object["id"]);
			var json_subscription = JObject.Parse(subscription);
			Assert.True((bool)json_subscription["deleted"]);
		}

		[Test]
		public void Test02_DeletePlan()
		{
			string data = culqiCRUD.CreatePlan();
			var json_object = JObject.Parse(data);
			string plan = culqiCRUD.DeletePlan((string)json_object["id"]);
			var json_plan = JObject.Parse(plan);
			Assert.True((bool)json_plan["deleted"]);
		}

		[Test]
		public void Test03_DeleteCard()
		{
			string data = culqiCRUD.CreateCard();
			var json_object = JObject.Parse(data);
			string card = culqiCRUD.DeleteCard((string)json_object["id"]);
			var json_card = JObject.Parse(card);
			Assert.True((bool)json_card["deleted"]);
		}

		[Test]
		public void Test04_DeleteCustomer()
		{
			string data = culqiCRUD.CreateCustomer();
			var json_object = JObject.Parse(data);
			string customer = culqiCRUD.DeleteCustomer((string)json_object["id"]);
			var json_customer = JObject.Parse(customer);
			Assert.True((bool)json_customer["deleted"]);
		}
		
    }
}
