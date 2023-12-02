using Shared;

namespace BlazorWasm.Services; 

public interface IBasketService {
	bool InBasketStock(int id);
	int? GetProductAmount(int id);
	int? GetAmountInBasket();
	Task<bool> AddItem(int id);
	void RemoveItem(int id);
	void Clear();
	Dictionary<int, Product> GetBasketItems();
	double GetBasketTotal();
	void OnChanged(Action<Object[]> callback);
}