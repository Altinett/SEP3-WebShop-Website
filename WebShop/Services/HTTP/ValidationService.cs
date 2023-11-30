using System;
using System.Text.RegularExpressions;

namespace WebShop.Services.HTTP
{
    public class ValidationService
    {
        private static ValidationService instance;
        private static Regex emailRegex;
        private static Regex addressRegex;
        private static Regex creditcardRegex;
        private DateTime currentTime = DateTime.Today;

        private ValidationService()
        {
            // Initialize the emailRegex with your desired pattern
            emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            addressRegex = new Regex(@"^\d{1,3}.,.[a-zA-Z]+$");
            creditcardRegex = new Regex(@"^\d{8,21}$");
        }

        public static ValidationService GetInstance()
        {
            if (instance == null)
            {
                instance = new ValidationService();
            }
            return instance;
        }

        private Action<string> NullOrEmpty(string message) {
            return (name) => {
                bool result = string.IsNullOrEmpty(name);
                if (result) ThrowError(message);
            };
        }

        private Action<string> OnlyLetters(string message) {
            return (name) => {
                bool result = name.All(char.IsLetter);
                if (!result) ThrowError(message);
            };
        }

        private void Filter(string value, List<Action<string>> filters) {
            foreach (var filter in filters) {
                filter(value);
            }
        }

        public string ValidateName(string name)
        {
            /*
            Filter(name, new List<Action<string>> {
                NullOrEmpty("Fornavn og efternavn må ikke være tom"),
                OnlyLetters("Fornavn og efternavn må kun indeholde bogstaver")
            });*/
            
            if (string.IsNullOrEmpty(name)) {
                ThrowError("Fornavn og efternavn må ikke være tom");
            }
            if (!name.All(char.IsLetter)) {
                ThrowError("Fornavn og efternavn må kun indeholde bogstaver");
            }
            
            return name;
        }
        
        public string ValidateAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                ThrowError("Addressen må ikke være tom");
            }
            if (!addressRegex.IsMatch(address))
            {
                ThrowError("Addresen skal først være nummeret, herefter et komma og så et navn. \\n eksempel: 37,berringsvej");
            }
            return address;
        }
        
        public int ValidateZipCode(int zipcode)
        { 
            if (zipcode <= 1000 || zipcode > 10000)
            {
                ThrowError("Postnummeret skal være mellem 1000 og 10000");
            }
            return zipcode;
        }
        
        public int ValidatePhonenumber(int phonenumber)
        { 
            if (phonenumber <= 10000000 || phonenumber > 99999999)
            {
                ThrowError("Telefon nummeret skal være mellem 8 cifre og uden -");
            }
            return phonenumber;
        }


        public string ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ThrowError("Email må ikke være tom");
            }
            if (!emailRegex.IsMatch(email))
            {
                ThrowError("Invalid email format");
            }

            return email;
        }

        public string ValidateCreditcard(string creditcard)
        {
            if (string.IsNullOrEmpty(creditcard))
            {
                ThrowError("Kreditkort må ikke være tom");
            }
            if (!creditcardRegex.IsMatch(creditcard))
            {
                ThrowError("Kreditkort skal være mellem 8 og 21 cifre");
            }
            return creditcard;
        }

        public DateTime ValidateExperationDate(DateTime experation)
        {
            if (ConvertToUnixTimestamp(experation) <= ConvertToUnixTimestamp(currentTime))
            {
                ThrowError("Udløbsdatoen skal være nyere end i dag");
            }
            return experation;
        }
        
        private long ConvertToUnixTimestamp(DateTime date)
        {
            return ((DateTimeOffset)date).ToUnixTimeMilliseconds();
            /*
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(((DateTimeOffset)date).ToUnixTimeMilliseconds() - unixEpoch).TotalSeconds;
            */
        }

        public int ValidateCVC(int cvc)
        {
            if (cvc <= 100 || cvc > 999)
            {
                 ThrowError("CVC nummeret skal være mellem 100 og 999");
            }
            return cvc;
        }

        private void ThrowError(string message)
        {
            throw new InvalidInputException(message);
        }


    }
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message)
        {
        }

        
    }
}