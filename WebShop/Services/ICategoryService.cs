using WebShop.Services.HTTP;
using WebShop.Shared.DTOs;

namespace BlazorWasm.Services;

public interface ICategoryService
{
    public List<CategoryDto> GetCategories();
    public Task<List<CategoryDto>> UpdateCategory();
}