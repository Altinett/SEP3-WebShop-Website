namespace Shared.DTOs;

public class ProductCreationDto
{
    public string name { get; init; }
    public string description { get; init; }
    public List<int>? categoryIds { get; init; }
    public double price { get; init; }
    public int amount { get; init; }
    public String image { get; init; }
    
}