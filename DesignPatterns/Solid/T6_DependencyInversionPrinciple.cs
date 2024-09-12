namespace DesignPatterns.Solid;

/*
 * High level modules should not directly depend on low level modules.
 * Instead, they should depend on abstractions
 */
public class T6_DependencyInversionPrinciple
{
    public static void Demo()
    {
        var parent = new Person("John");
        var child1 = new Person("Amy");
        var child2 = new Person("Tom");

        var relationships = new Relationships();
        relationships.AddParentAndChild(parent, child1);
        relationships.AddParentAndChild(parent, child2);

        new BadResearch(relationships).Research();
        new GoodResearch(relationships).Research();
    }

    private enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    private class Person(string name)
    {
        public string Name { get; init; } = name;
    }

    // low-level
    private class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> relations = [];
        public List<(Person, Relationship, Person)> Relations => relations;

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations
                .Where(r => r.Item1.Name.Equals(name) && r.Item2 == Relationship.Parent)
                .Select(r => r.Item3);
        }
    }

    /*
     * Bad design - high level module Research depends directly on low-level module Relationships
     * and its internals
     */
    private class BadResearch(Relationships relationships)
    {
        public void Research()
        {
            foreach (var relationship in relationships.Relations
                         .Where(relation =>
                             relation.Item1.Name.Equals("John") && relation.Item2 == Relationship.Parent))
            {
                Console.WriteLine($"John is parent of {relationship.Item3.Name}");
            }
        }
    }

    /*
     * Good example - Research depends on an abstraction and Relationships class is free to modify
     * its internals
     */
    private class GoodResearch(IRelationshipBrowser relationshipBrowser)
    {
        public void Research()
        {
            foreach (var person in relationshipBrowser.FindAllChildrenOf("John"))
            {
                Console.WriteLine($"John is parent of {person.Name}");
            }
        }
    }

    private interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }
}