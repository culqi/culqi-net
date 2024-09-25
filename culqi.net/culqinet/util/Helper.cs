using System;
using System.Text.RegularExpressions;
using System.Text.Json;


namespace culqinet.util
{
    public class Helper
    {
        public static bool IsValidCardNumber(string number)
        {
            return Regex.IsMatch(number, "^\\d{13,19}$");
        }

        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, "^\\S+@\\S+\\.\\S+$");
        }

        public static void ValidateCurrencyCode(string currencyCode)
        {
            if (string.IsNullOrEmpty(currencyCode))
            {
                throw new CustomException("Currency code is empty.");
            }

            List<string> allowedValues = new List<string> { "PEN", "USD" };
            if (!allowedValues.Contains(currencyCode))
            {
                throw new CustomException("Currency code must be either \"PEN\" or \"USD\".");
            }
        }

        public static void ValidateStringStart(string str, string start)
        {
            if (!(str.StartsWith(start + "_test_") || str.StartsWith(start + "_live_")))
            {
                throw new CustomException($"Incorrect format. The format must start with {start}_test_ or {start}_live_");
            }
        }

        public static void ValidateId(string id)
        {

            var data = new { id = id };
            if (data.id == null || data.id.ToString() == "" || data.id.ToString() == "undefined")
            {
                throw new CustomException($"El campo 'id' es requerido y no está presente");
            }
            if (string.IsNullOrEmpty(id) || id.Length != 25)
            {
                throw new CustomException($"El campo 'id' es inválido. La longitud debe ser de 25 caracteres.");
            }
        }


        public static void ValidateValue(string value, List<string> allowedValues)
        {
            if (!allowedValues.Contains(value))
            {
                throw new CustomException($"Invalid value. It must be one of {string.Join(", ", allowedValues)}");
            }
        }

        public static bool IsFutureDate(long expirationDate)
        {
            DateTimeOffset expDate = DateTimeOffset.FromUnixTimeSeconds(expirationDate);
            return expDate > DateTimeOffset.Now;
        }

        public static void ValidateDateFilter(string dateFrom, string dateTo)
        {
            if (!int.TryParse(dateFrom, out int parsedDateFrom) ||
                !int.TryParse(dateTo, out int parsedDateTo))
            {
                throw new CustomException("Invalid value. Date_from and Date_to must be integers.");
            }

            if (parsedDateTo < parsedDateFrom)
            {
                throw new CustomException("Invalid value. Date_from must be less than Date_to.");
            }
        }
        public static void ValidateAmountValue(object amountObj)
        {
            if (amountObj is int)
            {
                // Amount is already an integer, no further validation needed.
            }
            else if (amountObj is string amountStr && int.TryParse(amountStr, out int _))
            {
                // Successfully parsed string to integer
            }
            else
            {
                throw new CustomException("Invalid 'amount'. It should be an integer or a string representing an integer.");
            }
        }

        public static Exception AdditionalValidation(Dictionary<string, object> data, List<string> requiredFields)
        {
            foreach (var field in requiredFields)
            {
                if (!data.ContainsKey(field) || data[field] == null || data[field].ToString() == "" || data[field].ToString() == "undefined")
                {
                    return new Exception($"El campo '{field}' es requerido y no está presente");
                }
            }

            return null;
        }

        public static Exception ValidateInitialCyclesParameters(Dictionary<string, object> initialCycles)
        {
            string[] parametersInitialCycles = { "count", "has_initial_charge", "amount", "interval_unit_time" };

            foreach (string field in parametersInitialCycles)
            {
                if (!initialCycles.ContainsKey(field))
                {
                    throw new Exception($"El campo obligatorio '{field}' no está presente en 'initial_cycles'.");
                }
            }

            if (!(initialCycles["count"] is int))
            {
                throw new Exception("El campo 'initial_cycles.count' es inválido o está vacío, debe tener un valor numérico.");
            }

            if (!(initialCycles["has_initial_charge"] is bool))
            {
                throw new Exception("El campo 'initial_cycles.has_initial_charge' es inválido o está vacío. El valor debe ser un booleano (true o false).");
            }

            if (!(initialCycles["amount"] is int))
            {
                throw new Exception("El campo 'initial_cycles.amount' es inválido o está vacío, debe tener un valor numérico.");
            }

            int[] valuesIntervalUnitTime = { 1, 2, 3, 4, 5, 6 };

            if (!(initialCycles["interval_unit_time"] is int) || !valuesIntervalUnitTime.Contains((int)initialCycles["interval_unit_time"]))
            {
                throw new Exception("El campo 'initial_cycles.interval_unit_time' tiene un valor inválido o está vacío. Estos son los únicos valores permitidos: [1, 2, 3, 4, 5, 6]");
            }
            return null;
        }

        public static Exception ValidateEnumCurrency(string currency)
        {
            string[] allowedValues = { "PEN", "USD" };
            if (allowedValues.Contains(currency))
            {
                return null; // El valor está en la lista, no hay error
            }

            // Si llega aquí, significa que el valor no está en la lista
            return new CustomException($"El campo 'currency' es inválido o está vacío, el código de la moneda en tres letras (Formato ISO 4217). Culqi actualmente soporta las siguientes monedas: {string.Join(", ", allowedValues)}.");
        }

        public static void ValidateInitialCycles(bool hasInitialCharge, string currency, int count)
        {
            if (hasInitialCharge)
            {

                if (!(1 <= count && count <= 9999))
                {
                    throw new CustomException("El campo 'initial_cycles.count' solo admite valores numéricos en el rango 1 a 9999.");
                }

            }
            else
            {
                if (!(0 <= count && count <= 9999))
                {
                    throw new CustomException("El campo 'initial_cycles.count' solo admite valores numéricos en el rango 0 a 9999.");
                }

            }
        }

        public static void ValidateImage(string image)
        {
            // Expresión regular para validar URLs
            string regexImage = @"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-zA-Z0-9]+([-.]{1}[a-zA-Z0-9]+)*\.[a-zA-Z]{2,5}(:[0-9]{1,5})?(\/.*)?$";

            // Verificar si 'image' es una cadena y cumple con los criterios de validación
            if (!(image is string) || !(5 <= image.Length && image.Length <= 250) || !Regex.IsMatch(image, regexImage))
            {
                // La imagen no cumple con los criterios de validación
                throw new CustomException("El campo 'image' es inválido. Debe ser una cadena y una URL válida.");
            }
        }

        public static Exception ValidateMetadata(Dictionary<string, object> metadata)
        {
            // Permitir un diccionario vacío para el campo metadata
            if (metadata == null)
            {
                throw new CustomException("Enviaste el campo metadata con un formato incorrecto.");
            }

            if (!metadata.Any())
            {
                return null;
            }

            // Verificar límites de longitud de claves y valores
            Exception errorLength = ValidateKeyAndValueLength(metadata);
            if (errorLength != null)
            {
                throw new CustomException(errorLength.ToString());
            }

            // Convertir el diccionario transformado a JSON
            try
            {
                JsonSerializer.Serialize(metadata);
            }
            catch (System.Text.Json.JsonException e)
            {
                string errorMessage = $"Error al serializar el diccionario a JSON. Mensaje de error: {e.Message}";
                throw new CustomException(errorMessage);
            }

            return null;
        }

        public static Exception ValidateKeyAndValueLength(Dictionary<string, object> objMetadata)
        {
            int maxKeyLength = 30;
            int maxValueLength = 200;

            foreach (var kvp in objMetadata)
            {
                string keyStr = kvp.Key.ToString();
                string valueStr = kvp.Value?.ToString();

                // Verificar límites de longitud de claves
                if (!(1 <= keyStr.Length && keyStr.Length <= maxKeyLength) ||
                    !(1 <= (valueStr?.Length ?? 0) && (valueStr?.Length ?? 0) <= maxValueLength))
                {
                    string errorMessage = $"El objeto 'metadata' es inválido, límite key (1 - {maxKeyLength}), value (1 - {maxValueLength}).";
                    throw new CustomException(errorMessage);
                }
            }

            return null;
        }
    }
}

