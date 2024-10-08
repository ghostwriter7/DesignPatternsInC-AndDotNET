﻿namespace DesignPatterns.Structural.Adapter;

/// <summary>
/// Adapt a literal value to a type
/// </summary>
public class T26_GenericValueAdapter
{
    public static void Demo()
    {
        var vec2a = Vector2i.Create(0, 100);
        var vec2b = Vector2i.Create(50, 2);
        var vec3 = new Vector3f(3, 5, 10);

        WriteLine($"Vec2a: {vec2a}");
        WriteLine($"Vec2b: {vec2b}");
        WriteLine($"Vec2a + Vec2b: {vec2a + vec2b}");
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

public abstract class Vector<TSelf, T, D> 
    where D : IInteger, new()
    where TSelf : Vector<TSelf, T, D>, new()
{
    private T[] _data;

    protected Vector(params T[] values)
    {
        var requiredSize = new D().Value;
        _data = new T[requiredSize];

        var providedSize = values.Length;

        for (var i = 0; i < Math.Min(requiredSize, providedSize); i++)
            _data[i] = values[i];
    }

    public static TSelf Create(params T[] values)
    {
        var self = new TSelf();
        var requiredSize = new D().Value;
        self._data = new T[requiredSize];

        var providedSize = values.Length;

        for (var i = 0; i < Math.Min(requiredSize, providedSize); i++)
            self._data[i] = values[i];
        return self;
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

/// <summary>
/// Specialized class (intermediate class in hierarchy) to provide operator overloading
/// </summary>
public class VectorOfInts<TSelf, D>(params int[] values) : Vector<TSelf, int, D>(values)
    where D : IInteger, new()
    where TSelf : VectorOfInts<TSelf, D>, new()
{
    protected VectorOfInts() : this([])
    {
    }

    public static VectorOfInts<TSelf, D> operator +(VectorOfInts<TSelf, D> lhs, VectorOfInts<TSelf, D> rhs)
    {
        var vector = new VectorOfInts<TSelf, D>();
        for (int i = 0; i < new D().Value; i++)
            vector[i] = lhs[i] + rhs[i];
        return vector;
    }

    public static VectorOfInts<TSelf, D> operator -(VectorOfInts<TSelf, D> lhs, VectorOfInts<TSelf, D> rhs)
    {
        var vector = new VectorOfInts<TSelf, D>();
        for (int i = 0; i < new D().Value; i++)
            vector[i] = lhs[i] - rhs[i];
        return vector;
    }
}

public class Vector2i : VectorOfInts<Vector2i, Dimensions.Two>
{
}

public class Vector3f(params float[] values) : Vector<Vector3f, float, Dimensions.Three>(values)
{
    public Vector3f() : this([])
    {
    }
}