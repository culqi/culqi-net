using System;
using System.Text.RegularExpressions;
using culqi.net;

namespace culqinet.util
{
    public static class SubscriptionValidation
    {
        public static void Create(Dictionary<string, object> data)
        {
            string cardId = data["card_id"] as string;
            Helper.ValidateStringStart(cardId, "crd");
            
            string planId = data["plan_id"] as string;
            Helper.ValidateStringStart(planId, "pln");
        }

        public static void List(Dictionary<string, object> data)
        {
            if (data.ContainsKey("plan_id"))
            {
                string planId = data["plan_id"] as string;
                Helper.ValidateStringStart(planId, "pln");
            }
            if (data.ContainsKey("creation_date_from") && data.ContainsKey("creation_date_to"))
            {
                Helper.ValidateDateFilter(data["creation_date_from"] as string, data["creation_date_to"] as string);
            }
        }
    }
}

    