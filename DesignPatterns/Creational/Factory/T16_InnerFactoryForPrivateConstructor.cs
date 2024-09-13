namespace DesignPatterns.Creational.Factory;

public class T16_InnerFactoryForPrivateConstructor
{
    public static void Demo()
    {
        Point.Factory.NewCartesianPoint(5, 10);
    }

    public class Point
    {
        public double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y) => new Point(x, y);

            public static Point NewPolarPoint(double rho, double theta) =>
                new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }
}