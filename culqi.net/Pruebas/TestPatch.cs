using System;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace culqi.net
{
    [TestFixture]
    public class TestPatch
    {
        CulqiCRUD culqiCRUD = new CulqiCRUD();
        Security security = null;


        [Test]
        public void Test01_UpdatePlan()
        {
            HttpResponseMessage data = culqiCRUD.UpdatePlan();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.AreEqual("plan", (string)json_object["object"]);
        }


        [Test]
        public void Test02_UpdateOrder()
        {
            HttpResponseMessage data = culqiCRUD.UpdateOrder();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.AreEqual("order", (string)json_object["object"]);
        }


        [Test]
        public void Test03_UpdateCharge()
        {
            HttpResponseMessage data = culqiCRUD.UpdateCharge();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.AreEqual("charge", (string)json_object["object"]);
        }


        [Test]
        public void Test04_UpdateCard()
        {
            HttpResponseMessage data = culqiCRUD.UpdateCard();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.AreEqual("card", (string)json_object["object"]);
        }

    }
}