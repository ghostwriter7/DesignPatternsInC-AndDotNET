using System.Collections.ObjectModel;

namespace DesignPatterns.Structural.Adapter;

public class T25_Adapter
{
    public static void Demo()
    {
        var horizontalLine = new HorizontalLine(new(0, 0), new(10, 0));
        var lineToPointAdapter = new LineToPointAdapter(horizontalLine);
        foreach (var point in lineToPointAdapter)
        {
            Graphics.Draw(point);
        }
    }
}

public record Point(int X, int Y);

public record HorizontalLine(Point Start, Point End);

public class LineToPointAdapter : Collection<Point>
{
    public LineToPointAdapter(HorizontalLine line)
    {
        WriteLine(
            $"Adapting line starting at [{line.Start.X}, {line.Start.Y}], ending at [{line.End.X}, {line.End.Y}]");
        for (int i = line.Start.X; i <= line.End.X; i++)
            Add(new Point(i, line.Start.Y));
    }
}

public static class Graphics
{
    public static void Draw(Point point)
    {
        Console.WriteLine($"[{point.X}, {point.Y}]");
    }
}