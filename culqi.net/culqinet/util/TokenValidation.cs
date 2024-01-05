using System;
using System.Text.RegularExpressions;
using culqi.net;

namespace culqinet.util
{
    public static class TokenValidation
    {
        public static void Create(Dictionary<string, object> data)
        {
            string cardNumber = Convert.ToString(data["card_number"]) ?? string.Empty;
            string cvv = Convert.ToString(data["cvv"]) ?? string.Empty;
            string email = Convert.ToString(data["email"]) ?? string.Empty;
            string expirationMonth = Convert.ToString(data["expiration_month"]) ?? string.Empty;
            string expirationYear = Convert.ToString(data["expiration_year"]) ?? string.Empty;

            if (!Helper.IsValidCardNumber(cardNumber))
            {
                throw new CustomException("Invalid card number.");
            }

            if (!Regex.IsMatch(cvv, @"^\d{3,4}$"))
            {
                throw new CustomException("Invalid CVV.");
            }

            if (!Helper.IsValidEmail(email))
            {
                throw new CustomException("Invalid email.");
            }

            if (!Regex.IsMatch(expirationMonth, @"^(0?[1-9]|1[012])$"))
            {
                throw new CustomException("Invalid expiration month.");
            }

            int expYearInt;
            bool isYearValid = int.TryParse(expirationYear, out expYearInt);
            if (!isYearValid || expYearInt < DateTime.Now.Year)
            {
                throw new CustomException("Invalid expiration year.");
            }

            var expDate = new DateTime(expYearInt, int.Parse(expirationMonth), 1); // Assuming expiration is the first of the month
            if (expDate < DateTime.Now)
            {
                throw new CustomException("Card has expired.");
            }
        }

        public static void CreateTokenYape(Dictionary<string, object> data)
        {
            if (data.TryGetValue("amount", out object amountObj))
            {
                Helper.ValidateAmountValue(amountObj);
            }
        }

        public static void List(Dictionary<string, object> data)
        {
            if (data.ContainsKey("device_type"))
            {
                List<string> allowedDeviceValues = new List<string> { "desktop", "mobile", "tablet" };
                Helper.ValidateValue(data["device_type"] as string, allowedDeviceValues);
            }

            if (data.ContainsKey("card_brand"))
            {
                List<string> allowedBrandValues = new List<string> { "Visa", "Mastercard", "Amex", "Diners" };
                Helper.ValidateValue(data["card_brand"] as string, allowedBrandValues);
            }

            if (data.ContainsKey("card_type"))
            {
                List<string> allowedCardTypeValues = new List<string> { "credito", "debito", "internacional" };
                Helper.ValidateValue(data["card_type"] as string, allowedCardTypeValues);
            }

            if (data.ContainsKey("country_code"))
            {
                List<string> countryCodes = Util.GetCountryCodes();
                Helper.ValidateValue(data["country_code"] as string, countryCodes);
            }

            if (data.ContainsKey("creation_date_from") && data.ContainsKey("creation_date_to"))
            {
                Helper.ValidateDateFilter(data["creation_date_from"] as string, data["creation_date_to"] as string);
            }
        }
    }
}

    