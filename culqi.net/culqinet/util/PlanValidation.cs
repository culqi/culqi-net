using System;
using System.Text.RegularExpressions;
using culqi.net;

namespace culqinet.util
{
    public static class PlanValidation
    {
        public static void Create(Dictionary<string, object> data)
        {
            List<string> allowedValues = new List<string> { "dias", "semanas", "meses", "años" };
            Helper.ValidateValue(data["interval"] as string, allowedValues);
            object amountObj = data["amount"];
            Helper.ValidateAmountValue(amountObj);
            Helper.ValidateCurrencyCode(data["currency_code"] as string);
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

    