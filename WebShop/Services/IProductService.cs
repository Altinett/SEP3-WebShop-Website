using Shared;

namespace BlazorWasm.Services;

public interface IProductService
{
    public Task CreateProduct(string name, string Description, List<int> category_ids, int Price, int Amount);

    public Task RemoveProduct(int Id);

    public Task EditProduct(int id, string name, string Description, /*List<int> category_ids,*/ int Price,
        int Amount);
    
    public Task<List<Product>> GetProducts(/*Maybe make it so that it can do pages???*/);

    //public Task GetUser(int ownerId)
}