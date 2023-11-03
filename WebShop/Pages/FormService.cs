namespace WebShop.Pages;

public class FormService
{
    private static FormService instance;

    public static FormService getInstance()
    {
        if (instance == null)
        {
            instance = new FormService();
        }

        return instance;
    }

    private FormService()
    {
        
    }
}