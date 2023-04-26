using System;
namespace culqi.net
{
	public class Config
	{
		public Config()
		{
		}

		public string url_api_base { get; set;} = "https://qa-api.culqi.xyz/v2";
        public string url_api_token { get; set; } = "https://qa-secure.culqi.xyz/v2";

    }
}
