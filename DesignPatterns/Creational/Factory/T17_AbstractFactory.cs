namespace DesignPatterns.Creational.Factory;

public class T17_AbstractFactory
{
    public static void Demo()
    {
        var machine = new HotDrinkMachine();
        machine.DisplayDrinks();
        var drink = machine.MakeDrink("Coffee", 150);
        drink.Consume();
    }
}

public interface IHotDrink
{
    void Consume();
}

internal class Tea : IHotDrink
{
    public void Consume()
    {
        WriteLine("What a fantastic tea...");
    }
}

internal class Coffee : IHotDrink
{
    public void Consume()
    {
        WriteLine("This coffee is delicious...");
    }
}

public interface IHotDrinkFactory
{
    IHotDrink Prepare(int amount);
}

public class TeaFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        WriteLine($"Pouring {amount} ml of water into the cup with a tea bag... done");
        return new Tea();
    }
}
    
public class CoffeeFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        WriteLine($"Pouring {amount} ml of water into the cup with coffee beans... done");
        return new Coffee();
    }
}

public class HotDrinkMachine
{
    private List<(string, IHotDrinkFactory)> _factories = new();

    public HotDrinkMachine()
    {
        foreach (var type in typeof(HotDrinkMachine).Assembly.GetTypes())
        {
            if (type.IsAssignableTo(typeof(IHotDrinkFactory)) && type.IsClass)
            {
                var factory = Activator.CreateInstance(type) as IHotDrinkFactory;
                _factories.Add((type.Name.Replace("Factory", string.Empty), factory));
            }
        }
    }

    public IHotDrink MakeDrink(string drink, int amount)
    {
        var factory = _factories
            .Single(entry => entry.Item1.Equals(drink));
        return factory.Item2.Prepare(amount);
    }

    public void DisplayDrinks()
    {
        WriteLine("Available drinks:");
        foreach (var (name,_) in _factories)
        {
            WriteLine($" -- {name}");
        }
    }
    
}