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
        bool inList = false;

        foreach (var product in _basketItems)
        {
            if (product.Id == item.Id)
            {
                // Product with the same ID already exists, increment the quantity
                product.quantity += 1;
                Console.WriteLine(product.quantity);
                inList = true;
                break; // No need to continue iterating once a match is found
            }
        }

        if (!inList)
        {
            // Product is not in the basket, add it with quantity 1
            Console.WriteLine("Inside L");
            Console.WriteLine(item);
            Console.WriteLine("Out again");
            item.quantity += 1;
            _basketItems.Add(item);
        }

        Console.WriteLine("We made it");

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
            totalPrice += variablProduct.Price * variablProduct.quantity;
        }
        return Math.Round(totalPrice,2);
    }
    
}