using WebShop.Shared.DTOs;

namespace BlazorWasm.Services; 

public interface IFormService {
	string FirstName { get; set; }
	string LastName { get; set; }
	string Address { get; set; }
	int ZipCode { get; set; }
	int PhoneNumber { get; set; }
	string Email { get; set; } 
	string CardNumber { get; set; }
	DateTime ExpirationDate { get; set; }
	int CVC { get; set; }
	int OrderId { get; set; }
	int Total { get; set; }
	string OrderDate { get; set; }
	Task SendForm();
	Task<PaymentDto> GetOrder(string id);
}