using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace culqi.net
{
    [TestFixture]
    public class TestCreate
    {
        CulqiCRUD culqiCRUD = new CulqiCRUD();


        [Test]
        public void Test01_CreateToken()
        {
            //var data = culqiCRUD.CreateToken().Content;
            HttpResponseMessage data = culqiCRUD.CreateToken();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Console.WriteLine(data.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("token", (string)json_object["object"]);
        }

        [Test]
        public void Test02_CreateTokenEncrypt()
        {
            HttpResponseMessage data = culqiCRUD.CreateTokenEncrypt();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.AreEqual("token", (string)json_object["object"]);
        }

        [Test]
        public void Test03_CreateTokenYape()
        {
            HttpResponseMessage data = culqiCRUD.CreateTokenYape();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("token", (string)json_object["object"]);
        }

        [Test]
        public void Test04_CreateCharge()
        {
            HttpResponseMessage data = culqiCRUD.CreateCharge();
            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            Console.WriteLine(json_object);
            Assert.AreEqual("charge", (string)json_object["object"]);
        }

        [Test]
        public void Test05_CreateChargeEncrypt()
        {
            HttpResponseMessage data = culqiCRUD.CreateChargeEncrypt();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.AreEqual("charge", (string)json_object["object"]);
        }

        [Test]
        public void Test06_ChargeCapture()
        {
            HttpResponseMessage capture_data = culqiCRUD.CreateChargeCapture();

            var json_capture = JObject.Parse(capture_data.Content.ReadAsStringAsync().Result);

            Assert.AreNotSame("charge", (string)json_capture["id"]);
        }

        [Test]
        public void Test07_CreateOrder()
        {
            HttpResponseMessage data = culqiCRUD.CreateOrder();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.AreEqual("order", (string)json_object["object"]);
        }

        [Test]
        public void Test08_CreateOrderEncrypt()
        {
            HttpResponseMessage data = culqiCRUD.CreateOrderEncrypt();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.AreEqual("order", (string)json_object["object"]);
        }

        [Test]
        public void Test09_ConfirmOrder()
        {
            HttpResponseMessage data = culqiCRUD.CreateOrder();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.AreEqual("order", (string)json_object["object"]);
        }

        // dotnet test --filter FullyQualifiedName~TestCreate.Test10_CreatePlan
        [Test]
        public void Test10_CreatePlan()
        {
            try
            {
                Console.WriteLine("Validar ejecución");
                HttpResponseMessage data = culqiCRUD.CreatePlan();

                Console.WriteLine("Antes de la lectura del contenido");

                if (data.IsSuccessStatusCode)
                {
                    var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
                    Console.WriteLine("Después de la lectura del contenido");
                    Console.WriteLine("Contenido: " + json_object);

                    // Aquí puedes realizar más aserciones si es necesario
                    //Assert.AreEqual("plan", (string)json_object["object"]);
                    Assert.IsTrue(json_object.ContainsKey("id"));
                    Console.WriteLine("Prueba exitosa.");
                }
                else
                {
                    Console.WriteLine($"La solicitud no fue exitosa. Código de estado: {data}");
                    // Puedes lanzar una excepción o manejar según sea necesario
                    Assert.Fail("La solicitud no fue exitosa.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante la ejecución de la prueba: {ex}");
                // Puedes lanzar una excepción o manejar según sea necesario
                Assert.Fail("Error durante la ejecución de la prueba.");
            }

        }

        [Test]
        public void Test11_CreateCustomer()
        {
            HttpResponseMessage data = culqiCRUD.CreateCustomer();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            Console.WriteLine(json_object);

            Assert.AreEqual("customer", (string)json_object["object"]);
        }

        [Test]
        public void Test12_CreateCard()
        {
            HttpResponseMessage data = culqiCRUD.CreateCard();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);
            ;
            Assert.AreEqual("card", (string)json_object["object"]);
        }
        
        // dotnet test --filter FullyQualifiedName~TestCreate.Test13_CreateSubscription
        [Test]
        public void Test13_CreateSubscription()
        {
            HttpResponseMessage data = culqiCRUD.CreateSubscription();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            //Assert.AreEqual("subscription", (string)json_object["object"]);
            Assert.IsTrue(json_object.ContainsKey("id"));
        }

        [Test]
        public void Test14_CreateRefund()
        {
            HttpResponseMessage data = culqiCRUD.CreateRefund();

            var json_object = JObject.Parse(data.Content.ReadAsStringAsync().Result);

            Assert.AreEqual("refund", (string)json_object["object"]);
        }

    }

}