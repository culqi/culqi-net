using System;

namespace culqinet.util
{
    public class CustomException : Exception
    {
        public CustomException(string merchantMessage) : base(merchantMessage)
        {
            ErrorData = new ErrorData
            {
                ObjectType = "error",
                Type = "param_error",
                MerchantMessage = merchantMessage,
                UserMessage = merchantMessage
            };
        }

        public ErrorData ErrorData { get; }
    }

    public class ErrorData
    {
        public string ObjectType { get; set; }
        public string Type { get; set; }
        public string MerchantMessage { get; set; }
        public string UserMessage { get; set; }

        // Convert properties to a dictionary
        public Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>
            {
                { "object", this.ObjectType },
                { "type", this.Type },
                { "merchant_message", this.MerchantMessage },
                { "user_message", this.UserMessage }
            };
        }
    }
}