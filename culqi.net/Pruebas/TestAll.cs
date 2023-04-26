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
			security.public_key = "pk_test_90667d0a57d45c48";
			security.secret_key = "sk_test_1573b0e8079863ff";
		}

		[Test]
		public void allTokens()
		{	
			string tokens = new Token(security).List(filter);
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
		public void allCharges()
		{
			string charges = new Charge(security).List(filter);
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
        public void allOrders()
        {
            string orders = new Order(security).List(filter);
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
		public void allPlans()
		{
			string plans = new Plan(security).List(filter);
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
		public void allSubscriptions()
		{
			string subscriptions = new Subscription(security).List(filter);
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
		public void allCards()
		{
			string cards = new Card(security).List(filter);
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
		public void allCustomers()
		{
			string customers = new Customer(security).List(filter);
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
		public void allTransfers()
		{
			string transfers = new Transfer(security).List(filter);
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
		public void allRefunds()
		{
			string refunds = new Refund(security).List(filter);
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
