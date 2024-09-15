namespace DesignPatterns.Creational.Singleton.SingletonDatabase;

public class T23_OneSingletonePerThread
{
    public static void Demo()
    {
        var t1 = Task.Factory.StartNew(() =>
        {
            WriteLine($"T{Task.CurrentId}: {SingletonPerThread.ThreadInstance.Id}");
        });
        var t2 = Task.Factory.StartNew(() =>
        {
            WriteLine($"T{Task.CurrentId}: {SingletonPerThread.ThreadInstance.Id}");
            WriteLine($"T{Task.CurrentId}: {SingletonPerThread.ThreadInstance.Id}");
        });

        Task.WaitAll(t1, t2);
    }
}

public class SingletonPerThread
{
    private static readonly ThreadLocal<SingletonPerThread> _threadLocal =
        new(() => new SingletonPerThread());

    public static SingletonPerThread ThreadInstance => _threadLocal.Value;
    public int Id { get; private set; }
    
    private SingletonPerThread()
    {
        WriteLine($"Constructor called by {Task.CurrentId}");
        Id = Task.CurrentId.Value;
    }
}