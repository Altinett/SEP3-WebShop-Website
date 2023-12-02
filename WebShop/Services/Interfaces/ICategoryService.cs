using WebShop.Services.HTTP;
using WebShop.Shared.DTOs;

namespace BlazorWasm.Services;

public interface ICategoryService
{
    List<CategoryDto> GetCategories();
    Task<List<CategoryDto>> UpdateCategory();
}