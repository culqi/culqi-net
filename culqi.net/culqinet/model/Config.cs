using System;
namespace culqi.net
{
	public class Config
	{
		public Config()
		{
		}

		public string url_api_base { get; set;} = "https://api.culqi.com/v2";
        public string url_api_token { get; set; } = "https://secure.culqi.com/v2";

    }
}
