using System;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace culqi.net
{	
	[TestFixture]
	public class TestAll
	{	
		Dictionary<string, object> filter = new Dictionary<string, object>
		{
			{"limit", 50}
		};

		Security security = null;

		public TestAll()
		{
			security = new Security();
            security.public_key = "pk_test_e94078b9b248675d";
            security.secret_key = "sk_test_c2267b5b262745f0";
        }

		[Test]
		public void Test01_AllTokens()
		{	
			string tokens = new Token(security).List(filter).Content;
			JObject json_tokens = JObject.Parse(tokens);
			List<Dictionary<string, object>>  data =  json_tokens["data"].ToObject<List<Dictionary<string, object>>>();
			bool valid = false;
			Console.WriteLine(data);
			if (data.Count >= 0)
			{
				valid = true;
			}
			Assert.True(valid);
		}

		[Test]
		public void Test02_AllCharges()
		{
			string charges = new Charge(security).List(filter).Content;
			JObject json_charges = JObject.Parse(charges);
			List<Dictionary<string, object>> data = json_charges["data"].ToObject<List<Dictionary<string, object>>>();
			bool valid = false;
			if (data.Count >= 0)
			{
				valid = true;
			}
			Assert.True(valid);
		}

        [Test]
        public void Test03_AllOrders()
        {
            string orders = new Order(security).List(filter).Content;
            JObject json_charges = JObject.Parse(orders);
            List<Dictionary<string, object>> data = json_charges["data"].ToObject<List<Dictionary<string, object>>>();
            bool valid = false;
            if (data.Count >= 0)
            {
                valid = true;
            }
            Assert.True(valid);
        }

        [Test]
		public void Test04_AllPlans()
		{
			string plans = new Plan(security).List(filter).Content;
			JObject json_plans = JObject.Parse(plans);
			List<Dictionary<string, object>> data = json_plans["data"].ToObject<List<Dictionary<string, object>>>();
			bool valid = false;
			if (data.Count >= 0)
			{
				valid = true;
			}
			Assert.True(valid);
		}

		[Test]
		public void Test05_AllSubscriptions()
		{
			string subscriptions = new Subscription(security).List(filter).Content;
			JObject json_subscriptions = JObject.Parse(subscriptions);
			List<Dictionary<string, object>> data = json_subscriptions["data"].ToObject<List<Dictionary<string, object>>>();
			bool valid = false;
			if (data.Count >= 0)
			{
				valid = true;
			}
			Assert.True(valid);
		}

		[Test]
		public void Test06_AllCards()
		{
			string cards = new Card(security).List(filter).Content;
			JObject json_cards = JObject.Parse(cards);
			List<Dictionary<string, object>> data = json_cards["data"].ToObject<List<Dictionary<string, object>>>();
			bool valid = false;
			if (data.Count >= 0)
			{
				valid = true;
			}
			Assert.True(valid);
		}

		[Test]
		public void Test07_AllCustomers()
		{
			string customers = new Customer(security).List(filter).Content;
			JObject json_customers = JObject.Parse(customers);
			List<Dictionary<string, object>> data = json_customers["data"].ToObject<List<Dictionary<string, object>>>();
			bool valid = false;
			if (data.Count >= 0)
			{
				valid = true;
			}
			Assert.True(valid);
		}

		[Test]
		public void Test08_AllTransfers()
		{
			string transfers = new Transfer(security).List(filter).Content;
			JObject json_transfers = JObject.Parse(transfers);
			List<Dictionary<string, object>> data = json_transfers["data"].ToObject<List<Dictionary<string, object>>>();
			bool valid = false;
			if (data.Count >= 0)
			{
				valid = true;
			}
			Assert.True(valid);
		}

		[Test]
		public void Test09_AllRefunds()
		{
			string refunds = new Refund(security).List(filter).Content;
			var json_refunds = JObject.Parse(refunds);
			List<Dictionary<string, object>> data = json_refunds["data"].ToObject<List<Dictionary<string, object>>>();
			bool valid = false;
			if (data.Count >= 0)
			{
				valid = true;
			}
			Assert.True(valid);
		}

	}
}