using System;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace culqi.net
{	
	[TestFixture]
	public class TestPatch
	{	
		Security security = null;

		public TestPatch()
		{
			security = new Security();
			security.secret_key = "sk_test_1573b0e8079863ff";
		}

		protected string UpdatePlan()
		{
			Dictionary<string, object> metadata = new Dictionary<string, object>
			{
				{"abc", "555"}
			};

			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"metadata", metadata}
			};

			return new Plan(security).Update(map, "pln_test_Dx1bWsxGLiVMrTxJ");
		}

		[Test]
		public void ValidCreatePlan()
		{
			string data = UpdatePlan();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("plan", (string)json_object["object"]);
		}

        protected string UpdateOrder()
        {
            Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"abc", "555"}
            };

            Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"metadata", metadata}
            };

            return new Order(security).Update(map, "ord_test_uHb4ntzB9aK9NfqV");
        }

        [Test]
        public void ValidCreateOrder()
        {
            string data = UpdateOrder();

            var json_object = JObject.Parse(data);

            Assert.AreEqual("order", (string)json_object["object"]);
        }

        protected string UpdateCharge()
        {
            Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"abc", "555"}
            };

            Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"metadata", metadata}
            };

            return new Charge(security).Update(map, "chr_test_nTczF6ruLlo6YDMR");
        }

        [Test]
        public void ValidCreateCharge()
        {
            string data = UpdateCharge();

            var json_object = JObject.Parse(data);

            Assert.AreEqual("charge", (string)json_object["object"]);
        }

        protected string UpdateCard()
        {
            Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"abc", "555"}
            };

            Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"metadata", metadata}
            };

            return new Card(security).Update(map, "crd_test_i5TeGDwm6D3lI3Jr");
        }

        [Test]
        public void ValidCreateCard()
        {
            string data = UpdateCard();

            var json_object = JObject.Parse(data);

            Assert.AreEqual("card", (string)json_object["object"]);
        }

    }
}
