using BlazorWasm.Services;
using BlazorWasm.Services.Http;
using Newtonsoft.Json;
using Shared;
using WebShop.Pages;

namespace WebShop.Services.HTTP;

public class SearchService : Subject, ISearchService {
    public string Query { get; set; } = "";
    public List<int> Categories { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public int Page { get; set; } = 1;
    public int Min { get; set; }
    public int Max { get; set; }
    
    private readonly HttpClient Client = new();
    private readonly IProductService ProductService;
    public SearchService(IProductService productService) {
        ProductService = productService;
    }

    private string PreviousUri { get; set; } = "";
    public async Task<List<Product>> Search() {
        string uri = $"http://localhost:8080/products?showFlagged=false&page={Page}&query={Query}";
        uri += Categories.Count > 0 ? $"&categories={String.Join(",", Categories)}" : "";
        uri += Max > 0 ? $"&max={Max}" : "";
        uri += Min > 0 ? $"&min={Min}" : "";
        
        if (PreviousUri != uri) {
            HttpResponseMessage response = await Client.GetAsync(uri);
            string responseContent = await response.Content.ReadAsStringAsync();
            Products = JsonConvert.DeserializeObject<List<Product>>(responseContent);
            ProductService.Products = Products;
            PreviousUri = uri;
        }
        
        emit("Search");
        return Products;
    }
    
    public void NextPage() {
        Page++;
    }

    public void PreviousPage() {
        if (Page == 1) {
            return;
        } 
        Page--;
    }
    

    public void OnSearch(Action<Object[]> callback) {
        on("Search", callback);
    }
}