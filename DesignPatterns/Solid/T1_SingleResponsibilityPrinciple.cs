using System.Diagnostics;

namespace DesignPatterns.Solid;

public class T1_SingleResponsibilityPrinciple
{
    public static void Demo()
    {
        var journal = new Journal();
        journal.AddEntry("I cried today");
        journal.AddEntry("I ate a bug");

        var persistence = new Persistance();
        const string filename = @"C:\Users\hrozplochowski\journal.txt";
        persistence.SaveToFile(journal, filename, true);
    }

    // One responsibility & one reason to change
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

        // Violating Single Responsibility Principle...
        public void Save(string filename)
        {
            File.WriteAllText(filename, ToString());
        }

        // Violating Single Responsibility Principle...
        public static Journal Load(string filename)
        {
            var content = File.ReadAllLines(filename);
            var journal = new Journal();
            foreach (var entry in content)
            {
                journal.AddEntry(entry);
            }

            return journal;
        }
    }

    public class Persistance
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false) {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, journal.ToString());
        }
    }
}