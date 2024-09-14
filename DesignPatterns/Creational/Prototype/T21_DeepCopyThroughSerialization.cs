using System.Xml.Serialization;

namespace DesignPatterns.Creational.Prototype.Serialization;

public class T21_DeepCopyThroughSerialization
{
    public static void Demo()
    {
        var john = new Employee(300_000, ["John", "Doe"], new Address("Love str", 5));
        var roomMate = john.DeepClone();
        roomMate.Names[1] = "Smith";
        roomMate.Salary = 500_000;
            
        WriteLine(john);
        WriteLine(roomMate);
    }
}

public static class ExtensionMethods
{
    public static T DeepClone<T>(this T self)
    {
        using var memoryStream = new MemoryStream();
        var xmlSerializer = new XmlSerializer(typeof(T));
        xmlSerializer.Serialize(memoryStream, self);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return (T) xmlSerializer.Deserialize(memoryStream);
    }
}


public class Address
{
    public string Street;
    public int HouseNumber;

    public Address(string street, int houseNumber)
    {
        Street = street;
        HouseNumber = houseNumber;
    }

    public Address()
    {
    }

    public override string ToString()
    {
        return $"{nameof(Street)}: {Street}, {nameof(HouseNumber)}: {HouseNumber}";
    }
}

public class Person
{
    public string[] Names;
    public Address Address;

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
    }

    public Person()
    {
    }

    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(", ", Names)}, {nameof(Address)}: {Address}";
    }
}

public class Employee : Person
{
    public int Salary;

    public Employee()
    {
    }

    public Employee(int salary, string[] names, Address address) : base(names, address)
    {
        Salary = salary;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, {nameof(Salary)}: {Salary}";
    }
}