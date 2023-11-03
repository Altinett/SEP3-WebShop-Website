namespace Shared.DTOs;

public class ProductCreationDto
{
    public string Name { get; init; }
    public string Description { get; init; }
    public List<int>? CheckedValues { get; init; }
    public int Price { get; init; }
    public int Amount { get; init; }
    
}