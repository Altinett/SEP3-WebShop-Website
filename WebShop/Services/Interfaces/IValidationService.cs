namespace BlazorWasm.Services; 

public interface IValidationService {
	string ValidateName(string name);
	string ValidateAddress(string address);
	int ValidateZipCode(int zipcode);
	int ValidatePhoneNumber(int phoneNumber);
	string ValidateEmail(string email);
	string ValidateCreditCard(string creditCard);
	int ValidateCvc(int cvc);
	DateTime ValidateExpirationDate(DateTime expirationDate);
}