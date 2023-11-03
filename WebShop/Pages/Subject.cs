namespace WebShop.Pages;

public class Subject
{
    private Dictionary<String, List<Action<object[]>>> Observers = new();

    public void on(String _event, Action<object[]> action)
    {
        if (!Observers.ContainsKey(_event))
        {
            Observers.Add(_event, new List<Action<object[]>>());
        }
        Observers[_event].Add(action);
    }

    public void emit(String _event, params Object[] args)
    {
        foreach (Action<object[]> action in Observers[_event])
        {
            action(args);
        }
    }
    

}