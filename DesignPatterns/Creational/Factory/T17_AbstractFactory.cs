namespace DesignPatterns.Creational.Factory;

public class T17_AbstractFactory
{
    public static void Demo()
    {
        var machine = new HotDrinkMachine();
        var drink = machine.Buy(HotDrinkMachine.AvailableDrink.Coffee, 150);
        drink.Consume();
    }

    public interface IHotDrink
    {
        void Consume();
    }

    private class Tea : IHotDrink
    {
        public void Consume()
        {
            WriteLine("What a fantastic tea...");
        }
    }

    private class Coffee : IHotDrink
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
        public enum AvailableDrink
        {
            Coffee,
            Tea
        }

        private Dictionary<AvailableDrink, IHotDrinkFactory> _factories = new();

        public HotDrinkMachine()
        {
            foreach (AvailableDrink availableDrink in Enum.GetValues(typeof(AvailableDrink)))
            {
                var factory = (IHotDrinkFactory) Activator.CreateInstance(
                   Type.GetType($"DesignPatterns.Creational.Factory.{nameof(T17_AbstractFactory)}+{Enum.GetName(typeof(AvailableDrink), availableDrink)}Factory")     
                );
                _factories.Add(availableDrink, factory);
            }
        }

        public IHotDrink Buy(AvailableDrink drink, int amount)
        {
            return _factories[drink].Prepare(amount);
        }
    }
}