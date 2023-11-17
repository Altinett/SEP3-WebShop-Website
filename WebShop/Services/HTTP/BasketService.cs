using System.Runtime.InteropServices;
using Shared;
namespace WebShop.Pages;

public class BasketService : Subject
{
    private static List<Product> _basketItems = new List<Product>();

    private BasketService(): base()
    {
        
    }

    private static BasketService instance;
    public static BasketService getInstance()
    {
        if (instance == null)
        {
            instance = new BasketService();
        }

        return instance;
    }

    public void AddItem(Product item)
    {
        _basketItems.Add(item);
        emit("Changed");
    }

    public void RemoveItem(int id)
    {
        foreach (Product item in _basketItems)
        {
            if (id == item.Id)
            {
                _basketItems.Remove(item);
                break;
            }
        }
        emit("Changed");
    }
    
    public List<Product> GetBasketItems()
    {
        return _basketItems;
    }

    public double getBasketTotal()
    {
        double totalPrice = 0;
        foreach (var variablProduct in _basketItems)
        {
            totalPrice += variablProduct.Price;
        }
        return Math.Round(totalPrice,2);
    }
    
}