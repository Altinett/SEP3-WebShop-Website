using System.Text.Json.Serialization;

namespace Shared;

public class Product
{
	public int Id { get; set; }
	public string Image { get; set; }
	public string? name { get; set; }
	public double Price { get; set; }

	public int amount { get; set; }
	public List<int>? categoryIds { get; set; }
	public string? description { get; set; }
	public bool InStock { get; set; }
	public int quantity { get; set; }
	
	public bool flagged { get; set; }

	public Product() { }
	public Product(int id, string img, string name, double price, bool inStock)
	{
		Id = id;
		Image = img;
		this.name = name;
		Price = price;
		InStock = inStock;
	}
	
	//Constructor for the basket
	public Product(int id, string img, string name, double price, bool inStock, int amount, String description, int quantity)
	{
		Id = id;
		Image = img;
		this.name = name;
		Price = price;
		InStock = inStock;
		this.amount = amount;
		this.description = description;
		this.quantity = quantity;
	}

	public Product(int id, string name, double price, int amount, List<int> categoryIds, string description)
	{
		Id = id;
		Image = "https://i.ebayimg.com/images/g/h9UAAOSwpPNlLMxt/s-l500.jpg";
		this.name = name;
		Price = price;

		if (amount > 0)
			InStock = true;
		else
			InStock = false;
	}


	public Product(Product productToCopy)
	{
		// Assuming these are the properties of Product
		this.Id = productToCopy.Id;
		this.amount = productToCopy.amount;
		this.name = productToCopy.name;
		this.Price = productToCopy.Price;
		// ...copy all the other properties from productToCopy...
	}
	public override string ToString()
	{
		var categoryIdsString = categoryIds != null ? string.Join(", ", categoryIds) : "None";
		return $"Product ID: {Id}\n" +
		       $"Image URL: {Image}\n" +
		       //$"Title: {Title}\n" +
		       $"Name: {name}\n" +
		       $"Price: {Price}\n" +
		       //$"Duplicate Price (might be an error): {price}\n" + // This line might need to be removed if 'price' is indeed a duplicate
		       $"Amount: {amount.ToString() ?? "Not specified"}\n" +
		       $"Categories: {categoryIdsString}\n" +
		       $"Description: {description}\n" +
		       $"In Stock: {(InStock ? "Yes" : "No")}\n" + 
		       $"Flagged: {flagged} ";
	}
}