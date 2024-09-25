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
        // dotnet test --filter FullyQualifiedName~TestDelete.Test01_DeleteSubscription 
        [Test]
        public void Test01_DeleteSubscription()
        {
            HttpResponseMessage data = culqiCRUD.CreateSubscription();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            HttpResponseMessage subscription = culqiCRUD.DeleteSubscription((string)json_object["id"]);
            var json_subscription = JObject.Parse(subscription.Content.ReadAsStringAsync().Result);
            Assert.True((bool)json_subscription["deleted"]);
        }

        // dotnet test --filter FullyQualifiedName~TestDelete.Test02_DeletePlan
        [Test]
        public void Test02_DeletePlan()
        {
            HttpResponseMessage data = culqiCRUD.CreatePlan();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            HttpResponseMessage plan = culqiCRUD.DeletePlan((string)json_object["id"]);
            var json_plan = JObject.Parse(plan.Content.ReadAsStringAsync().Result);
            Assert.True((bool)json_plan["deleted"]);
        }

        [Test]
        public void Test03_DeleteCard()
        {
            HttpResponseMessage data = culqiCRUD.CreateCard();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            HttpResponseMessage card = culqiCRUD.DeleteCard((string)json_object["id"]);
            var json_card = JObject.Parse(card.Content.ReadAsStringAsync().Result);
            Assert.True((bool)json_card["deleted"]);
        }

        [Test]
        public void Test04_DeleteCustomer()
        {
            HttpResponseMessage data = culqiCRUD.CreateCustomer();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            HttpResponseMessage customer = culqiCRUD.DeleteCustomer((string)json_object["id"]);
            var json_customer = JObject.Parse(customer.Content.ReadAsStringAsync().Result);
            Assert.True((bool)json_customer["deleted"]);
        }

    }
}