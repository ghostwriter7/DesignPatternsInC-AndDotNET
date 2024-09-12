namespace DesignPatterns.Solid;

/*
 * You should always be able to upcast X to its base class
 * and any functionality should work in the same way
 */
public class T4_LiskovSubstitutionPrinciple
{
    public static void Demo()
    {
        var rc = new Rectangle(2, 3);
        Console.WriteLine($"Area of {rc} is {Calculator.Area(rc)}");

        Rectangle sq = new Square(5);
        sq.Width = 10;
        Console.WriteLine($"Area of {sq} is {Calculator.Area(sq)}");
    }

    private static class Calculator
    {
        public static int Area(Rectangle rectangle) => rectangle.Height * rectangle.Width;
    }

    private class Rectangle(int width, int height)
    {
        public virtual int Width { get; set; } = width;
        public virtual int Height { get; set; } = height;

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    private class Square(int dimension) : Rectangle(dimension, dimension)
    {
        public override int Width
        {
            set => base.Width = base.Height = value;
        }
        
        public override int Height
        {
            set => base.Width = base.Height = value;
        }
    }
}