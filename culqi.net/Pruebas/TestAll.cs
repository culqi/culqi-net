using System;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.Numerics;

namespace culqi.net
{
    [TestFixture]
    public class TestAll
    {
        Dictionary<string, object> filter = new Dictionary<string, object>
        {
            {"limit", 50},
            {"country_code", "PE"}
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
            HttpResponseMessage tokens = new Token(security).List(filter);

            JObject json_tokens = JObject.Parse(tokens.Content.ReadAsStringAsync().Result);
            List<Dictionary<string, object>> data = json_tokens["data"].ToObject<List<Dictionary<string, object>>>();
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
            HttpResponseMessage charges = new Charge(security).List(filter);
            JObject json_charges = JObject.Parse(charges.Content.ReadAsStringAsync().Result);
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
            HttpResponseMessage orders = new Order(security).List(filter);
            JObject json_charges = JObject.Parse(orders.Content.ReadAsStringAsync().Result);
            List<Dictionary<string, object>> data = json_charges["data"].ToObject<List<Dictionary<string, object>>>();
            bool valid = false;
            if (data.Count >= 0)
            {
                valid = true;
            }
            Assert.True(valid);
        }

        // dotnet test --filter FullyQualifiedName~TestAll.Test04_AllPlans
        [Test]
        public void Test04_AllPlans()
        {
            Dictionary<string, object> filterPlan = new Dictionary<string, object>
            {
                {"limit", 50},
                //{"min_amount", 300},
                //{"max_amount", 500000},
                //{"status", 1},
                //{"before", "pln_live_uGTYhNHIhndkeYbJ"},
                //{"after", "pln_live_uGTYhNHIhndkeYbx"}
            };
            HttpResponseMessage plans = new Plan(security).List(filterPlan);
            JObject json_plans = JObject.Parse(plans.Content.ReadAsStringAsync().Result);
            List<Dictionary<string, object>> data = json_plans["data"].ToObject<List<Dictionary<string, object>>>();
            bool valid = false;
            if (data.Count >= 0)
            {
                valid = true;
            }
            Assert.True(valid);
        }

        // dotnet test --filter FullyQualifiedName~TestAll.Test05_AllSubscriptions 
        [Test]
        public void Test05_AllSubscriptions()
        {
            Dictionary<string, object> filterSubscriptions = new Dictionary<string, object>
            {
                {"limit", 50},
                //{"plan_id", "pln_live_uGTYhNHIhndkeYbJ"},
                //{"status", 1},
                //{"before", "sxn_live_IijsfgIHFSNSTnsx"},
                //{"after", "sxn_live_IijsfgIHFSNSTnsf"},
            };
            HttpResponseMessage subscriptions = new Subscription(security).List(filterSubscriptions);
            JObject json_subscriptions = JObject.Parse(subscriptions.Content.ReadAsStringAsync().Result);
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
            HttpResponseMessage cards = new Card(security).List(filter);
            JObject json_cards = JObject.Parse(cards.Content.ReadAsStringAsync().Result);
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
            HttpResponseMessage customers = new Customer(security).List(filter);
            JObject json_customers = JObject.Parse(customers.Content.ReadAsStringAsync().Result);
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
            HttpResponseMessage transfers = new Transfer(security).List(filter);
            JObject json_transfers = JObject.Parse(transfers.Content.ReadAsStringAsync().Result);
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
            HttpResponseMessage refunds = new Refund(security).List(filter);
            var json_refunds = JObject.Parse(refunds.Content.ReadAsStringAsync().Result);
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
