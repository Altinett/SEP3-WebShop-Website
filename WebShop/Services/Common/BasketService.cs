using BlazorWasm.Services;
using Shared;
namespace WebShop.Pages;

public class BasketService : Subject, IBasketService {
    private static Dictionary<int, Product> BasketItems = new();

    private readonly IProductService ProductService;
    public BasketService(IProductService productService) {
        ProductService = productService;
    }

    public bool InBasketStock(int id) {
        BasketItems.TryGetValue(id, out Product product);
        if (product == null) return true;

        return product.amount > 0;
    }

    public bool InBasket(int id) {
        BasketItems.TryGetValue(id, out Product product);
        return product != null;
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

    public bool AddItem(int id) {
        if (!InBasketStock(id)) return false;
        foreach (Product product in ProductService.GetProducts()) {
            if (product.Id != id) continue;
            BasketItems.TryAdd(product.Id, product);
            //product.amount -= 1;

            Product basketProduct = BasketItems[product.Id];
            basketProduct.quantity += 1;
            basketProduct.amount -= 1;

            emit("BasketChanged");
            emit("BasketProductAdded", id);
            return basketProduct.amount > 0; // No need to continue iterating once a match is found
        }
        return false;
    }

    public void RemoveItem(int id) {
        Product product = BasketItems[id];
        if (product.quantity <= 0) return;
        product.amount++;
        product.quantity--;
        if (product.quantity <= 0)
        {
            BasketItems.Remove(id);
        }
        emit("BasketChanged");
        emit("BasketProductRemoved", id);
    }

    public void Clear() {
        BasketItems.Clear();
        emit("BasketChanged");
    }
    
    public Dictionary<int, Product> GetBasketItems() {
        return BasketItems;
    }

    public Product GetBasketItem(int id) {
        return BasketItems[id];
    }

    public double GetBasketTotal()
    {
        double totalPrice = 0;
        foreach (Product product in BasketItems.Values) {
            totalPrice += product.Price * product.quantity;
        }
        return Math.Round(totalPrice,2);
    }

    public void OnChanged(Action<Object[]> callback) {
        on("BasketChanged", callback);
    }
}