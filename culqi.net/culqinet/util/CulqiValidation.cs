using System;
using System.Text.RegularExpressions;

namespace culqinet.util

public class CulqiValidation
{
    public void CreateTokenValidation(Dictionary<string, string> data)
    {
        // Validate card number
        if (!IsCardNumberValid(data["card_number"]))
        {
            throw new Exception("Invalid card number.");
        }

        // Validate CVV
        if (!Regex.IsMatch(data["cvv"], @"^\d{3,4}$"))
        {
            throw new Exception("Invalid CVV.");
        }

        // Validate email
        if (!IsEmailValid(data["email"]))
        {
            throw new Exception("Invalid email.");
        }

        // Validate expiration month
        if (!Regex.IsMatch(data["expiration_month"], @"^(0?[1-9]|1[012])$"))
        {
            throw new Exception("Invalid expiration month.");
        }

        // Validate expiration year
        if (!Regex.IsMatch(data["expiration_year"], @"^\d{4}$") ||
            int.Parse(data["expiration_year"]) < DateTime.Now.Year)
        {
            throw new Exception("Invalid expiration year.");
        }

        // Check if the card is expired
        var expDate = DateTime.ParseExact(data["expiration_year"] + "-" + data["expiration_month"], "yyyy-MM", null);
        if (expDate < DateTime.Now)
        {
            throw new Exception("Card has expired.");
        }
    }

    public void ChargeValidation(Dictionary<string, object> data)
    {
        // Validate email
        if (!IsEmailValid(data["email"].ToString()))
        {
            throw new Exception("Invalid email.");
        }

        // Validate amount
        if (!IsNumeric(data["amount"]))
        {
            throw new Exception("Invalid amount.");
        }

        ValidateCurrencyCode(data["currency_code"].ToString());
        ValidateStringStart(data["source_id"].ToString(), "tkn");
    }

    public void RefundValidation(Dictionary<string, object> data)
    {
        // Validate charge format
        ValidateStringStart(data["charge_id"].ToString(), "chr");

        // Validate reason
        List<string> allowedValues = new List<string> { "duplicado", "fraudulento", "solicitud_comprador" };
        ValidateValue(data["reason"].ToString(), allowedValues);

        // Validate amount
        if (!IsNumeric(data["amount"]))
        {
            throw new Exception("Invalid amount.");
        }
    }

    public void PlanValidation(Dictionary<string, object> data)
    {
        // Validate amount
        if (!IsNumeric(data["amount"]))
        {
            throw new Exception("Invalid amount.");
        }

        // Validate interval
        List<string> allowedValues = new List<string> { "dias", "semanas", "meses", "años" };
        ValidateValue(data["interval"].ToString(), allowedValues);

        // Validate currency
        ValidateCurrencyCode(data["currency_code"].ToString());
    }

    public void CustomerValidation(Dictionary<string, object> data)
    {
        // Validate address, firstname, and lastname
        if (!data.ContainsKey("first_name") || string.IsNullOrEmpty(data["first_name"].ToString()))
        {
            throw new Exception("First name is empty.");
        }

        if (!data.ContainsKey("last_name") || string.IsNullOrEmpty(data["last_name"].ToString()))
        {
            throw new Exception("Last name is empty.");
        }

        if (!data.ContainsKey("address") || string.IsNullOrEmpty(data["address"].ToString()))
        {
            throw new Exception("Address is empty.");
        }

        if (!data.ContainsKey("address_city") || string.IsNullOrEmpty(data["address_city"].ToString()))
        {
            throw new Exception("Address city is empty.");
        }

        // Validate country code
        ValidateValue(data["country_code"].ToString(), Util.GetCountryCodes());

        // Validate email
        if (!IsEmailValid(data["email"].ToString()))
        {
            throw new Exception("Invalid email.");
        }
    }

    public void CardValidation(Dictionary<string, object> data)
    {
        // Validate customer and token format
        ValidateStringStart(data["customer_id"].ToString(), "cus");
        ValidateStringStart(data["token_id"].ToString(), "tkn");
    }

    public void SubscriptionValidation(Dictionary<string, object> data)
    {
        // Validate card and plan format
        ValidateStringStart(data["card_id"].ToString(), "crd");
        ValidateStringStart(data["plan_id"].ToString(), "pln");
    }

    public void OrderValidation(Dictionary<string, object> data)
    {
        // Validate amount
        if (!IsNumeric(data["amount"]))
        {
            throw new Exception("Invalid amount.");
        }

        // Validate currency
        ValidateCurrencyCode(data["currency_code"].ToString());

        // Validate firstname, lastname, and phone
        Dictionary<string, object> clientDetails = data.ContainsKey("client_details")
            ? data["client_details"] as Dictionary<string, object>
            : new Dictionary<string, object>();

        if (!clientDetails.ContainsKey("first_name") || string.IsNullOrEmpty(clientDetails["first_name"].ToString()))
        {
            throw new Exception("First name is empty.");
        }

        if (!clientDetails.ContainsKey("last_name") || string.IsNullOrEmpty(clientDetails["last_name"].ToString()))
        {
            throw new Exception("Last name is empty.");
        }

        if (!clientDetails.ContainsKey("phone_number") || string.IsNullOrEmpty(clientDetails["phone_number"].ToString()))
        {
            throw new Exception("Phone number is empty.");
        }

        // Validate email
        if (!IsEmailValid(clientDetails.ContainsKey("email") ? clientDetails["email"].ToString() : null))
        {
            throw new Exception("Invalid email.");
        }

        // Validate expiration date
        if (!IsFutureDate((DateTime)data["expiration_date"]))
        {
            throw new Exception("Expiration date must be a future date.");
        }
    }

    public void ConfirmOrderTypeValidation(Dictionary<string, object> data)
    {
        ValidateStringStart(data["order_id"].ToString(), "ord");
    }

    public static bool IsCardNumberValid(string number)
    {
        return Regex.IsMatch(number, @"^\d{13,19}$");
    }

    public static bool IsEmailValid(string email)
    {
        return Regex.IsMatch(email, @"^\S+@\S+\.\S+$");
    }

    public static void ValidateCurrencyCode(string currencyCode)
    {
        if (string.IsNullOrEmpty(currencyCode))
        {
            throw new Exception("Currency code is empty.");
        }

        if (!(currencyCode is string))
        {
            throw new Exception("Currency code must be a string.");
        }

        List<string> allowedValues = new List<string> { "PEN", "USD" };
        if (!allowedValues.Contains(currencyCode))
        {
            throw new Exception("Currency code must be either 'PEN' or 'USD'.");
        }
    }

    public static void ValidateStringStart(string inputString, string start)
    {
        if (!inputString.StartsWith($"{start}_test_") && !inputString.StartsWith($"{start}_live_"))
        {
            throw new Exception($"Incorrect format. The format must start with {start}_test_ or {start}_live_");
        }
    }

    public static void ValidateValue(string value, List<string> allowedValues)
    {
        if (!allowedValues.Contains(value))
        {
            throw new Exception($"Invalid value. It must be {string.Join(", ", allowedValues)}.");
        }
    }

    public static bool IsFutureDate(DateTime expirationDate)
    {
        return expirationDate > DateTime.Now;
    }
}
