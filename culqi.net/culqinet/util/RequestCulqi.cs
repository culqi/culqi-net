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

        public RestResponse Request(Object model, string url, string api_key, string type_method, Dictionary<string, object> custom_header = null)
        {
            return GenericRequest(model, url, api_key, type_method, null, custom_header);

        }

        public RestResponse Request(Object model, string url, string api_key, string type_method, string rsa_id, Dictionary<string, object> custom_header = null)
        {
            return GenericRequest(model, url, api_key, type_method, rsa_id, custom_header);

        }


        public RestResponse GenericRequest(Object model, string url, string api_key, string type_method, string rsa_id, Dictionary<string, object> custom_header = null)
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

            var env = config.x_culqi_env_live;

            if (api_key.Contains("test"))
            {
                env = config.x_culqi_env_test;
            }

            //string output = JsonConvert.SerializeObject(model);
            //Console.WriteLine(output);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + api_key);
            request.AddHeader("x-culqi-env", env);
            request.AddHeader("x-api-version", config.x_api_version);
            request.AddHeader("x-culqi-client", config.x_culqi_client);
            request.AddHeader("x-culqi-client-version", config.x_culqi_client_version);

            if (rsa_id != null)
            {
                request.AddHeader("x-culqi-rsa-id", rsa_id);
            }

            if (custom_header != null)
            {
                Console.WriteLine("\nCustom Headers:");
                Console.WriteLine(JsonConvert.SerializeObject(custom_header));

                foreach (var header in custom_header)
                {
                    if (header.Value != null && !string.IsNullOrWhiteSpace(header.Value.ToString()))
                    {
                        request.AddHeader(header.Key, header.Value.ToString());
                    }
                }
            }

            RestResponse response = client.Execute(request);
            /*respCulqi.statusCode = (int) response.StatusCode;
            respCulqi.body = response.Content;*/
            Console.WriteLine(request);
            Console.WriteLine(response.Content);
            return response;

        }

    }
}
