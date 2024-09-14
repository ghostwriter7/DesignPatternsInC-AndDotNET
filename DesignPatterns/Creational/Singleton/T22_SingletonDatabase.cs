using System.Xml.Serialization;
using Autofac;
using NUnit.Framework;

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

public class OrdinaryDatabase : IDatabase
{
    private Dictionary<string, int> _cities;

    public OrdinaryDatabase()
    {
        WriteLine("Reading the database...");

        _cities = File.ReadAllLines(
                Path.Combine(
                    new FileInfo(typeof(SingletonDatabase).Assembly.Location).DirectoryName,
                    "capitals.txt"
                ))
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

public class SingletonDatabase : IDatabase
{
    private Dictionary<string, int> _cities;

    private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

    public static SingletonDatabase Instance = instance.Value;

    private static int instanceCount;
    public static int InstanceCount => instanceCount;

    public SingletonDatabase()
    {
        WriteLine("Reading the database...");
        instanceCount++;

        _cities = File.ReadAllLines(
                Path.Combine(
                    new FileInfo(typeof(SingletonDatabase).Assembly.Location).DirectoryName,
                    "capitals.txt"
                ))
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

public class RecordFinder
{
    public int GetTotalPopulation(IEnumerable<string> names) =>
        names.Select((name) => SingletonDatabase.Instance.GetPopulation(name)).Sum();
}

public class DummyDatabase : IDatabase
{
    public int GetPopulation(string cityName)
    {
        return new Dictionary<string, int>()
        {
            ["alpha"] = 1,
            ["beta"] = 2,
            ["gamma"] = 3
        }[cityName];
    }
}

public class ConfigurableRecordFinder(IDatabase database)
{
    public int GetTotalPopulation(IEnumerable<string> names) =>
        names.Aggregate(0, (total, name) => total + database.GetPopulation(name));
}

[TestFixture]
class SingletonTests
{
    [Test]
    public void IsSingletonTest()
    {
        var db1 = SingletonDatabase.Instance;
        var db2 = SingletonDatabase.Instance;
        Assert.That(db1, Is.SameAs(db2));
        Assert.That(SingletonDatabase.InstanceCount, Is.EqualTo(1));
    }

    [Test]
    public void GetTotalPopulationTest()
    {
        string[] names = ["Beijing", "London"];
        var total = new RecordFinder().GetTotalPopulation(names);
        // problem! running tests on live database due to singleton's implementation
        Assert.That(total, Is.EqualTo(20463000 + 9541000));
    }

    [Test]
    public void ConfigurableRecordFinderTest()
    {
        var crf = new ConfigurableRecordFinder(new DummyDatabase());
        Assert.That(crf.GetTotalPopulation(["alpha", "beta"]), Is.EqualTo(3));
    }

    [Test]
    public void DIOrdinaryDatabaseTest()
    {
        var cb = new ContainerBuilder();
        cb.RegisterType<OrdinaryDatabase>()
            .As<IDatabase>()
            .SingleInstance();
        cb.RegisterType<ConfigurableRecordFinder>();
        using (var ctx = cb.Build())
        {
            var configurableRecordFinder = ctx.Resolve<ConfigurableRecordFinder>();
            Assert.That(configurableRecordFinder.GetTotalPopulation(["Tokyo"]), Is.EqualTo(37393000));
        }
    }
}