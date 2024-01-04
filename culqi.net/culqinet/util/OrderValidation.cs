using System;
using System.Text.RegularExpressions;
using culqi.net;

namespace culqinet.util
{
    public static class OrderValidation
    {
        public static void Create(Dictionary<string, object> data)
        {
            if (!(data.TryGetValue("client_details", out var clientDetailsObj) && clientDetailsObj is Dictionary<string, object> clientDetails))
            {
                throw new Exception("client_details is missing or invalid.");
            }
            
            string firstName = clientDetails.TryGetValue("first_name", out var temp) ? (string)temp : null;
            string lastName = clientDetails.TryGetValue("last_name", out temp) ? (string)temp : null;
            string phoneNumber = clientDetails.TryGetValue("phone_number", out temp) ? (string)temp : null;
            string email = clientDetails.TryGetValue("email", out temp) ? (string)temp : null;

            if (string.IsNullOrEmpty(firstName))
            {
                throw new Exception("first name is empty.");
            }
            if (string.IsNullOrEmpty(lastName))
            {
                throw new Exception("last name is empty.");
            }
            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new Exception("phone number is empty.");
            }
            if (!Helper.IsValidEmail(email))
            {
                throw new CustomException("Invalid email.");
            }

            Helper.ValidateCurrencyCode(data["currency_code"] as string);
            object amountObj = data.TryGetValue("amount", out temp) ? temp : null;
            Helper.ValidateAmountValue(amountObj);

            if (data.TryGetValue("expiration_date", out temp) && temp is long expirationDate && !Helper.IsFutureDate(expirationDate))
            {
                throw new Exception("expiration_date must be a future date.");
            }
        }

        public static void List(Dictionary<string, object> data)
        {
            if (data.ContainsKey("amount"))
            {
                object amountObj = data["amount"];
                Helper.ValidateAmountValue(amountObj);
            }
            if (data.ContainsKey("min_amount"))
            {
                object amountObj = data["min_amount"];
                Helper.ValidateAmountValue(amountObj);
            }
            if (data.ContainsKey("max_amount"))
            {
                object amountObj = data["max_amount"];
                Helper.ValidateAmountValue(amountObj);
            }
            if (data.ContainsKey("creation_date_from") && data.ContainsKey("creation_date_to"))
            {
                Helper.ValidateDateFilter(data["creation_date_from"] as string, data["creation_date_to"] as string);
            }
        }
    }
}

    