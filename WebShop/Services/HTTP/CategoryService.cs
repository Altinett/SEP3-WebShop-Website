using BlazorWasm.Services;
using Newtonsoft.Json;
using WebShop.Shared.DTOs;

namespace WebShop.Services.HTTP;

public class CategoryService : ICategoryService
{
    private readonly HttpClient Client = new();
    private List<CategoryDto> Categories;
    
    public List<CategoryDto> GetCategories() {
        return Categories;
    }
    
    public async Task<List<CategoryDto>> UpdateCategory() {
        HttpResponseMessage response = await Client.GetAsync("http://localhost:8080/categories"); 
        string responseContent = await response.Content.ReadAsStringAsync();
        Categories = JsonConvert.DeserializeObject<List<CategoryDto>>(responseContent);
        return Categories;
    }
    
    
}