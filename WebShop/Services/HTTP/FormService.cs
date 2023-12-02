using System.Runtime.InteropServices.JavaScript;
using System.Text;
using BlazorWasm.Services;
using Newtonsoft.Json;
using Shared;
using WebShop.Shared.DTOs;

namespace WebShop.Pages;

public class FormService : IFormService
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public int ZipCode { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; } 
    public string CardNumber { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int CVC { get; set; }
    public int OrderId { get; set; }
    public int Total { get; set; }

    private void getFormValues() {
        
    }
    
    private IBasketService BasketService;

    public FormService(IBasketService basketService) {
        BasketService = basketService;
    }

    private readonly HttpClient client = new ();
    
    public async Task SendForm() {
        Dictionary<int, int> products = new();

        foreach (var entry in BasketService.GetBasketItems()) {
            products.Add(entry.Key, entry.Value.quantity);
        }

        PaymentDto paymentDto = new() {
            products = products,
            firstname = FirstName,
            lastname = LastName,
            address = Address,
            postcode = ZipCode,
            phonenumber = PhoneNumber,
            email = Email,
            cardnumber = CardNumber,
            expirationdate = ExpirationDate,
            cvc = CVC,
            total = Total
        };
        
        // Convert DTO to JSON UTF8 string and post to ordering endpoint 
        string postAsJson = JsonConvert.SerializeObject(paymentDto);
        StringContent content = new(postAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("http://localhost:8080/orders/order", content);

        // Convert response to object
        string responseContent = await response.Content.ReadAsStringAsync();
        dynamic data = JsonConvert.DeserializeObject(responseContent);
        OrderId = data.orderId;
        
        if (!response.IsSuccessStatusCode) {
            throw new Exception(responseContent);
        }
    }

    public async Task<PaymentDto> GetOrder(string id)
    {
        HttpResponseMessage response = await client.GetAsync($"http://localhost:8080/orders/{id}");
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
        PaymentDto order = JsonConvert.DeserializeObject<PaymentDto>(responseContent);
        return order;
    }
}