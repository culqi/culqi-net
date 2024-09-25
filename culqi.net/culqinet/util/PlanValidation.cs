using System;
using System.Text.RegularExpressions;
using culqi.net;

namespace culqinet.util
{
    public static class PlanValidation
    {
        public static void Create(Dictionary<string, object> data)
        {
            List<string> requiredPayload = new List<string> { "short_name", "description", "amount", "currency", "interval_unit_time", "interval_count", "initial_cycles", "name" };
            Exception resultValidation = Helper.AdditionalValidation(data, requiredPayload);
            if (resultValidation != null)
            {
                throw new CustomException(resultValidation.Message);
            }
            else
            {
                List<int> valuesIntervalUnitTime = new List<int> { 1, 2, 3, 4, 5, 6 };
                if (!data.ContainsKey("interval_unit_time") || !(data["interval_unit_time"] is int) || !valuesIntervalUnitTime.Contains((int)data["interval_unit_time"]))
                {
                    throw new CustomException("El campo 'interval_unit_time' tiene un valor inválido o está vacío. Estos son los únicos valores permitidos: [ 1, 2, 3, 4, 5, 6]");
                }

                int[] rangeIntervalCount = Enumerable.Range(0, 10000).ToArray();
                if (!data.ContainsKey("interval_count") || !(data["interval_count"] is int) || !rangeIntervalCount.Contains((int)data["interval_count"]))
                {
                    throw new CustomException("El campo 'interval_count' solo admite valores numéricos en el rango 0 a 9999.");
                }

                if (!data.ContainsKey("amount") || !(data["amount"] is int))
                {
                    throw new CustomException("El campo 'amount' es inválido o está vacío, debe tener un valor numérico.");
                }

                Exception validateParameterCurrency = Helper.ValidateEnumCurrency(data["currency"].ToString());
                if (validateParameterCurrency != null)
                {
                    throw new CustomException(validateParameterCurrency.Message);
                }

                int[] rangeName = Enumerable.Range(5, 47).ToArray();
                if (!data.ContainsKey("name") || !(data["name"] is string) || !rangeName.Contains(((string)data["name"]).Length))
                {
                    throw new CustomException("El campo 'name' es inválido o está vacío. El valor debe tener un rango de 5 a 50 caracteres.");
                }

                int[] rangeDescription = Enumerable.Range(5, 246).ToArray();
                if (!data.ContainsKey("description") || !(data["description"] is string) || !rangeDescription.Contains(((string)data["description"]).Length))
                {
                    throw new CustomException("El campo 'description' es inválido o está vacío. El valor debe tener un rango de 5 a 250 caracteres.");
                }

                int[] rangeShortName = Enumerable.Range(5, 47).ToArray();
                if (!data.ContainsKey("short_name") || !(data["short_name"] is string) || !rangeShortName.Contains(((string)data["short_name"]).Length))
                {
                    throw new CustomException("El campo 'short_name' es inválido o está vacío. El valor debe tener un rango de 5 a 50 caracteres.");
                }

                Dictionary<string, object> initialCyclesData = data["initial_cycles"] as Dictionary<string, object>;
                Exception validParameterInitialCycle = Helper.ValidateInitialCyclesParameters(initialCyclesData);
                if (validParameterInitialCycle != null)
                {
                    throw new CustomException(validParameterInitialCycle.Message);
                }

                bool hasInitialCharge = Convert.ToBoolean(initialCyclesData["has_initial_charge"]);
                int payAmount = Convert.ToInt32(initialCyclesData["amount"]);
                int count = Convert.ToInt32(initialCyclesData["count"]);

                Helper.ValidateInitialCycles(hasInitialCharge, data["currency"].ToString(), count);

                if (data.ContainsKey("image"))
                {
                    string image = data["image"].ToString();
                    Helper.ValidateImage(image);
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
            if (data.ContainsKey("status"))
            {
                List<int> valuesStatus = new List<int> { 1, 2 };
                if (!(data["status"] is int) || !valuesStatus.Contains((int)data["status"]))
                {
                    throw new CustomException("El campo 'status' tiene un valor inválido o está vacío. Estos son los únicos valores permitidos: [1, 2].");
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
                    throw new CustomException("El filtro 'limit' admite valores en el rango 1 a 100.");
                }
            }

            if (data.ContainsKey("max_amount"))
            {
                if (!(data["max_amount"] is int))
                {
                    throw new CustomException("El filtro 'max_amount' es invalido, debe tener un valor numérico entero.");
                }
            }

            if (data.ContainsKey("min_amount"))
            {
                if (!(data["min_amount"] is int))
                {
                    throw new CustomException("El filtro 'min_amount' es invalido, debe tener un valor numérico entero.");
                }
            }

            if (data.ContainsKey("creation_date_from") && data.ContainsKey("creation_date_to"))
            {
                Helper.ValidateDateFilter(data["creation_date_from"] as string, data["creation_date_to"] as string);
            }
        }

        public static void Update(Dictionary<string, object> data)
        {
            if (data.ContainsKey("name"))
            {
                int[] rangeName = Enumerable.Range(5, 47).ToArray();
                if (!(data["name"] is string) || !rangeName.Contains(((string)data["name"]).Length))
                {
                    throw new CustomException("El campo 'name' es inválido o está vacío. El valor debe tener un rango de 5 a 50 caracteres.");
                }
            }

            if (data.ContainsKey("description"))
            {
                int[] rangeDescription = Enumerable.Range(5, 246).ToArray();
                if (!(data["description"] is string) || !rangeDescription.Contains(((string)data["description"]).Length))
                {
                    throw new CustomException("El campo 'description' es inválido o está vacío. El valor debe tener un rango de 5 a 250 caracteres.");
                }
            }

            if (data.ContainsKey("short_name"))
            {
                int[] rangeShortName = Enumerable.Range(5, 47).ToArray(); 
                if (!(data["short_name"] is string) || !rangeShortName.Contains(((string)data["short_name"]).Length))
                {
                    throw new CustomException("El campo 'short_name' es inválido o está vacío. El valor debe tener un rango de 5 a 50 caracteres.");
                }
            }

            if (data.ContainsKey("image"))
            {
                string image = data["image"].ToString();
                Helper.ValidateImage(image);
            }

            if (data.ContainsKey("metadata"))
            {
                Dictionary<string, object> metadata = data["metadata"] as Dictionary<string, object>;
                Helper.ValidateMetadata(metadata);
            }

            if (data.ContainsKey("status"))
            {
                List<int> valuesStatus = new List<int> { 1, 2 };
                if (!(data["status"] is int) || !valuesStatus.Contains((int)data["status"]))
                {
                    throw new CustomException("El campo 'status' tiene un valor inválido o está vacío. Estos son los únicos valores permitidos: [ 1, 2 ]");
                }
            }

        }
    }
}

