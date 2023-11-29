namespace WebShop.Services.HTTP;

public class HandicapService
{
    private static HandicapService instance;
    public static HandicapService getInstance() {
        if (instance == null) {
            instance = new HandicapService();
        }

        return instance;
    }

    public bool accessibility = false;
    public bool AccessibilityEnabled()
    {
        return accessibility;
    }
    public void ToggleAccessibility()
    {
        accessibility = !accessibility;
    }
}