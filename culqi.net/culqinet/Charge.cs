using System;
using System.Collections.Generic;
using culqinet.util;
using Newtonsoft.Json;

namespace culqi.net
{
	public class Charge
	{	
		const string URL = "/charges/";

		Security security { get; set; }

		public Charge(Security security)
		{
			this.security = security;
		}

		public string List(Dictionary<string, object> query_params)
		{
			return new Util().Request(query_params, URL, security.secret_key, "get");
		}

		public string Create(Dictionary<string, object> body)
		{
			return new Util().Request(body, URL, security.secret_key, "post");
		}
        public string Create(Dictionary<string, object> body, String rsa_id, String rsa_key)
        {

            Encrypt encrypt = new Encrypt();
            var jsonString = JsonConvert.SerializeObject(body);

            // Llamada a la función EncryptWithAESRSAAsync
            var encryptedResultTask = encrypt.EncryptWithAESRSA(jsonString, rsa_key, true);
            // Esperar a que la tarea se complete y obtener el resultado usando la propiedad Result
            var encryptedResult = encryptedResultTask.Result;
            body = encryptedResult;
            return new Util().Request(body, URL, security.secret_key, "post", rsa_id);
        }
        public string Get(String id)
		{
			return new Util().Request(null, URL + id + "/", security.secret_key, "get");
		}

		public string Update(Dictionary<string, object> body, String id)
		{
			return new Util().Request(body, URL + id + "/", security.secret_key, "patch");
		}

		public string Capture(String id)
		{
			return new Util().Request(null, URL + id + "/capture/", security.secret_key, "post");
		}

	}
}
