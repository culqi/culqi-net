using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Text;
using culqinet.util;
using Newtonsoft.Json;
using RestSharp;


namespace culqi.net
{
    public class Generic
    {
        string URL = "/";

        Security security { get; set; }
        Util util = new Util();

        public Generic(Security security, String url)
        {
            this.security = security;
            this.URL = url;
        }

        public HttpResponseMessage List(Dictionary<string, object> query_params)
        {
            var responseObject = new RequestCulqi().Request(query_params, URL, security.secret_key, "get");

            return util.CustomResponse(responseObject);
        }

        public HttpResponseMessage Create(Dictionary<string, object> body)
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

            var responseObject = new RequestCulqi().Request(body, URL, api_key, "post");

            return util.CustomResponse(responseObject);

        }
        public HttpResponseMessage Create(Dictionary<string, object> body, String rsa_id, String rsa_key)
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

            var responseObject = new RequestCulqi().Request(body, URL, api_key, "post", rsa_id);

            return util.CustomResponse(responseObject);
        }
        public HttpResponseMessage Get(String id)
        {
            var responseObject = new RequestCulqi().Request(null, URL + id + "/", security.secret_key, "get");

            return util.CustomResponse(responseObject);
        }

        public HttpResponseMessage Update(Dictionary<string, object> body, String id)
        {
            var responseObject = new RequestCulqi().Request(body, URL + id + "/", security.secret_key, "patch");

            return util.CustomResponse(responseObject);
        }

        public HttpResponseMessage Update(Dictionary<string, object> body, String id, String rsa_id, String rsa_key)
        {
            Encrypt encrypt = new Encrypt();
            var jsonString = JsonConvert.SerializeObject(body);

            // Llamada a la función EncryptWithAESRSAAsync
            var encryptedResultTask = encrypt.EncryptWithAESRSA(jsonString, rsa_key, true);
            // Esperar a que la tarea se complete y obtener el resultado usando la propiedad Result
            var encryptedResult = encryptedResultTask;//.Result;
            body = encryptedResult;

            var responseObject = new RequestCulqi().Request(body, URL + id + "/", security.secret_key, "patch", rsa_id);

            return util.CustomResponse(responseObject);
        }

        public HttpResponseMessage Delete(String id)
        {
            var responseObject = new RequestCulqi().Request(null, URL + id + "/", security.secret_key, "delete");

            return util.CustomResponse(responseObject);
        }
        public HttpResponseMessage CreateYape(Dictionary<string, object> body)
        {
            var responseObject = new RequestCulqi().Request(body, URL + "yape", security.public_key, "post");

            return util.CustomResponse(responseObject);
        }
        public HttpResponseMessage Capture(String id)
        {
            var responseObject = new RequestCulqi().Request(null, URL + id + "/capture/", security.secret_key, "post");

            return util.CustomResponse(responseObject);
        }
    }
}