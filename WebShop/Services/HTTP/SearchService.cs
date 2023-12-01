using BlazorWasm.Services.Http;
using Newtonsoft.Json;
using Shared;
using WebShop.Pages;

namespace WebShop.Services.HTTP;

public class SearchService : Subject
{
    private readonly HttpClient client = new ();

    public SearchService() : base() {
        
    }

    private static SearchService instance;
    public static SearchService getInstance()
    {
        if (instance == null)
        {
            instance = new SearchService();
        }

        return instance;
    }
    
    
    private static List<Product> products;
    private ProductService _productService = ProductService.getInstance();
    private List<int> curretCategories = new();
    private string currentQuery = "";

    public string GetCurrentQuery()
    {
        return currentQuery;
    }

    public void SetCurrentQuery(string query)
    {
        currentQuery = query;
    }

    public List<int> GetCurrentCategories()
    {
        return curretCategories;
    }
    public void SetCurrentCategories(List<int> categories)
    {
        curretCategories = categories;
    }
    public async Task<List<Product>> Search()
    {
        HttpResponseMessage response = await client.GetAsync("http://localhost:8080/products?showFlagged=false&query=" + currentQuery + "&categories=" + String.Join(",", curretCategories));
        string responseContent = await response.Content.ReadAsStringAsync();
        products = JsonConvert.DeserializeObject<List<Product>>(responseContent);
        _productService.SetProducts(products);
        return products;
    }
    
    public async Task<List<Product>> test(string query)
    {
        HttpResponseMessage response = await client.GetAsync("http://localhost:8080/products/search?query=" + query);
        string responseContent = await response.Content.ReadAsStringAsync();
        products = JsonConvert.DeserializeObject<List<Product>>(responseContent);
        Console.WriteLine(responseContent);
        _productService.SetProducts(products);
        return products;
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
}