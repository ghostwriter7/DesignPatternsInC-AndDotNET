using System.Text;

namespace DesignPatterns.Creational.Builder;

public class T8_Builder
{
    public static void Demo()
    {
        var builder = new HtmlBuilder("ul");
        builder.AddChild("li", "hello");
        builder.AddChild("li", "world");
        WriteLine(builder.ToString());
    }

    private class HtmlElement(string name, string? text)
    {
        public readonly string Name = name;
        public string Text = text;
        public List<HtmlElement> Elements = [];
        private const int _indentSize = 2;

        private string ToString(int indent)
        {
            var sb = new StringBuilder();
            var indentChars = new string(' ',_indentSize * indent);
            sb.AppendLine($"{indentChars}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.AppendLine($"{new string(' ', _indentSize * (indent + 1))}{Text}");
            }

            foreach (var element in Elements)
            {
                sb.AppendLine(element.ToString(indent + 1));
            }
            sb.Append($"{indentChars}</{Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString(0);
        }
    }

    private class HtmlBuilder(string rootName)
    {
        private HtmlElement root = new HtmlElement(rootName, null);

        public void AddChild(string childName, string childText)
        {
            var child = new HtmlElement(childName, childText);
            root.Elements.Add(child);
        }

        public void Clear()
        {
            root = new HtmlElement(rootName, null);
        }
        
        public override string ToString()
        {
            return root.ToString();
        }
    }
}