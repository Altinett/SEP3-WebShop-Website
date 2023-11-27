using BlazorWasm.Services.Http;
using Microsoft.AspNetCore.Components.Forms;
using Shared;

namespace BlazorWasm.Services;

public interface IProductService
{
    public Task CreateProduct(string name, string Description, List<int> category_ids, double Price, int Amount, IBrowserFile Image);

    public Task<string> RemoveProduct(int? Id);

    public Task EditProduct(int id, string name, string Description, /*List<int> category_ids,*/ double Price, int Amount, IBrowserFile Image, string savedImage);
    
    public List<Product> GetProducts(/*Maybe make it so that it can do pages???*/); // Get products from variable
    public Task<List<Product>> UpdateProducts(); // Get latest products from api

    public Task<Product?> GetProductById(int? id);

    //public Task GetUser(int ownerId)
}