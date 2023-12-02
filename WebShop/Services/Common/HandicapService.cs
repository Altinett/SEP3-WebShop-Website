using BlazorWasm.Services;

namespace WebShop.Services.HTTP;

public class HandicapService : IHandicapService {
    public bool AccessbilityEnabled { get; set; }
    public bool ToggleAccessibility() {
        AccessbilityEnabled = !AccessbilityEnabled;
        return AccessbilityEnabled;
    }
}