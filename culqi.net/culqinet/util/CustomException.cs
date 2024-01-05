using System;

namespace culqinet.util
{
    public class CustomException : Exception
    {
        public CustomException(string merchantMessage) : base(merchantMessage)
        {
            ErrorData = new ErrorData
            {
                Object = "error",
                Type = "param_error",
                MerchantMessage = merchantMessage,
                UserMessage = merchantMessage
            };
        }

        public ErrorData ErrorData { get; }
    }

    public class ErrorData
    {
        public string Object { get; set; }
        public string Type { get; set; }
        public string MerchantMessage { get; set; }
        public string UserMessage { get; set; }

        // Convert properties to a dictionary
        public Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>
            {
                { "Object", this.Object },
                { "Type", this.Type },
                { "MerchantMessage", this.MerchantMessage },
                { "UserMessage", this.UserMessage }
            };
        }
    }
}