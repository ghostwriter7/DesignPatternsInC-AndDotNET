namespace DesignPatterns.Creational.Builder;

public class T10_StepwiseBuilder
{
    public static void Demo()
    {
        var crossover = Car.New
            .OfType(CarType.Crossover)
            .WithWheelSize(20)
            .Build();

        var sedan = Car.New
            .OfType(CarType.Sedan)
            .WithWheelSize(15)
            .Build();

        var messedUpCar = Car.New;
        // you can still access methods in a different order through reflection
        var methodInfo = messedUpCar.GetType().GetMethod("WithWheelSize", new[] { typeof(int) });
        
    }

    private enum CarType
    {
        Sedan,
        Crossover
    }

    private class Car
    {
        public CarType CarType;
        public int WheelSize;
        public static ISpecifyCarType New => new CarBuilder();

        private class CarBuilder : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
        {
            private Car car = new();

            public ISpecifyWheelSize OfType(CarType type)
            {
                car.CarType = type;
                return this;
            }

            public IBuildCar WithWheelSize(int wheelSize)
            {
                switch (car.CarType)
                {
                    case CarType.Crossover when wheelSize is < 17 or > 20:
                    case CarType.Sedan when wheelSize is < 15 or > 17:
                        throw new ArgumentException($"Wheel size in a wrong range for {car.CarType}");
                }

                car.WheelSize = wheelSize;
                return this;
            }

            public Car Build() => car;
        }
    }


    private interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }

    private interface ISpecifyWheelSize
    {
        IBuildCar WithWheelSize(int wheelSize);
    }

    private interface IBuildCar
    {
        Car Build();
    }
}