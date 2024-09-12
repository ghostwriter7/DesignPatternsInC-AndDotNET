namespace DesignPatterns.Creational.Builder;

public class T12_FacetedBuilder
{
    public static void Demo()
    {
        var pb = new HumanBuilder();
        Human person = pb
            .Works
            .At("Microsoft")
            .AsA("Cybersecurity expert")
            .Earning(500_000)
            .Lives
            .At("Unreal 100")
            .WithPostalCode("11X22")
            .In("New Orlean");
            // .Build(); see implicit operator
        
        WriteLine(person);
    }

    public class Human()
    {
        // address
        public string Street, PostalCode, City;
        
        // company
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(Street)}: {Street}, {nameof(PostalCode)}: {PostalCode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    public class HumanBuilder // facade 
    {
        protected Human human = new();

        public PersonJobBuilder Works => new PersonJobBuilder(human);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(human);

        public Human Build() => human;

        public static implicit operator Human(HumanBuilder hb) => hb.human;
    }

    public class PersonJobBuilder : HumanBuilder
    {
        public PersonJobBuilder(Human human)
        {
            this.human = human;
        }
        
        public PersonJobBuilder At(string companyName)
        {
            human.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            human.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            human.AnnualIncome = amount;
            return this;
        }
    }

    public class PersonAddressBuilder : HumanBuilder
    {
        public PersonAddressBuilder(Human human)
        {
            this.human = human;
        }

        public PersonAddressBuilder At(string street)
        {
            human.Street = street;
            return this;
        }

        public PersonAddressBuilder WithPostalCode(string postalCode)
        {
            human.PostalCode = postalCode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            human.City = city;
            return this;
        }
    }
}