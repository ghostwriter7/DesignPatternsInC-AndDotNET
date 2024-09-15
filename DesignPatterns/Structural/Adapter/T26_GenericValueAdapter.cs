namespace DesignPatterns.Structural.Adapter;

/// <summary>
/// Adapt a literal value to a type
/// </summary>
public class T26_GenericValueAdapter
{

    public static void Demo()
    {
        var vec2 = new Vector2i(0, 100);
        var vec3 = new Vector3f(3, 5, 10);
        
        WriteLine($"Vec2: {vec2}");
        WriteLine($"Vec3: {vec3}");
    }
}

public interface IInteger
{
    int Value { get; }
}

public static class Dimensions
{
    public class Two : IInteger
    {
        public int Value => 2;
    }

    public class Three : IInteger
    {
        public int Value => 3;
    }
}

public abstract class Vector<T, D> where D : IInteger, new()
{
    private readonly T[] _data;

    protected Vector(params T[] values)
    {
        var requiredSize = new D().Value;
        _data = new T[requiredSize];

        var providedSize = values.Length;

        for (var i = 0; i < Math.Min(requiredSize, providedSize); i++)
            _data[i] = values[i];
    }
    
    public T this[int index]
    {
        get => _data[index];
        set => _data[index] = value;
    }

    public override string ToString()
    {
        return string.Join(", ", _data);
    }
}

public class Vector2i(params int[] values) : Vector<int, Dimensions.Two>(values)
{
    public Vector2i() : this([])
    {
    }
}

public class Vector3f(params float[] values) : Vector<float, Dimensions.Three>(values)
{
    public Vector3f() : this([])
    {
    }
}