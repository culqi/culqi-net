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

        // dotnet test --filter FullyQualifiedName~TestPatch.Test01_UpdatePlan
        [Test]
        public void Test01_UpdatePlan()
        {
            HttpResponseMessage data = culqiCRUD.UpdatePlan();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(json_object.ContainsKey("id"));
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

        // dotnet test --filter FullyQualifiedName~TestPatch.Test05_UpdateSubscription
        [Test]
        public void Test05_UpdateSubscription()
        {
            HttpResponseMessage data = culqiCRUD.UpdateSubscription();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(json_object.ContainsKey("id"));

        }
    }
}