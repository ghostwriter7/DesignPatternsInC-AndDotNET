using System.Text;

namespace DesignPatterns.Structural.Composite;

public class T29_CompositePattern
{
    public static void Demo()
    {
        var drawing = new GraphicObject() { Name = "My Drawing" };
        drawing.Children.Add(new Square() { Color = "Red"});
        drawing.Children.Add(new Circle() { Color = "Blue"});

        var group = new GraphicObject() { Name = "Group" };
        group.Children.Add(new Square() { Color = "Pink" });
        group.Children.Add(new Circle() { Color = "Yellow" });
        drawing.Children.Add(group);
        
        WriteLine(drawing);
    }
}

public class GraphicObject
{
    public string Color;
    public virtual string Name { get; set; } = "Group";

    private readonly Lazy<List<GraphicObject>> _children = new();
    public List<GraphicObject> Children => _children.Value;

    private void Print(StringBuilder sb, int depth)
    {
        sb
            .Append(new string(' ', depth * 3))
            .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
            .AppendLine(Name);
        foreach (var child in Children)
        {
            child.Print(sb, depth + 1);
        }
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        Print(stringBuilder, 0);
        return stringBuilder.ToString();
    }
}

public class Circle : GraphicObject
{
    public override string Name => "Circle";
}

public class Square : GraphicObject
{
    public override string Name => "Square";
}