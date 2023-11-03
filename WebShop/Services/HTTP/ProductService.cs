using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using Shared.DTOs;

namespace BlazorWasm.Services.Http;

public class ProductService : IProductService
{
    private readonly HttpClient client = new ();


    public async Task CreateProduct(string name, string Description, List<int> checkedValues, int Price, int Amount)
    {
        ProductCreationDto productCreationDto = new ProductCreationDto
        {
            Name = name,
            Description = Description,
            CheckedValues = checkedValues,
            Price = Price,
            Amount = Amount

        };
        
        string postAsJson = JsonConvert.SerializeObject(productCreationDto);
        StringContent content = new StringContent(postAsJson, Encoding.UTF8, "application/json");
        Console.WriteLine(postAsJson);
        HttpResponseMessage response = await client.PostAsync("http://localhost:8080/products/add", content);
        
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public async Task RemoveProduct(int Id)
    {
        HttpResponseMessage response = await client.PostAsync("http://localhost:8080/products/remove?id=" + Id, null);

        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
}