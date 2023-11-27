using BlazorWasm.Services;
using Newtonsoft.Json;
using WebShop.Shared.DTOs;

namespace WebShop.Services.HTTP;

public class CategoryService : ICategoryService
{
    private readonly HttpClient client = new ();
    private static List<CategoryDto> categorys;
    private static CategoryService instance;
    
    public static CategoryService getInstance()
    {
        if (instance == null)
        {
            instance = new CategoryService();
        }
        return instance;
    }
    
    public List<CategoryDto> GetCategories()
    {
        return categorys;
    }
    
    public async Task<List<CategoryDto>> UpdateCategory()
    {	
        HttpResponseMessage response = await client.GetAsync("http://localhost:8080/categories"); 
        string responseContent = await response.Content.ReadAsStringAsync();
        categorys = JsonConvert.DeserializeObject<List<CategoryDto>>(responseContent);
        return categorys;
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
    
    
}