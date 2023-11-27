using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using Shared;
using Shared.DTOs;

namespace BlazorWasm.Services.Http;

public class ProductService : IProductService
{
    private readonly HttpClient client = new ();
    
    private static ProductService instance;
    private static List<Product> products;
    
    public static ProductService getInstance()
    {
        if (instance == null)
        {
            instance = new ProductService();
        }
        return instance;
    }

    public async Task CreateProduct(string name, string Description, List<int> category_ids, int Price, int Amount, IBrowserFile image)
    {
        string base64Image = await ConvertImageToBase64(image);
        
        ProductCreationDto productCreationDto = new ProductCreationDto
        {
            name = name,
            description = Description,
            categoryIds = category_ids,
            price = Price,
            amount = Amount,
            image = base64Image
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
    
    private async Task<string> ConvertImageToBase64(IBrowserFile image)
    {
        using (var memoryStream = new MemoryStream())
        {
            await image.OpenReadStream().CopyToAsync(memoryStream);
            byte[] imageBytes = memoryStream.ToArray();
            return Convert.ToBase64String(imageBytes);
        }
    }

    public async Task<string> RemoveProduct(int Id)
    {
        HttpResponseMessage response = await client.DeleteAsync("http://localhost:8080/products/remove?id=" + Id);

        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        return responseContent;
    }
    
    public async Task EditProduct(int id, string name, string Description, /*List<int> category_ids,*/ int Price, int Amount)
    {
        ProductEditingDto productEditingDto = new ProductEditingDto
        {
            id = id,
            name = name,
            description = Description,
            /*categoryIds = category_ids,*/
            price = Price,
            amount = Amount

        };
        
        string postAsJson = JsonConvert.SerializeObject(productEditingDto);
        StringContent content = new StringContent(postAsJson, Encoding.UTF8, "application/json");
        Console.WriteLine(postAsJson);
        HttpResponseMessage response = await client.PostAsync("http://localhost:8080/products/edit", content);
        
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
    

    public async Task<List<Product>> UpdateProducts()
    {
        HttpResponseMessage response = await client.GetAsync("http://localhost:8080/products");
        string responseContent = await response.Content.ReadAsStringAsync();
        products = JsonConvert.DeserializeObject<List<Product>>(responseContent);
        return products;
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public List<Product> GetProducts()
    {
        return products;
    }
    
    public void SetProducts(List<Product> products)
    {
        ProductService.products = products;
    }
    
    public async Task<List<Product>> GetProductsByOrderId(string id)
    {
        Console.WriteLine("Get test");
        //List<Product> products = new List<Product>();
        //StringContent content = new StringContent(postAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.GetAsync($"http://localhost:8080/products/fromOrder?id={id}");
        
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
        List<Product> products = JsonConvert.DeserializeObject<List<Product>>(responseContent);
        Console.WriteLine(products);
        return products;
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        
    }
 
    public async Task<Product?> GetProductById(int? id)
    {
        HttpResponseMessage response = await client.GetAsync($"http://localhost:8080/products/{id}");
        string responseContent = await response.Content.ReadAsStringAsync();
        Product? product = JsonConvert.DeserializeObject<Product>(responseContent);
        Console.WriteLine("Product: " + product);
        return product;
    }

    public Boolean IsInStok(Product product)
    {
        return true;
    }
}