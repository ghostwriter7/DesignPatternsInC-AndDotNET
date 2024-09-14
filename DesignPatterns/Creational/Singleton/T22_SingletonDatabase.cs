using System.Xml.Serialization;

namespace DesignPatterns.Creational.Singleton.SingletonDatabase;

public class T22_SingletonDatabase
{
    public static void Demo()
    {
        WriteLine(SingletonDatabase.Instance.GetPopulation("Tokyo"));
    }
}

public interface IDatabase
{
    int GetPopulation(string cityName);
}

public class SingletonDatabase : IDatabase
{
    private Dictionary<string, int> _cities;

    private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

    public static SingletonDatabase Instance = instance.Value;
    
    public SingletonDatabase()
    {
        WriteLine("Reading the database...");

        _cities = File.ReadAllLines(@"Creational\Singleton\capitals.txt")
            .Select((line) =>
            {
                var elements = line.Split(':');
                return (capital: elements.ElementAt(0), population: int.Parse(elements.ElementAt(1).Replace(",", "")));
            }).ToDictionary(
                (element => element.capital),
                (element => element.population)
            );
    }

    public int GetPopulation(string cityName)
    {
        return _cities[cityName];
    }
}