namespace Shared; 

public class Product {
	public string Id { get; set; }
	public string Img { get; set; }
	public string Title { get; set; }
	public double Price { get; set; }
	public bool InStock { get; set; }

	public Product(string id, string img, string title, double price, bool inStock) {
		Id = id;
		Img = img;
		Title = title;
		Price = price;
		InStock = inStock;
	}
}