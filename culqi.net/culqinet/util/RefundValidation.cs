using System;
using System.Text.RegularExpressions;
using culqi.net;

namespace culqinet.util
{
    public static class RefundValidation
    {
        public static void Create(Dictionary<string, object> data)
        {
            string chargeId = data["charge_id"] as string;
            Helper.ValidateStringStart(chargeId, "chr");
            List<string> allowedValues = new List<string> { "duplicado", "fraudulento", "solicitud_comprador" };
            Helper.ValidateValue(data["reason"] as string, allowedValues);
            object amountObj = data["amount"];
            Helper.ValidateAmountValue(amountObj);
        }

        public static void List(Dictionary<string, object> data)
        {
            if (data.ContainsKey("reason"))
            {
                List<string> allowedValues = new List<string> { "duplicado", "fraudulento", "solicitud_comprador" };
                Helper.ValidateValue(data["reason"] as string, allowedValues);
            }
            if (data.ContainsKey("creation_date_from") && data.ContainsKey("creation_date_to"))
            {
                Helper.ValidateDateFilter(data["creation_date_from"] as string, data["creation_date_to"] as string);
            }
        }
    }
}

    