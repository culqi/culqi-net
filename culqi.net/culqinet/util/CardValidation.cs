using System;
using System.Text.RegularExpressions;
using culqi.net;

namespace culqinet.util
{
    public static class CardValidation
    {
        public static void Create(Dictionary<string, object> data)
        {
            string sourceId = data["token_id"] as string;
            Helper.ValidateStringStart(sourceId, "tkn");
            
            string customerId = data["customer_id"] as string;
            Helper.ValidateStringStart(customerId, "cus");
        }

        public static void List(Dictionary<string, object> data)
        {
            if (data.ContainsKey("card_brand"))
            {
                List<string> allowedBrandValues = new List<string> { "Visa", "Mastercard", "Amex", "Diners" };
                Helper.ValidateValue(data["card_brand"] as string, allowedBrandValues);
            }
            if (data.ContainsKey("card_type"))
            {
                List<string> allowedBrandValues = new List<string> { "credito", "debito", "internacional" };
                Helper.ValidateValue(data["card_type"] as string, allowedBrandValues);
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

    