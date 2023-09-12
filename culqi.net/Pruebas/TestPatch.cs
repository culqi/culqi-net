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
            string data = culqiCRUD.UpdatePlan().Content;

			var json_object = JObject.Parse(data);

			Assert.AreEqual("plan", (string)json_object["object"]);
		}


        [Test]
        public void Test02_UpdateOrder()
        {
            string data = culqiCRUD.UpdateOrder().Content;

            var json_object = JObject.Parse(data);

            Assert.AreEqual("order", (string)json_object["object"]);
        }


        [Test]
        public void Test03_UpdateCharge()
        {
            string data = culqiCRUD.UpdateCharge().Content;

            var json_object = JObject.Parse(data);

            Assert.AreEqual("charge", (string)json_object["object"]);
        }


        [Test]
        public void Test04_UpdateCard()
        {
            string data = culqiCRUD.UpdateCard().Content;

            var json_object = JObject.Parse(data);

            Assert.AreEqual("card", (string)json_object["object"]);
        }

    }
}