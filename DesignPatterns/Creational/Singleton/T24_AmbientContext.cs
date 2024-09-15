using System.Drawing;
using System.Text;

namespace DesignPatterns.Creational.Singleton.SingletonDatabase;

public class T24_AmbientContext
{
    public static void Demo()
    {
        var house = new Building();

        using (new BuildingContext(3000))
        {
            house.AddWall(new(new(0,0), new(0, 5000)));
            house.AddWall(new(new(0, 5000), new(5000, 5000)));

            using (new BuildingContext(3500))
            {
                house.AddWall(new(new(0,0), new(0, 5000)));
                house.AddWall(new(new(0, 5000), new(5000, 5000)));
            }
            
            house.AddWall(new(new(0, 0), new(5000, 0)));
        }
        
        WriteLine(house);
    }
}

public sealed class BuildingContext : IDisposable
{
    public int WallHeight { get; init; }
    
    private static Stack<BuildingContext> _stack = new();

    public static BuildingContext Current => _stack.Peek();

    public BuildingContext(int wallHeight)
    {
        WallHeight = wallHeight;
        _stack.Push(this);
    }
    
    public void Dispose()
    {
        if (_stack.Count > 1)
            _stack.Pop();
    }
}

public class Building
{
    private List<Wall> _walls = new();

    public void AddWall(Wall wall) => _walls.Add(wall);

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var wall in _walls)
            sb.AppendLine(wall.ToString());
        return sb.ToString();
    }
}

public class Wall
{
    private Point _start, _end;
    private int _wallHeight;

    public Wall(Point start, Point end)
    {
        _start = start;
        _end = end;
        _wallHeight = BuildingContext.Current.WallHeight;
    }

    public override string ToString()
    {
        return $"START: {_start}, END: {_end}, WALL HEIGHT: {_wallHeight}";
    }
}

public struct Point(int X, int Y)
{
    public override string ToString()
    {
        return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
    }
};