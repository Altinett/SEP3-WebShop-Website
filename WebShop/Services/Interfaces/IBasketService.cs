using Shared;

namespace BlazorWasm.Services; 

public interface IBasketService {
	bool InBasket(int id);
	bool InBasketStock(int id);
	int? GetProductAmount(int id);
	int? GetAmountInBasket();
	bool AddItem(int id);
	void RemoveItem(int id);
	void Clear();
	Dictionary<int, Product> GetBasketItems();
	Product GetBasketItem(int id);
	double GetBasketTotal();
	void OnChanged(Action<Object[]> callback);
}