using System.Runtime.InteropServices;
using BlazorWasm.Services;
using BlazorWasm.Services.Http;
using Shared;
namespace WebShop.Pages;

public class BasketService : Subject
{
    private static Dictionary<int, Product> BasketItems = new();

    private BasketService(): base()
    {
        
    }

    private static BasketService instance;
    public static BasketService getInstance() {
        if (instance == null) {
            instance = new BasketService();
        }

        return instance;
    }

    private IProductService _productService = ProductService.getInstance();

    public bool InBasketStock(int id) {
        foreach (var product in BasketItems.Values) {
            if (product.Id != id) continue;
            return product.amount > 0;
        }
        return true;
    }
    
    public int? GetProductAmount(int id) {
        if (BasketItems.ContainsKey(id)) {
            return BasketItems[id].amount;
        }
        return null;
    }
    
    public int? GetAmountInBasket()
    {
        return BasketItems.Values.Sum(product => product.quantity);
    }

    public async Task<bool> AddItem(int id)
    {
        if (!InBasketStock(id)) return false;
        foreach (var product in _productService.GetProducts()) {
            if (product.Id != id) continue;
            BasketItems.TryAdd(product.Id, product);
            //product.amount -= 1;

            Product basketProduct = BasketItems[product.Id];
            basketProduct.quantity += 1;
            basketProduct.amount -= 1;

            emit("Changed");
            return basketProduct.amount > 0; // No need to continue iterating once a match is found
        }
        return false;
    }

    public void RemoveItem(int id)
    {
        Product product = BasketItems[id];
        if (product.quantity <= 0) return;
        product.amount++;
        product.quantity--;
        if (product.quantity <= 0)
        {
            BasketItems.Remove(id);
        }
        emit("Changed");
    }

    public void RemoveAll()
    {
        BasketItems.Clear();
        emit("Changed");
    }
    
    public Dictionary<int, Product> GetBasketItems() {
        return BasketItems;
    }

    public double getBasketTotal()
    {
        double totalPrice = 0;
        foreach (Product product in BasketItems.Values) {
            totalPrice += product.Price * product.quantity;
        }
        return Math.Round(totalPrice,2);
    }
    
}