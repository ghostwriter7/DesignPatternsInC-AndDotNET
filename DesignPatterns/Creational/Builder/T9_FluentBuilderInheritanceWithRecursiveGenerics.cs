namespace DesignPatterns.Creational.Builder;

public class T9_FluentBuilderInheritanceWithRecursiveGenerics
{
    public static void Demo()
    {
        var person = Person.New
            .Called("Kubo")
            .WorksAsA("Wizard")
            .Enjoys("climbing")
            .Enjoys("fighting")
            .Build();
        
        WriteLine(person);
    }

    private class Person
    {
        public string Name;
        public string Position;
        public List<string> Hobbies = [];
        
        public class Builder : PersonHobbyBuilder<Builder> {}

        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{Name,10} -- {Position,15} -- enjoys: {string.Join(" + ", Hobbies)}";
        }
    }

    private abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build() => person;
    }

    private class PersonInfoBuilder<SELF> : PersonBuilder where SELF : PersonInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    private class PersonWorkBuilder<SELF> : PersonInfoBuilder<PersonWorkBuilder<SELF>>
        where SELF : PersonWorkBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF)this;
        }
    }

    private class PersonHobbyBuilder<SELF> : PersonWorkBuilder<PersonHobbyBuilder<SELF>>
        where SELF : PersonHobbyBuilder<SELF>
    {
        public SELF Enjoys(string hobby)
        {
            person.Hobbies.Add(hobby);
            return (SELF)this;
        }
    }
}