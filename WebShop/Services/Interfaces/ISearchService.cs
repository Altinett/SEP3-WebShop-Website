using Shared;

namespace BlazorWasm.Services; 

public interface ISearchService {
	string Query { get; set; }
	List<int> Categories { get; set; }
	List<Product> Products { get; set; }
	int Page { get; set; }

	void NextPage();
	void PreviousPage();
	Task<List<Product>> Search();
	void OnSearch(Action<Object[]> callback);
}