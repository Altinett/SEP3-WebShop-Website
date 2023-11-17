using System.Runtime.InteropServices.JavaScript;

namespace WebShop.Shared.DTOs;

public class PaymentDto
{
    public List<int> productIds { get; init; }
    public Dictionary<int, int> products { get; init; }
    public string firstname { get; init; }
    public string lastname { get; init; }
    public string address { get; init; }
    public int postcode { get; init; }
    public int phonenumber { get; init; }
    public string email { get; init; } 
    public int cardnumber { get; init; }
    public DateOnly expirationdate { get; init; }
    public int cvc { get; init; }
}