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
		Security security = null;

        public static int GetRandomNumber()
		{
			using (RNGCryptoServiceProvider rngCrypt = new RNGCryptoServiceProvider())
			{
                byte[] tokenBuffer = new byte[6];       // `int32` takes 4 bytes in C#
                rngCrypt.GetBytes(tokenBuffer);
                return BitConverter.ToInt32(tokenBuffer, 0);
            }
        }

        public TestCreate()
		{
			security = new Security();
			security.public_key = "pk_live_889113cd74ecfc55";
			security.secret_key = "sk_live_34a07dcb6d4c7e39";
			security.rsa_id = "508fc232-0a9d-4fc0-a192-364a0b782b89";
			security.rsa_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDYp0451xITpczkBrl5Goxkh7m1oynj8eDHypIn7HmbyoNJd8cS4OsT850hIDBwYmFuwmxF1YAJS8Cd2nes7fjCHh+7oNqgNKxM2P2NLaeo4Uz6n9Lu4KKSxTiIT7BHiSryC0+Dic91XLH7ZTzrfryxigsc+ZNndv0fQLOW2i6OhwIDAQAB";
        }

		protected static string GetRandomString()
		{
			string path = Path.GetRandomFileName();
			path = path.Replace(".", "");
			return path;
		}

		protected string CreateToken()
		{	
			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"card_number", "4557880621568322"},
				{"cvv", "111"},
				{"expiration_month", "11"},
				{"expiration_year", "2026"},
				{"email", "test.culqi@culqi.com"}
			};
			return new Token(security).Create(map);
		}

		[Test]
		public void ValidCreateToken()
		{
			string data = CreateToken();

			var json_object = JObject.Parse(data);
			Console.WriteLine(json_object);

			Assert.AreEqual("token",(string)json_object["object"]);
		}

        protected string CreateTokenEncrypt()
        {
            Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"card_number", "4557880621568322"},
                {"cvv", "111"},
                {"expiration_month", "11"},
                {"expiration_year", "2026"},
                {"email", "jordan.diaz@culqi.com"}
            };
            return new Token(security).Create(map, security.rsa_id, security.rsa_key);
        }

        [Test]
        public void ValidCreateTokenEncrypt()
        {
            string data = CreateTokenEncrypt();

            var json_object = JObject.Parse(data);
            Console.WriteLine(json_object);

            Assert.AreEqual("token", (string)json_object["object"]);
        }

        protected string CreateTokenYape()
        {
            Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"amount", "36200"},
                {"fingerprint", "86d3c875769bf62b0471b47853bfda77"},
                {"number_phone", "900000001"},
                {"otp", "425251"}
            };
            return new Token(security).CreateYape(map);
        }

        [Test]
        public void ValidCreateTokenYape()
        {
            string data = CreateTokenYape();
            var json_object = JObject.Parse(data);
            Assert.AreEqual("token", (string)json_object["object"]);
        }

        protected string CreateCharge()
		{	

			string data = CreateToken();

			var json_object = JObject.Parse(data);

			Dictionary<string, object> metadata = new Dictionary<string, object>
			{
				{"order_id", "777"}
			};

			Dictionary<string, object> map = new Dictionary<string, object>
			{	
				{"amount", 1000},
				{"capture", true},
				{"currency_code", "PEN"},
				{"description", "Venta de prueba"},
				{"email", "wmuro@me.com"},
				{"installments", 0},
				{"metadata", metadata},
				{"source_id", (string)json_object["id"]}
			};

			return new Charge(security).Create(map);

		}

		[Test]
		public void ValidCreateCharge()
		{
			string data = CreateCharge();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("charge", (string)json_object["object"]);
		}

        protected string CreateChargeEncrypt()
        {

            string data = CreateToken();

            var json_object = JObject.Parse(data);

            Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"order_id", "777"}
            };

            Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"amount", 1000},
                {"capture", true},
                {"currency_code", "PEN"},
                {"description", "Venta de prueba"},
                {"email", "wmuro@me.com"},
                {"installments", 0},
                {"metadata", metadata},
                {"source_id", (string)json_object["id"]}
            };

            return new Charge(security).Create(map, security.rsa_id, security.rsa_key);

        }

        [Test]
        public void ValidCreateChargeEncrypt()
        {
            string data = CreateChargeEncrypt();

            var json_object = JObject.Parse(data);

            Assert.AreEqual("charge", (string)json_object["object"]);
        }

        protected string CreateOrder()
        {

            string data = CreateToken();

            var json_object = JObject.Parse(data);
            int order_number = GetRandomNumber();
            TimeSpan t = DateTime.UtcNow.AddDays(1) - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;

            Dictionary<string, object> client_details = new Dictionary<string, object>
            {
                {"first_name", "Juan"},
                {"last_name", "Diaz"},
                {"email", "juan.diaz@culqi.com"},
                {"phone_number", "+51948747421"},

            };

            Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"amount", 1000},                
                {"currency_code", "PEN"},
                {"description", "Venta de prueba"},
                {"order_number", "pedido"+Convert.ToString(order_number)},
                {"client_details", client_details},
                {"expiration_date", secondsSinceEpoch},
                {"confirm", true}

            };

            return new Order(security).Create(map);

        }

        [Test]
        public void ValidCreateOrder()
        {
            string data = CreateOrder();

            var json_object = JObject.Parse(data);

            Assert.AreEqual("order", (string)json_object["object"]);
        }

        protected string CreateOrderEncrypt()
        {

            string data = CreateToken();

            var json_object = JObject.Parse(data);
            int order_number = GetRandomNumber();
            TimeSpan t = DateTime.UtcNow.AddDays(1) - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;

            Dictionary<string, object> client_details = new Dictionary<string, object>
            {
                {"first_name", "Juan"},
                {"last_name", "Diaz"},
                {"email", "juan.diaz@culqi.com"},
                {"phone_number", "+51948747421"},

            };

            Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"amount", 1000},
                {"currency_code", "PEN"},
                {"description", "Venta de prueba"},
                {"order_number", "pedido"+Convert.ToString(order_number)},
                {"client_details", client_details},
                {"expiration_date", secondsSinceEpoch},
                {"confirm", true}

            };

            return new Order(security).Create(map, security.rsa_id, security.rsa_key);

        }

        [Test]
        public void ValidCreateOrderEncrypt()
        {
            string data = CreateOrderEncrypt();

            var json_object = JObject.Parse(data);

            Assert.AreEqual("order", (string)json_object["object"]);
        }

        protected string CreatePlan()
		{	

			Dictionary<string, object> metadata = new Dictionary<string, object>
			{
				{"others_id", "9092"}
			};

			Dictionary<string, object> map = new Dictionary<string, object>
			{	
				{"amount", 10000},
				{"currency_code", "PEN"},
				{"interval", "dias"},
				{"interval_count", 15},
				{"limit", 2},
				{"metadata", metadata},
				{"name", "plan-culqi-"+GetRandomString()},
				{"trial_days", 15}
			};

			return new Plan(security).Create(map);
		}

		[Test]
		public void ValidCreatePlan()
		{
			string data = CreatePlan();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("plan", (string)json_object["object"]);
		}

		protected string CreateCustomer()
		{
			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"address", "Av Lima 123"},
				{"address_city", "Lima"},
				{"country_code", "PE"},
				{"email", "test"+GetRandomString()+"@culqi.com"},
				{"first_name", "Test"},
				{"last_name", "Culqi"},
				{"phone_number", 99004356}
			};

			return new Customer(security).Create(map);
		}

		[Test]
		public void ValidCreateCustomer()
		{
			string data = CreateCustomer();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("customer", (string)json_object["object"]);
		}

		protected string CreateCard()
		{

			string token = CreateToken();
			string customer = CreateCustomer();

			var json_token = JObject.Parse(token);
			var json_customer = JObject.Parse(customer);

			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"customer_id", (string)json_customer["id"]},
				{"token_id", (string)json_token["id"]}
			};

			return new Card(security).Create(map);

		}

		[Test]
		public void ValidCreateCard()
		{
			string data = CreateCard();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("card", (string)json_object["object"]);
		}


		protected string CreateSubscription()
		{	
			string plan_data = CreatePlan();
			var json_plan = JObject.Parse(plan_data);

			string card_data = CreateCard();
			var json_card = JObject.Parse(card_data);

			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"card_id", (string)json_card["id"]},
				{"plan_id", (string)json_plan["id"]}
			};

			return new Subscription(security).Create(map);
		}

		[Test]
		public void ValidCreateSubscription()
		{
			string data = CreateSubscription();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("subscription", (string)json_object["object"]);
		}

		[Test]
		public void GetChargeCapture()
		{	
			string charge_data = CreateCharge();

			var json_charge = JObject.Parse(charge_data);

			string capture_data = new Charge(security).Capture((string)json_charge["id"]);

			var json_capture = JObject.Parse(capture_data);

			Assert.AreNotSame("charge", (string)json_capture["id"]);
		}

		protected string CreateRefund()
		{	
			string data = CreateCharge();

			var json_object = JObject.Parse(data);

			Dictionary<string, object> map = new Dictionary<string, object>
			{
				{"amount", 500},
				{"charge_id", (string)json_object["id"]},
				{"reason", "solicitud_comprador"}
			};

			return new Refund(security).Create(map);
		}

		[Test]
		public void ValidCreateRefund()
		{
			string data = CreateRefund();

			var json_object = JObject.Parse(data);

			Assert.AreEqual("refund", (string)json_object["object"]);
		}

		// Consultar Recursos

		[Test]
		public void findToken()
		{
			string data = CreateToken();
			var json_object = JObject.Parse(data);
			string token = new Token(security).Get((string)json_object["id"]);
			var json_token = JObject.Parse(token);
			Assert.AreEqual("token", (string)json_token["object"]);
		}

        [Test]
        public void findOrder()
        {
            string data = CreateOrder();
            var json_object = JObject.Parse(data);
            string order = new Order(security).Get((string)json_object["id"]);
            var json_order = JObject.Parse(order);
            Assert.AreEqual("order", (string)json_order["object"]);
        }

        [Test]
		public void findCharge()
		{
			string data = CreateCharge();
			var json_object = JObject.Parse(data);
			string charge = new Charge(security).Get((string)json_object["id"]);
			var json_charge = JObject.Parse(charge);
			Assert.AreEqual("charge", (string)json_charge["object"]);
		}

		[Test]
		public void findPlan()
		{
			string data = CreatePlan();
			var json_object = JObject.Parse(data);
			string plan = new Plan(security).Get((string)json_object["id"]);
			var json_plan = JObject.Parse(plan);
			Assert.AreEqual("plan", (string)json_plan["object"]);
		}

		[Test]
		public void findCustomer()
		{
			string data = CreateCustomer();
			var json_object = JObject.Parse(data);
			string customer = new Customer(security).Get((string)json_object["id"]);
			var json_customer = JObject.Parse(customer);
			Assert.AreEqual("customer", (string)json_customer["object"]);
		}

		[Test]
		public void findCard()
		{
			string data = CreateCard();
			var json_object = JObject.Parse(data);
			string card = new Card(security).Get((string)json_object["id"]);
			var json_card = JObject.Parse(card);
			Assert.AreEqual("card", (string)json_card["object"]);
		}

		[Test]
		public void findSubscription()
		{
			string data = CreateSubscription();
			var json_object = JObject.Parse(data);
			string subscrption = new Subscription(security).Get((string)json_object["id"]);
			var json_subscrption = JObject.Parse(subscrption);
			Assert.AreEqual("subscription", (string)json_subscrption["object"]);
		}

		[Test]
		public void findRefund()
		{
			string data = CreateRefund();
			var json_object = JObject.Parse(data);
			string refund = new Refund(security).Get((string)json_object["id"]);
			var json_refund = JObject.Parse(refund);
			Assert.AreEqual("refund", (string)json_refund["object"]);
		}

		// Eliminar Recursos

		[Test]
		public void deleteSubscription()
		{
			string data = CreateSubscription();
			var json_object = JObject.Parse(data);
			string subscription = new Subscription(security).Delete((string)json_object["id"]);
			var json_subscription = JObject.Parse(subscription);
			Assert.True((bool)json_subscription["deleted"]);
		}

		[Test]
		public void deletePlan()
		{
			string data = CreatePlan();
			var json_object = JObject.Parse(data);
			string plan = new Plan(security).Delete((string)json_object["id"]);
			var json_plan = JObject.Parse(plan);
			Assert.True((bool)json_plan["deleted"]);
		}

		[Test]
		public void deleteCard()
		{
			string data = CreateCard();
			var json_object = JObject.Parse(data);
			string card = new Card(security).Delete((string)json_object["id"]);
			var json_card = JObject.Parse(card);
			Assert.True((bool)json_card["deleted"]);
		}

		[Test]
		public void deleteCustomer()
		{
			string data = CreateCustomer();
			var json_object = JObject.Parse(data);
			string customer = new Customer(security).Delete((string)json_object["id"]);
			var json_customer = JObject.Parse(customer);
			Assert.True((bool)json_customer["deleted"]);
		}

	}
}
