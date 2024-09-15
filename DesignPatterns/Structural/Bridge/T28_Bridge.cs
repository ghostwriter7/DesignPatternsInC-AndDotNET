using Autofac;

namespace DesignPatterns.Structural.Bridge;

public class T28_Bridge
{
    public static void Demo()
    {
        var rasterRenderer = new RasterRenderer();
        var vectorRenderer = new VectorRenderer();

        var circleA = new Circle(rasterRenderer, 5);
        var circleB = new Circle(vectorRenderer, 10);

        circleA.Draw();
        circleB.Draw();
        circleB.Resize(2);
        circleB.Draw();

        var cb = new ContainerBuilder();
        cb.RegisterType<VectorRenderer>().As<IRenderer>().SingleInstance();
        cb.Register<Circle>((ctx, parameters) =>
            new Circle(ctx.Resolve<IRenderer>(), parameters.Positional<double>(0))
        );

        using (var container = cb.Build())
        {
            var circle = container.Resolve<Circle>(new PositionalParameter(0, 5.0));
            circle.Draw();
            circle.Resize(2);
            circle.Draw();
        }
    }
}

public interface IRenderer
{
    void RenderCircle(double radius);
}

public class VectorRenderer : IRenderer
{
    public void RenderCircle(double radius)
    {
        WriteLine($"Drawing a circle with radius {radius}");
    }
}

public class RasterRenderer : IRenderer
{
    public void RenderCircle(double radius)
    {
        WriteLine($"Drawing pixels for a circle with radius {radius}");
    }
}

public abstract class Shape(IRenderer renderer)
{
    protected IRenderer Renderer { get; init; } = renderer;

    public abstract void Draw();
    public abstract void Resize(double factor);
}

public class Circle(IRenderer renderer, double radius) : Shape(renderer)
{
    private double _radius = radius;

    public override void Draw()
    {
        renderer.RenderCircle(_radius);
    }

    public override void Resize(double factor)
    {
        _radius *= factor;
    }
}