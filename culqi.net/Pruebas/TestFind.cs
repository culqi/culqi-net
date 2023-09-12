using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace culqi.net
{	
	[TestFixture]
	public class TestFind
	{
        
		CulqiCRUD culqiCRUD = new CulqiCRUD();


        [Test]
        public void Test01_FindToken()
        {
            string data = culqiCRUD.CreateToken().Content;
            var json_object = JObject.Parse(data);
            string token = culqiCRUD.GetToken((string)json_object["id"]).Content;
            var json_token = JObject.Parse(token);
            Assert.AreEqual("token", (string)json_token["object"]);
        }

        [Test]
        public void Test02_FindOrder()
        {
            string data = culqiCRUD.CreateOrder().Content;
            var json_object = JObject.Parse(data);
            string order = culqiCRUD.GetOrder((string)json_object["id"]).Content;
            var json_order = JObject.Parse(order);
            Assert.AreEqual("order", (string)json_order["object"]);
        }

        [Test]
        public void Test03_FindCharge()
        {
            string data = culqiCRUD.CreateCharge().Content;
            var json_object = JObject.Parse(data);
            string charge = culqiCRUD.GetCharge((string)json_object["id"]).Content;
            var json_charge = JObject.Parse(charge);
            Assert.AreEqual("charge", (string)json_charge["object"]);
        }

        [Test]
        public void Test04_FindPlan()
        {
            string data = culqiCRUD.CreatePlan().Content;
            var json_object = JObject.Parse(data);
            string plan = culqiCRUD.GetPlan((string)json_object["id"]).Content;
            var json_plan = JObject.Parse(plan);
            Assert.AreEqual("plan", (string)json_plan["object"]);
        }

        [Test]
        public void Test05_FindCustomer()
        {
            string data = culqiCRUD.CreateCustomer().Content;
            var json_object = JObject.Parse(data);
            string customer = culqiCRUD.GetCustomer((string)json_object["id"]).Content;
            var json_customer = JObject.Parse(customer);
            Assert.AreEqual("customer", (string)json_customer["object"]);
        }

        [Test]
        public void Test06_FindCard()
        {
            string data = culqiCRUD.CreateCard().Content;
            var json_object = JObject.Parse(data);
            string card = culqiCRUD.GetCard((string)json_object["id"]).Content;
            var json_card = JObject.Parse(card);
            Assert.AreEqual("card", (string)json_card["object"]);
        }

        [Test]
        public void Test07_FindSubscription()
        {
            string data = culqiCRUD.CreateSubscription().Content;
            var json_object = JObject.Parse(data);
            string subscrption = culqiCRUD.GetSubscription((string)json_object["id"]).Content;
            var json_subscrption = JObject.Parse(subscrption);
            Assert.AreEqual("subscription", (string)json_subscrption["object"]);
        }

        [Test]
        public void Test08_FindRefund()
        {
            string data = culqiCRUD.CreateRefund().Content;
            var json_object = JObject.Parse(data);
            string refund = culqiCRUD.GetRefund((string)json_object["id"]).Content;
            var json_refund = JObject.Parse(refund);
            Assert.AreEqual("refund", (string)json_refund["object"]);
        }
        
    }
}