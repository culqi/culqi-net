using System;
using System.Net;
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
            Dictionary<string, string> validationResponse = VerifyClassValidationList(query_params, this.URL);
            if (validationResponse != null)
            {
                RestResponse response = new RestResponse();
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = JsonConvert.SerializeObject(validationResponse);

                string merchantMessage = validationResponse?["MerchantMessage"] ?? "Mensaje no encontrado";
                return util.CustomResponse(response);
            }
            var responseObject = new RequestCulqi().Request(query_params, URL, security.secret_key, "get");

            return util.CustomResponse(responseObject);
        }

        public HttpResponseMessage Create(Dictionary<string, object> body, Dictionary<string, object> custom_header = null)
        {
            Dictionary<string, string> validationResponse = VerifyClassValidationCreate(body, this.URL);
            if (validationResponse != null)
            {
                RestResponse response = new RestResponse();
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = JsonConvert.SerializeObject(validationResponse);
                // Obtener el valor asociado con la clave "MerchantMessage" o proporcionar un valor predeterminado
                string merchantMessage = validationResponse?["MerchantMessage"] ?? "Mensaje no encontrado";
                return util.CustomResponse(response);
            }
            var api_key = "";
            if (URL.Contains("tokens") || URL.Contains("confirm"))
            {
                api_key = security.public_key;
            }
            else if (URL.Contains("plans") || URL.Contains("subscriptions"))
            {
                api_key = security.secret_key;
                URL = URL + "create";
            }
            else
            {
                api_key = security.secret_key;
            }

            var responseObject = new RequestCulqi().Request(body, URL, api_key, "post", custom_header);

            return util.CustomResponse(responseObject);

        }
        public HttpResponseMessage Create(Dictionary<string, object> body, String rsa_id, String rsa_key, Dictionary<string, object> custom_header = null)
        {
            Dictionary<string, string> validationResponse = VerifyClassValidationCreate(body, this.URL);
            if (validationResponse != null)
            {
                RestResponse response = new RestResponse();
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = JsonConvert.SerializeObject(validationResponse);
                string merchantMessage = validationResponse?["MerchantMessage"] ?? "Mensaje no encontrado";
                return util.CustomResponse(response);
            }
            var api_key = "";
            if (URL.Contains("tokens") || URL.Contains("confirm"))
            {
                api_key = security.public_key;
            }
            else if (URL.Contains("plans") || URL.Contains("subscriptions"))
            {
                api_key = security.secret_key;
                URL = URL + "create";
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

            var responseObject = new RequestCulqi().Request(body, URL, api_key, "post", rsa_id, custom_header);

            return util.CustomResponse(responseObject);
        }
        public HttpResponseMessage Get(String id)
        {
            Dictionary<string, string> validationResponse = VerifyClassValidationUpdate(id, this.URL);
            if (validationResponse != null)
            {
                RestResponse response = new RestResponse();
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = JsonConvert.SerializeObject(validationResponse);
                string merchantMessage = validationResponse?["MerchantMessage"] ?? "Mensaje no encontrado";
                return util.CustomResponse(response);
            }
            var responseObject = new RequestCulqi().Request(null, URL + id + "/", security.secret_key, "get");

            return util.CustomResponse(responseObject);
        }

        public HttpResponseMessage Update(Dictionary<string, object> body, String id)
        {
            Dictionary<string, string> validationResponse = VerifyClassValidationPayloadUpdate(id, this.URL, body);
            if (validationResponse != null)
            {
                RestResponse response = new RestResponse();
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = JsonConvert.SerializeObject(validationResponse);
                string merchantMessage = validationResponse?["MerchantMessage"] ?? "Mensaje no encontrado";
                return util.CustomResponse(response);
            }
            var responseObject = new RequestCulqi().Request(body, URL + id + "/", security.secret_key, "patch");

            return util.CustomResponse(responseObject);
        }

        public HttpResponseMessage Update(Dictionary<string, object> body, String id, String rsa_id, String rsa_key)
        {
            Dictionary<string, string> validationResponse = VerifyClassValidationPayloadUpdate(id, this.URL, body);
            if (validationResponse != null)
            {
                RestResponse response = new RestResponse();
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = JsonConvert.SerializeObject(validationResponse);
                string merchantMessage = validationResponse?["MerchantMessage"] ?? "Mensaje no encontrado";
                return util.CustomResponse(response);
            }
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
            Dictionary<string, string> validationResponse = VerifyClassValidationUpdate(id, this.URL);
            if (validationResponse != null)
            {
                RestResponse response = new RestResponse();
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = JsonConvert.SerializeObject(validationResponse);
                string merchantMessage = validationResponse?["MerchantMessage"] ?? "Mensaje no encontrado";
                return util.CustomResponse(response);
            }
            var responseObject = new RequestCulqi().Request(null, URL + id + "/", security.secret_key, "delete");

            return util.CustomResponse(responseObject);
        }
        public HttpResponseMessage CreateYape(Dictionary<string, object> body)
        {
            Dictionary<string, string> validationResponse = VerifyClassValidationYape(body);
            if (validationResponse != null)
            {
                RestResponse response = new RestResponse();
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = JsonConvert.SerializeObject(validationResponse);
                string merchantMessage = validationResponse?["MerchantMessage"] ?? "Mensaje no encontrado";
                return util.CustomResponse(response);
            }
            var responseObject = new RequestCulqi().Request(body, URL + "yape", security.public_key, "post");

            return util.CustomResponse(responseObject);
        }
        public HttpResponseMessage Capture(String id)
        {
            var responseObject = new RequestCulqi().Request(null, URL + id + "/capture/", security.secret_key, "post");

            return util.CustomResponse(responseObject);
        }

        private static Dictionary<string, string> VerifyClassValidationCreate(Dictionary<string, object> body, string url)
        {
            try
            {
                if (url.Contains("tokens"))
                {
                    TokenValidation.Create(body);
                }
                if (url.Contains("charges"))
                {
                    ChargeValidation.Create(body);
                }
                if (url.Contains("cards"))
                {
                    CardValidation.Create(body);
                }
                if (url.Contains("customers"))
                {
                    CustomerValidation.Create(body);
                }
                if (url.Contains("plans"))
                {
                    PlanValidation.Create(body);
                }
                if (url.Contains("refunds"))
                {
                    RefundValidation.Create(body);
                }
                if (url.Contains("subscriptions"))
                {
                    SubscriptionValidation.Create(body);
                }
                if (url.Contains("orders"))
                {
                    OrderValidation.Create(body);
                }
            }
            catch (CustomException e)
            {
                Console.WriteLine("Entro al error ");
                Dictionary<string, string> errorDictionary = e.ErrorData.ToDictionary();
                return errorDictionary;
            }
            return null;
        }
        private static Dictionary<string, string> VerifyClassValidationPayloadUpdate(string id, string url, Dictionary<string, object> body)
        {
            try
            {
                if (url.Contains("plans"))
                {
                    Helper.ValidateId(id);
                    Helper.ValidateStringStart(id, "pln");
                    PlanValidation.Update(body);
                }
                if (url.Contains("subscriptions"))
                {
                    Helper.ValidateId(id);
                    Helper.ValidateStringStart(id, "sxn");
                    SubscriptionValidation.Update(body);
                }
            }
            catch (CustomException e)
            {
                Dictionary<string, string> errorDictionary = e.ErrorData.ToDictionary();
                return errorDictionary;
            }
            return null;
        }

        private static Dictionary<string, string> VerifyClassValidationUpdate(string id, string url)
        {
            try
            {
                if (url.Contains("tokens"))
                {
                    Helper.ValidateStringStart(id, "tkn");
                }
                if (url.Contains("charges"))
                {
                    Helper.ValidateStringStart(id, "chr");
                }
                if (url.Contains("cards"))
                {
                    Helper.ValidateStringStart(id, "crd");
                }
                if (url.Contains("customers"))
                {
                    Helper.ValidateStringStart(id, "cus");
                }
                if (url.Contains("plans"))
                {
                    Helper.ValidateId(id);
                    Helper.ValidateStringStart(id, "pln");
                }
                if (url.Contains("refunds"))
                {
                    Helper.ValidateStringStart(id, "ref");
                }
                if (url.Contains("subscriptions"))
                {
                    Helper.ValidateId(id);
                    Helper.ValidateStringStart(id, "sxn");
                }
                if (url.Contains("orders"))
                {
                    Helper.ValidateStringStart(id, "ord");
                }
            }
            catch (CustomException e)
            {
                Dictionary<string, string> errorDictionary = e.ErrorData.ToDictionary();
                return errorDictionary;
            }
            return null;
        }
        private static Dictionary<string, string> VerifyClassValidationYape(Dictionary<string, object> body)
        {
            try
            {
                TokenValidation.CreateTokenYape(body);
            }
            catch (CustomException e)
            {
                Dictionary<string, string> errorDictionary = e.ErrorData.ToDictionary();
                return errorDictionary;
            }
            return null;
        }

        private static Dictionary<string, string> VerifyClassValidationList(Dictionary<string, object> query_params, string url)
        {
            try
            {
                if (url.Contains("tokens"))
                {
                    TokenValidation.List(query_params);
                }
                if (url.Contains("charges"))
                {
                    ChargeValidation.List(query_params);
                }
                if (url.Contains("cards"))
                {
                    CardValidation.List(query_params);
                }
                if (url.Contains("customers"))
                {
                    CustomerValidation.List(query_params);
                }
                if (url.Contains("plans"))
                {
                    PlanValidation.List(query_params);
                }
                if (url.Contains("refunds"))
                {
                    RefundValidation.List(query_params);
                }
                if (url.Contains("subscriptions"))
                {
                    SubscriptionValidation.List(query_params);
                }
                if (url.Contains("orders"))
                {
                    OrderValidation.List(query_params);
                }
            }
            catch (CustomException e)
            {
                Dictionary<string, string> errorDictionary = e.ErrorData.ToDictionary();
                return errorDictionary;
            }
            return null;
        }
    }
}