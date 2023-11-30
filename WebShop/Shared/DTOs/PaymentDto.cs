using System.Runtime.InteropServices.JavaScript;

namespace WebShop.Shared.DTOs;

public class PaymentDto
{
    //public List<int> productIds { get; init; }
    public Dictionary<int, int> products { get; init; }
    public string firstname { get; init; }
    public string lastname { get; init; }
    public string address { get; init; }
    public int postcode { get; init; }
    public int phonenumber { get; init; }
    public string email { get; init; } 
    public string cardnumber { get; init; }
    public DateTime expirationdate { get; init; }
    public int cvc { get; init; }
    public int total { get; init; }
    
    public string date { get; init; }
    
}