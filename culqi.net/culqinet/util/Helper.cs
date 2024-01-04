using System;
using System.Text.RegularExpressions;

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
    }
}

    