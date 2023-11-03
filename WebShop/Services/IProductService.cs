namespace BlazorWasm.Services;

public interface IProductService
{
    public Task CreateProduct(string name, string Description, List<int> checkedValues, int Price, int Amount);

    public Task RemoveProduct(int Id);
    
    //public Task GetUser(int ownerId)
}