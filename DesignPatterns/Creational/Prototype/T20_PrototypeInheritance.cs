namespace DesignPatterns.Creational.Prototype;

public class T20_PrototypeInheritance
{
    public static void Demo()
    {
        var john = new Employee(300_000, ["John", "Doe"], new Address("Love str", 5));
        var roomMate = john.DeepCopy();
        roomMate.Names[1] = "Smith";
        roomMate.Salary = 500_000;
        
        WriteLine(john);
        WriteLine(roomMate);
    }
}

public interface IDeepCopyable<T> where T : new()
    {
        void CopyTo(T t);

        public T DeepCopy()
        {
            T t = new T();
            CopyTo(t);
            return t;
        }
    }

    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this IDeepCopyable<T> item) where T : new()
        {
            return item.DeepCopy();
        }

        public static T DeepCopy<T>(this T person) where T : Person, new()
        {
            return ((IDeepCopyable<T>) person).DeepCopy();
        }
    }
    
    public class Address : IDeepCopyable<Address>
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

        public void CopyTo(Address target)
        {
            target.Street = Street;
            target.HouseNumber = HouseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(Street)}: {Street}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    public class Person : IDeepCopyable<Person>
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

        public void CopyTo(Person target)
        {
            target.Names = (string[])Names.Clone();
            target.Address = Address.DeepCopy();
        }
        
        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(", ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Employee : Person, IDeepCopyable<Employee>
    {
        public int Salary;

        public Employee()
        {
            
        }

        public Employee(int salary, string[] names, Address address) : base(names, address)
        {
            Salary = salary;
        }

        public void CopyTo(Employee target)
        {
            base.CopyTo(target);
            target.Salary = Salary;
        } 

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(Salary)}: {Salary}";
        }
    }