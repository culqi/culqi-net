using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using culqinet.util;
using Newtonsoft.Json;

namespace culqi.net
{
	public class Token
	{
		const string URL = "/tokens";

		Security security { get; set; }

		public Token(Security security)
		{
			this.security = security;
		}

		public string List(Dictionary<string, object> query_params)
		{
			return new Util().Request(query_params, URL, security.secret_key, "get");
		}

		public string Create(Dictionary<string, object> body)
		{
			return new Util().Request(body, URL, security.public_key, "post");
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
            return new Util().Request(body, URL, security.public_key, "post", rsa_id);
        }

        public string CreateYape(Dictionary<string, object> body)
        {
            return new Util().Request(body, URL+"yape", security.public_key, "post");
        }

        public string Get(String id)
		{
			return new Util().Request(null, URL + id + "/", security.secret_key, "get");
		}

	}
}
