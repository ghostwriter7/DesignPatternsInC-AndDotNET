namespace DesignPatterns.Creational.Prototype;

public class T18_IClonableIsBad
{
    public static void Demo()
    {
        var john = new Person(["John", "Smith"], new Address("Str", 123));
        var jane = john.Clone();
    }

    public class Person : ICloneable
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ",Names)}, {nameof(Address)}: {Address}";
        }

        public object Clone()
        {
            return new Person(Names, (Address) Address.Clone());
        }
    }

    public class Address : ICloneable
    {
        public string Street;
        public int HouseNumber;

        public Address(string street, int houseNumber)
        {
            Street = street;
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(Street)}: {Street}, {nameof(HouseNumber)}: {HouseNumber}";
        }

        public object Clone()
        {
            return new Address(Street, HouseNumber);
        }
    }
}