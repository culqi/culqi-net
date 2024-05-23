using System;
namespace culqi.net
{
	public class Config
	{
		public Config()
		{
		}

		public string url_api_base { get; set;} = "https://api.culqi.com/v2";
        public string url_api_secure { get; set; } = "https://secure.culqi.com/v2";
		public string x_culqi_env_test { get; set; } = "test";
		public string x_culqi_env_live { get; set; } = "live";
		public string x_api_version { get; set; } = "2";
		public string x_culqi_client { get; set; } = "culqi-net";
		public string x_culqi_client_version { get; set; } = "0.0.2";


    }
}
