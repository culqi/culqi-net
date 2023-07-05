using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace culqi.net
{	
	[TestFixture]
	public class TestCreate
	{
		CulqiCRUD culqiCRUD = new CulqiCRUD();


		[Test]
		public void Test01_CreateToken()
		{
			string data = culqiCRUD.CreateToken().body;

			var json_object = JObject.Parse(data);

			Assert.AreEqual("token",(string)json_object["object"]);
		}

        [Test]
        public void Test02_CreateTokenEncrypt()
        {
            string data = culqiCRUD.CreateTokenEncrypt().body;

            var json_object = JObject.Parse(data);

            Assert.AreEqual("token", (string)json_object["object"]);
        }
        
        [Test]
        public void Test03_CreateTokenYape()
        {
            string data = culqiCRUD.CreateTokenYape().body;
            var json_object = JObject.Parse(data);
            Assert.AreEqual("token", (string)json_object["object"]);
        }

		[Test]
		public void Test04_CreateCharge()
		{
			string data = culqiCRUD.CreateCharge().body;

			var json_object = JObject.Parse(data);
            Assert.AreEqual("charge", (string)json_object["object"]);
		}

        [Test]
        public void Test05_CreateChargeEncrypt()
        {
            string data = culqiCRUD.CreateChargeEncrypt().body;

            var json_object = JObject.Parse(data);

            Assert.AreEqual("charge", (string)json_object["object"]);
        }

        [Test]
        public void Test06_ChargeCapture()
        {
            string capture_data = culqiCRUD.CreateChargeCapture().body;

            var json_capture = JObject.Parse(capture_data);

            Assert.AreNotSame("charge", (string)json_capture["id"]);
        }

        [Test]
        public void Test07_CreateOrder()
        {
            string data = culqiCRUD.CreateOrder().body;

            var json_object = JObject.Parse(data);

            Assert.AreEqual("order", (string)json_object["object"]);
        }

        [Test]
        public void Test08_CreateOrderEncrypt()
        {
            string data = culqiCRUD.CreateOrderEncrypt().body;

            var json_object = JObject.Parse(data);

            Assert.AreEqual("order", (string)json_object["object"]);
        }

        [Test]
        public void Test09_ConfirmOrder()
        {
            string data = culqiCRUD.CreateOrder().body;

            var json_object = JObject.Parse(data);

            Assert.AreEqual("order", (string)json_object["object"]);
        }

        [Test]
		public void Test10_CreatePlan()
		{
			string data = culqiCRUD.CreatePlan().body;

			var json_object = JObject.Parse(data);

			Assert.AreEqual("plan", (string)json_object["object"]);
		}

		[Test]
		public void Test11_CreateCustomer()
		{
			string data = culqiCRUD.CreateCustomer().body;

			var json_object = JObject.Parse(data);

			Assert.AreEqual("customer", (string)json_object["object"]);
		}

		[Test]
		public void Test12_CreateCard()
		{
			string data = culqiCRUD.CreateCard().body;

			var json_object = JObject.Parse(data);
;
            Assert.AreEqual("card", (string)json_object["object"]);
		}

		[Test]
		public void Test13_CreateSubscription()
		{
			string data = culqiCRUD.CreateSubscription().body;

			var json_object = JObject.Parse(data);

			Assert.AreEqual("subscription", (string)json_object["object"]);
		}

        [Test]
        public void Test14_CreateRefund()
        {
            string data = culqiCRUD.CreateRefund().body;

            var json_object = JObject.Parse(data);

            Assert.AreEqual("refund", (string)json_object["object"]);
        }
		
    }

}
