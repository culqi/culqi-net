using System;
using System.Collections.Generic;
using culqinet.util;
using Newtonsoft.Json;
using RestSharp;

namespace culqi.net
{
	public class Generic
	{	
		string URL = "/";

		Security security { get; set; }

		public Generic(Security security, String url)
		{
			this.security = security;
            this.URL = url;
        }

		public RestResponse List(Dictionary<string, object> query_params)
		{
			return new RequestCulqi().Request(query_params, URL, security.secret_key, "get");
		}

		public RestResponse Create(Dictionary<string, object> body)
		{
            var api_key = "";
            if (URL.Contains("tokens") || URL.Contains("confirm"))
            {
                api_key = security.public_key;
            }
            else
            {
                api_key = security.secret_key;
            }
            return new RequestCulqi().Request(body, URL, api_key, "post");
		}
        public RestResponse Create(Dictionary<string, object> body, String rsa_id, String rsa_key)
        {
            var api_key = "";
            if (URL.Contains("tokens") || URL.Contains("confirm"))
            {
                api_key = security.public_key;
            }
            else
            {
                api_key = security.secret_key;
            }
            Encrypt encrypt = new Encrypt();
            var jsonString = JsonConvert.SerializeObject(body);

            // Llamada a la función EncryptWithAESRSAAsync
            var encryptedResultTask = encrypt.EncryptWithAESRSA(jsonString, rsa_key, true);
            // Esperar a que la tarea se complete y obtener el resultado usando la propiedad Result
            var encryptedResult = encryptedResultTask;//.Result;
            Console.WriteLine(encryptedResult);
            body = encryptedResult;
            return new RequestCulqi().Request(body, URL, api_key, "post", rsa_id);
        }
        public RestResponse Get(String id)
		{
			return new RequestCulqi().Request(null, URL + id + "/", security.secret_key, "get");
		}

		public RestResponse Update(Dictionary<string, object> body, String id)
		{
			return new RequestCulqi().Request(body, URL + id + "/", security.secret_key, "patch");
		}

        public RestResponse Update(Dictionary<string, object> body, String id, String rsa_id, String rsa_key)
        {
            Encrypt encrypt = new Encrypt();
            var jsonString = JsonConvert.SerializeObject(body);

            // Llamada a la función EncryptWithAESRSAAsync
            var encryptedResultTask = encrypt.EncryptWithAESRSA(jsonString, rsa_key, true);
            // Esperar a que la tarea se complete y obtener el resultado usando la propiedad Result
            var encryptedResult = encryptedResultTask;//.Result;
            body = encryptedResult;
            return new RequestCulqi().Request(body, URL + id + "/", security.secret_key, "patch", rsa_id);
        }

        public RestResponse Delete(String id)
        {
            return new RequestCulqi().Request(null, URL + id + "/", security.secret_key, "delete");
        }
        public RestResponse CreateYape(Dictionary<string, object> body)
        {
            return new RequestCulqi().Request(body, URL + "yape", security.public_key, "post");
        }
        public RestResponse Capture(String id)
        {
            return new RequestCulqi().Request(null, URL + id + "/capture/", security.secret_key, "post");
        }
    }
}
