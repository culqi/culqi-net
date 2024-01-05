using System;
using System.Text.RegularExpressions;
using culqi.net;

namespace culqinet.util
{
    public static class CustomerValidation
    {
        public static void Create(Dictionary<string, object> data)
        {
            string firstName = data.TryGetValue("first_name", out var temp) ? (string)temp : null;
            string lastName = data.TryGetValue("last_name", out temp) ? (string)temp : null;
            string address = data.TryGetValue("address", out temp) ? (string)temp : null;
            string addressCity = data.TryGetValue("address_city", out temp) ? (string)temp : null;
            string email = data.TryGetValue("email", out temp) ? (string)temp : null;

            if (string.IsNullOrEmpty(firstName))
            {
                throw new CustomException("first name is empty.");
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new CustomException("last name is empty.");
            }

            if (string.IsNullOrEmpty(address))
            {
                throw new CustomException("address is empty.");
            }

            if (string.IsNullOrEmpty(addressCity))
            {
                throw new CustomException("address_city is empty.");
            }

            if (!(data.TryGetValue("phone_number", out var phoneNumber) && phoneNumber is string))
            {
                throw new CustomException("Invalid 'phone_number'. It should be a string.");
            }

            List<string> countryCodes = Util.GetCountryCodes();
            Helper.ValidateValue(data["country_code"] as string, countryCodes);

            if (!Helper.IsValidEmail(email))
            {
                throw new CustomException("Invalid email.");
            }
        }

        public static void List(Dictionary<string, object> data)
        {
            if (data.ContainsKey("email"))
            {
                string email = data["email"] as string;
                if (!Helper.IsValidEmail(email))
                {
                    throw new CustomException("Invalid email.");
                }
            }
            if (data.ContainsKey("country_code"))
            {
                List<string> countryCodes = Util.GetCountryCodes();
                Helper.ValidateValue(data["country_code"] as string, countryCodes);
            }
        }
    }
}

    