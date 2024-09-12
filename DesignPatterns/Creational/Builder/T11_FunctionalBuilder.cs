namespace DesignPatterns.Creational.Builder;

public class T11_FunctionalBuilder
{
    public static void Demo()
    {
        var person = new PersonBuilder()
            .Called("Kubo")
            .WorksAt("King")
            .Do((p) => p.Hobby = "Magic")
            .Build();

        WriteLine(person);
    }

    public class Person
    {
        public string Position, Name, Hobby;

        public override string ToString()
        {
            return $"{Name} works at {Position} and enjoys {Hobby}";
        }
    }

    public sealed class PersonBuilder
    {
        private List<Func<Person, Person>> actions = [];

        public PersonBuilder Called(string name) => AddAction((p) => p.Name = name);


        public PersonBuilder Do(Action<Person> action) => AddAction(action);

        public Person Build() => actions.Aggregate(new Person(), (person, func) => func(person));

        private PersonBuilder AddAction(Action<Person> action)
        {
            actions.Add((p) =>
            {
                action(p);
                return p;
            });
            return this;
        }
    }
}

public static class PersonBuilderExtensions
{
    public static T11_FunctionalBuilder.PersonBuilder WorksAt(this T11_FunctionalBuilder.PersonBuilder personBuilder, string position) =>
        personBuilder.Do(p => p.Position = position);
}