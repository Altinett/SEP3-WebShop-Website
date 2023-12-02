using System.Text.RegularExpressions;
using BlazorWasm.Services;
using WebShop.Shared.Exceptions;

public class ValidationService : IValidationService {
    private Regex _emailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    private Regex _addressRegex = new(@"^\d{1,3}.,.[a-zA-Z]+$");
    private Regex _creditCardRegex = new(@"^\d{8,21}$");

    public string ValidateName(string name) {
        if (string.IsNullOrEmpty(name)) {
            Error("Fornavn og efternavn må ikke være tom");
        }
        if (!name.All(char.IsLetter)) {
            Error("Fornavn og efternavn må kun indeholde bogstaver");
        }
        return name;
    }
    
    public string ValidateAddress(string address) {
        if (string.IsNullOrEmpty(address)) {
            Error("Addressen må ikke være tom");
        }
        if (!_addressRegex.IsMatch(address)) {
            Error("Addresen skal først være nummeret, herefter et komma og så et navn. \\n eksempel: 37,berringsvej");
        }
        return address;
    }
    
    public int ValidateZipCode(int zipcode) { 
        if (zipcode <= 1000 || zipcode > 10000) {
            Error("Postnummeret skal være mellem 1000 og 10000");
        }
        return zipcode;
    }
    
    public int ValidatePhoneNumber(int phoneNumber) { 
        if (phoneNumber <= 10000000 || phoneNumber > 99999999) {
            Error("Telefon nummeret skal være mellem 8 cifre og uden -");
        }
        return phoneNumber;
    }
    
    public string ValidateEmail(string email) {
        if (string.IsNullOrEmpty(email)) {
            Error("Email må ikke være tom");
        }
        if (!_emailRegex.IsMatch(email)) {
            Error("Invalid email format");
        }
        return email;
    }

    public string ValidateCardNumber(string creditCard) {
        if (string.IsNullOrEmpty(creditCard)) {
            Error("Kortnummer må ikke være tom");
        }
        if (!_creditCardRegex.IsMatch(creditCard)) {
            Error("Kortnummer skal være mellem 8 og 21 cifre");
        }
        return creditCard;
    }

    public DateTime ValidateExpirationDate(DateTime expirationDate) {
        if (ConvertToUnixTimestamp(expirationDate) <= ConvertToUnixTimestamp(DateTime.Today)) {
            Error("Udløbsdatoen skal være nyere end i dag");
        }
        return expirationDate;
    }

    public int ValidateCvc(int cvc) {
        if (cvc <= 100 || cvc > 999) {
            Error("CVC nummeret skal være mellem 100 og 999");
        }
        return cvc;
    }
    
    private long ConvertToUnixTimestamp(DateTime date) {
        return ((DateTimeOffset)date).ToUnixTimeMilliseconds();
    }
    private void Error(string message) {
        throw new InvalidInputException(message);
    }
}