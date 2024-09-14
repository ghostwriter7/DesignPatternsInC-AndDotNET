namespace DesignPatterns.Creational.Prototype;

public class T19_CopyConstructor
{
    public class Person
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address);
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

        public Address(Address other)
        {
            Street = other.Street;
            HouseNumber = other.HouseNumber;
        }
    }
}