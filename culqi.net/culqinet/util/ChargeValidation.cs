using System;
using System.Text.RegularExpressions;
using culqi.net;

namespace culqinet.util
{
    public static class ChargeValidation
    {
        public static void Create(Dictionary<string, object> data)
        {
            string email = data["email"] as string;
            if (!Helper.IsValidEmail(email))
            {
                throw new CustomException("Invalid email.");
            }

            object amountObj = data["amount"];
            Helper.ValidateAmountValue(amountObj);

            Helper.ValidateCurrencyCode(data["currency_code"] as string);

            string sourceId = data["source_id"] as string;

            if (sourceId.StartsWith("tkn"))
            {
                Helper.ValidateStringStart(sourceId, "tkn");
            }
            else if (sourceId.StartsWith("ype"))
            {
                Helper.ValidateStringStart(sourceId, "ype");
            }
            else if (sourceId.StartsWith("crd"))
            {
                Helper.ValidateStringStart(sourceId, "crd");
            }
            else
            {
                throw new CustomException("Incorrect format. The format must start with tkn, ype, or crd.");
            }
        }

        public static void List(Dictionary<string, object> data)
        {
            if (data.ContainsKey("currency_code"))
            {
                Helper.ValidateCurrencyCode(data["currency_code"] as string);
            }
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
            if (data.ContainsKey("installments"))
            {
                object amountObj = data["installments"];
                Helper.ValidateAmountValue(amountObj);
            }
            if (data.ContainsKey("min_installments"))
            {
                object amountObj = data["min_installments"];
                Helper.ValidateAmountValue(amountObj);
            }
            if (data.ContainsKey("max_installments"))
            {
                object amountObj = data["max_installments"];
                Helper.ValidateAmountValue(amountObj);
            }
            if (data.ContainsKey("email"))
            {
                string email = data["email"] as string;
                if (!Helper.IsValidEmail(email))
                {
                    throw new CustomException("Invalid email.");
                }
            }
            if (data.ContainsKey("card_brand"))
            {
                List<string> allowedBrandValues = new List<string> { "Visa", "Mastercard", "Amex", "Diners" };
                Helper.ValidateValue(data["card_brand"] as string, allowedBrandValues);
            }
            if (data.ContainsKey("card_type"))
            {
                List<string> allowedBrandValues = new List<string> { "credito", "debito", "internacional" };
                Helper.ValidateValue(data["card_brand"] as string, allowedBrandValues);
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

    