namespace DesignPatterns.Creational.Factory;

public class T14_AsyncFactoryMethod
{
    public static async Task Demo()
    {
        var foo = await Foo.CreateAsync();
    }

    public class Foo
    {
        private Foo()
        {
            
        }

        private async Task<Foo> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        public static Task<Foo> CreateAsync()
        {
            var foo = new Foo();
            return foo.InitAsync();
        } 
    }
}