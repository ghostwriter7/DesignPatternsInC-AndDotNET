using System.Text;

namespace DesignPatterns.Creational.Builder;

public class T7_LifeWithoutBuilder
{
    public static void Demo()
    {
        var text = "hello";
        var sb = new StringBuilder();
        sb.Append("<p>");
        sb.Append(text);
        sb.Append("</p>");

        List<string> words = ["Hello", "world"];
        sb.Clear();
        sb.Append("<ul>");
        foreach (var word in words)
        {
            sb.AppendFormat("<li>{0}</li>", word);
        }

        sb.Append("</ul>");
        WriteLine(sb);
    }
}