namespace DesignPatterns.Creational.Factory;

public class T13_FactoryMethod
{
    public static void Demo()
    {
    }

    public class Point
    {
        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point NewPolarPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewCartesianPoint(double rho, double theta)
        {
            return NewPolarPoint(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }
}