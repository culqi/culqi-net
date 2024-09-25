using System;
using System.Text.RegularExpressions;
using culqi.net;

namespace culqinet.util
{
    public static class SubscriptionValidation
    {
        public static void Create(Dictionary<string, object> data)
        {
            List<string> requiredPayload = new List<string> { "card_id", "plan_id", "tyc" };
            Exception resultValidation = Helper.AdditionalValidation(data, requiredPayload);
            if (resultValidation != null)    
            {
                throw new CustomException(resultValidation.Message);
            }
            else
            {
                if (!(data["card_id"] is string) || ((string)data["card_id"]).Length != 25)
                {
                    throw new CustomException("El campo 'card_id' es inválido. La longitud debe ser de 25.");
                }
                Helper.ValidateStringStart((string)data["card_id"], "crd");

                if (!(data["plan_id"] is string) || ((string)data["plan_id"]).Length != 25)
                {
                    throw new CustomException("El campo 'plan_id' es inválido. La longitud debe ser de 25.");
                }
                Helper.ValidateStringStart((string)data["plan_id"], "pln");

                if (!(data["tyc"] is bool))
                {
                    throw new CustomException("El campo 'tyc' es inválido o está vacío. El valor debe ser un booleano.");
                }

                if (data.ContainsKey("metadata"))
                {
                    Dictionary<string, object> metadata = data["metadata"] as Dictionary<string, object>;
                    Helper.ValidateMetadata(metadata);
                }
            }
        }

        public static void List(Dictionary<string, object> data)
        {
            if (data.ContainsKey("plan_id"))
            {
                if (!(data["plan_id"] is string) || ((string)data["plan_id"]).Length != 25)
                {
                    throw new CustomException("El campo 'plan_id' es inválido. La longitud debe ser de 25.");
                }
                Helper.ValidateStringStart((string)data["plan_id"], "pln");
            }

            if (data.ContainsKey("status"))
            {
                List<int> valuesStatus = new List<int> { 1, 2, 3, 4, 5, 6, 8 };
                if (!(data["status"] is int) || !valuesStatus.Contains((int)data["status"]))
                {
                    throw new CustomException("El campo 'status' es inválido. Estos son los únicos valores permitidos: 1, 2, 3, 4, 5, 6, 8");
                }
            }

            if (data.ContainsKey("creation_date_from"))
            {
                if (!(data["creation_date_from"] is string) || ((string)data["creation_date_from"]).Length != 10 && ((string)data["creation_date_from"]).Length != 13)
                {
                    throw new CustomException("El campo 'creation_date_from' debe tener una longitud de 10 o 13 caracteres.");
                }
            }

            if (data.ContainsKey("creation_date_to"))
            {
                if (!(data["creation_date_to"] is string) || ((string)data["creation_date_to"]).Length != 10 && ((string)data["creation_date_to"]).Length != 13)
                {
                    throw new CustomException("El campo 'creation_date_to' debe tener una longitud de 10 o 13 caracteres.");
                }
            }


            if (data.ContainsKey("before"))
            {
                if (!(data["before"] is string) || ((string)data["before"]).Length != 25)
                {
                    throw new CustomException("El campo 'before' es inválido. La longitud debe ser de 25 caracteres.");
                }
            }

            if (data.ContainsKey("after"))
            {
                if (!(data["after"] is string) || ((string)data["after"]).Length != 25)
                {
                    throw new CustomException("El campo 'after' es inválido. La longitud debe ser de 25 caracteres.");
                }
            }

            if (data.ContainsKey("limit"))
            {
                if (!(data["limit"] is int) || (int)data["limit"] < 1 || (int)data["limit"] > 100)
                {
                    throw new CustomException("El filtro 'limit' admite valores en el rango 1 a 100");
                }
            }
            if (data.ContainsKey("creation_date_from") && data.ContainsKey("creation_date_to"))
            {
                Helper.ValidateDateFilter(data["creation_date_from"] as string, data["creation_date_to"] as string);
            }
        }

        public static void Update(Dictionary<string, object> data)
        {
            List<string> requiredPayload = new List<string> { "card_id" };
            Exception resultValidation = Helper.AdditionalValidation(data, requiredPayload);
            if (resultValidation != null)
            {
                throw new CustomException(resultValidation.Message);
            }
            else
            {
                if (!(data["card_id"] is string) || ((string)data["card_id"]).Length != 25)
                {
                    throw new CustomException("El campo 'card_id' es inválido. La longitud debe ser de 25.");
                }
                Helper.ValidateStringStart((string)data["card_id"], "crd");

                if (data.ContainsKey("metadata"))
                {
                    Dictionary<string, object> metadata = data["metadata"] as Dictionary<string, object>;
                    Helper.ValidateMetadata(metadata);
                }
            }
        }
    }
}

