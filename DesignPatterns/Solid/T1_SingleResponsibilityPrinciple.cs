namespace DesignPatterns.Solid;

public class T1_SingleResponsibilityPrinciple
{
    public static void Demo()
    {
        var journal = new Journal();
        journal.AddEntry("I cried today");
        journal.AddEntry("I ate a bug");
        Console.WriteLine(journal);
    }

    public class Journal
    {
        private readonly List<string> entries = [];
        private static int count = 0;

        public int AddEntry(string entry)
        {
            entries.Add($"{++count}: {entry}");
            return count;
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }
}