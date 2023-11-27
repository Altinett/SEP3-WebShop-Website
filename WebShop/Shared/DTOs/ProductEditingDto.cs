namespace Shared.DTOs;

public class ProductEditingDto
{
    public string name { get; init; }
    public string description { get; init; }
    /*public List<int>? categoryIds { get; init; }*/
    public double price { get; init; }
    public int amount { get; init; }
    public int id { get; init; }
    public string image { get; init; }
}