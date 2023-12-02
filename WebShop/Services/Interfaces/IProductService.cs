using BlazorWasm.Services.Http;
using Microsoft.AspNetCore.Components.Forms;
using Shared;

namespace BlazorWasm.Services;

public interface IProductService
{
    List<Product> Products { get; set; }
    Product LastReviewedProduct { get; set; }
    Task CreateProduct(string name, string Description, List<int> category_ids, double Price, int Amount, IBrowserFile Image);

    Task<string> RemoveProduct(int? Id);

    Task EditProduct(int id, string name, string Description, /*List<int> category_ids,*/ double Price, int Amount, IBrowserFile Image, string savedImage);
    
    Task<List<Product>> GetProductsByOrderId(string id);
    Task<List<Product>> UpdateProducts(); // Get latest products from api
    
    
    List<Product> GetProducts(/*Maybe make it so that it can do pages???*/); // Get products from variable

    Task<Product?> GetProductById(int? id);

}