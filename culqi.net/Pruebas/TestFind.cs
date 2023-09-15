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
            HttpResponseMessage data = culqiCRUD.CreateToken();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            HttpResponseMessage token = culqiCRUD.GetToken((string)json_object["id"]);
            var json_token = JObject.Parse(token.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("token", (string)json_token["object"]);
        }

        [Test]
        public void Test02_FindOrder()
        {
            HttpResponseMessage data = culqiCRUD.CreateOrder();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            HttpResponseMessage order = culqiCRUD.GetOrder((string)json_object["id"]);
            var json_order = JObject.Parse(order.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("order", (string)json_order["object"]);
        }

        [Test]
        public void Test03_FindCharge()
        {
            HttpResponseMessage data = culqiCRUD.CreateCharge();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            HttpResponseMessage charge = culqiCRUD.GetCharge((string)json_object["id"]);
            var json_charge = JObject.Parse(charge.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("charge", (string)json_charge["object"]);
        }

        [Test]
        public void Test04_FindPlan()
        {
            HttpResponseMessage data = culqiCRUD.CreatePlan();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            HttpResponseMessage plan = culqiCRUD.GetPlan((string)json_object["id"]);
            var json_plan = JObject.Parse(plan.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("plan", (string)json_plan["object"]);
        }

        [Test]
        public void Test05_FindCustomer()
        {
            HttpResponseMessage data = culqiCRUD.CreateCustomer();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            HttpResponseMessage customer = culqiCRUD.GetCustomer((string)json_object["id"]);
            var json_customer = JObject.Parse(customer.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("customer", (string)json_customer["object"]);
        }

        [Test]
        public void Test06_FindCard()
        {
            HttpResponseMessage data = culqiCRUD.CreateCard();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            HttpResponseMessage card = culqiCRUD.GetCard((string)json_object["id"]);
            var json_card = JObject.Parse(card.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("card", (string)json_card["object"]);
        }

        [Test]
        public void Test07_FindSubscription()
        {
            HttpResponseMessage data = culqiCRUD.CreateSubscription();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            HttpResponseMessage subscrption = culqiCRUD.GetSubscription((string)json_object["id"]);
            var json_subscrption = JObject.Parse(subscrption.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("subscription", (string)json_subscrption["object"]);
        }

        [Test]
        public void Test08_FindRefund()
        {
            HttpResponseMessage data = culqiCRUD.CreateRefund();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            HttpResponseMessage refund = culqiCRUD.GetRefund((string)json_object["id"]);
            var json_refund = JObject.Parse(refund.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("refund", (string)json_refund["object"]);
        }

    }
}