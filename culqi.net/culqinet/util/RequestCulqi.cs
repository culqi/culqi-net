using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using Newtonsoft.Json;
using RestSharp;
namespace culqi.net
{
	public class RequestCulqi
	{	

		Config config = new Config();
        Security security { get; set; }

        public RequestCulqi()
		{
        }

		public RestResponse Request(Object model, string url, string api_key, string type_method)
		{
			return GenericRequest(model, url, api_key, type_method, null);

		}

        public RestResponse Request(Object model, string url, string api_key, string type_method, string rsa_id)
        {
            return GenericRequest(model, url, api_key ,type_method, rsa_id);

        }


        public RestResponse GenericRequest(Object model, string url, string api_key, string type_method, string rsa_id)
        {
            //ResponseCulqi respCulqi= new ResponseCulqi();
            var client = new RestClient(config.url_api_base);
            //var api_key = "";

            if (url == "/tokens/" || url == "/tokens/yape")
            {
                client = new RestClient(config.url_api_secure);
            }
            /*
            if (url.Contains("tokens") || url.Contains("confirm"))
            {
                api_key = CulqiKeys.public_key;
            }
            else
            {
                api_key = CulqiKeys.secret_key;
            }
            */
            
            RestSharp.RestRequest request = new RestRequest();

            if (type_method.Equals("get"))
            {
                request = new RestRequest(url, Method.Get);
                if (model != null)
                {
                    Dictionary<string, object> query_params = (Dictionary<string, object>)model;
                    foreach (KeyValuePair<string, object> entry in query_params)
                    {
                        request.AddParameter(entry.Key, entry.Value, ParameterType.QueryString);
                    }
                }
            }
            else if (type_method.Equals("delete"))
            {
                request = new RestRequest(url, Method.Delete);
            }
            else if (type_method.Equals("post"))
            {
                request = new RestRequest(url, Method.Post);
                if (model != null)
                {
                    request.AddJsonBody(model);
                }    
            }
            else if (type_method.Equals("patch"))
            {
                request = new RestRequest(url, Method.Patch);
                if (model != null)
                {
                    request.AddJsonBody(model);
                }
            }

            //string output = JsonConvert.SerializeObject(model);
            //Console.WriteLine(output);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + api_key);
            request.AddHeader("x-culqi-env", config.x_culqi_env);
            request.AddHeader("x-api-version", config.x_api_version);
            request.AddHeader("x-culqi-client", config.x_culqi_client);
            request.AddHeader("x-culqi-client-version", config.x_culqi_client_version);

            if (rsa_id != null)
            {
                request.AddHeader("x-culqi-rsa-id", rsa_id);
            }
            RestResponse response = client.Execute(request);
            /*respCulqi.statusCode = (int) response.StatusCode;
            respCulqi.body = response.Content;*/
            Console.WriteLine(response.Content);
            return response;

        }

    }
}
