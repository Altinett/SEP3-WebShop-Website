using System.Runtime.InteropServices.JavaScript;
using System.Text;
using Newtonsoft.Json;
using Shared;
using WebShop.Shared.DTOs;

namespace WebShop.Pages;

public class FormService
{
    private static FormService instance;

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public int ZipCode { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; } 
    public int CardNumber { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public int CVC { get; set; }
    
    
    public static FormService getInstance()
    {
        if (instance == null)
        {
            instance = new FormService();
        }
        return instance;
    }
    //din mor
    private FormService()
    {
        
    }

    private void getFormValues()
    {
        
    }

    private readonly HttpClient client = new ();
    
    public async Task sendToLogic()
    {
        BasketService.getInstance().GetBasketItems();

        List<int> ids = new List<int>();


        foreach (Product item in BasketService.getInstance().GetBasketItems())
        {
            ids.Add(item.Id);
        }
            
        PaymentDto dto = new PaymentDto
        {
            productIds = ids,
            firstname = FirstName,
            lastname = LastName,
            address = Address,
            postcode = ZipCode,
            phonenumber = PhoneNumber,
            email = Email,
            cardnumber = CardNumber,
            expirationdate = ExpirationDate,
            cvc = CVC
        };
        
        string postAsJson = JsonConvert.SerializeObject(dto);
        StringContent content = new StringContent(postAsJson, Encoding.UTF8, "application/json");
        Console.WriteLine(postAsJson);
        HttpResponseMessage response = await client.PostAsync("http://localhost:8080/orders/order", content);
        
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine("response:");
        Console.WriteLine(responseContent);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
}