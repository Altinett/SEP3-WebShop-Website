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
    public int orderId { get; set; }
    
    public int Total { get; set; }
    
    public string Date { get; set; }
    
    
    public static FormService getInstance()
    {
        if (instance == null)
        {
            instance = new FormService();
        }
        return instance;
    }
    
    private FormService()
    {
        
    }

    private void getFormValues()
    {
        
    }

    private readonly HttpClient client = new ();
    
    public async Task sendToLogic()
    {
        BasketService basketService = BasketService.getInstance();

        List<int> ids = new List<int>();
        Dictionary<int, int> productsDictionary = new Dictionary<int, int>();

        foreach (var entry in basketService.GetBasketItems())
        {
            ids.Add(entry.Value.Id);
        }
        /*
        foreach (Product item in basketService.GetBasketItems())
        {
            ids.Add(item.Id);

            //if (productsDictionary.ContainsKey(item.Id))
            //{
                // Key already exists, update the quantity
              //  productsDictionary[item.Id] += item.quantity;
            //}
            //else
            //{
                // Key doesn't exist, add it to the dictionary
                productsDictionary.Add(item.Id, item.quantity);
           // }
        }*/

        Console.WriteLine("Before the PaymentDTO");
        PaymentDto dto = new PaymentDto
        {
            //productIds = ids,
            products = productsDictionary,
            //products = new Dictionary<int, int> {
                //{2, 4},
                //{1, 2}
            //},
            firstname = FirstName,
            lastname = LastName,
            address = Address,
            postcode = ZipCode,
            phonenumber = PhoneNumber,
            email = Email,
            cardnumber = CardNumber,
            expirationdate = ExpirationDate,
            cvc = CVC,
            total = Total,
            date = Date
        };
        Console.WriteLine("after The paymentDTO, aber before Json");
        
        string postAsJson = JsonConvert.SerializeObject(dto);
        StringContent content = new StringContent(postAsJson, Encoding.UTF8, "application/json");
        Console.WriteLine(postAsJson);
        HttpResponseMessage response = await client.PostAsync("http://localhost:8080/orders/order", content);

        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine("response:");
        dynamic data = JsonConvert.DeserializeObject(responseContent);
        Console.WriteLine(data);
        orderId = data.orderId;
        
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public async Task<PaymentDto> GetFromLogic(string id)
    {
        HttpResponseMessage response = await client.GetAsync($"http://localhost:8080/orders/{id}");
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
        PaymentDto order = JsonConvert.DeserializeObject<PaymentDto>(responseContent);
        return order;
    }
}