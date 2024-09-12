namespace DesignPatterns.Solid;

/*
 * If you have an interface which contains too much stuff,
 * break it into smaller, managable interfaces
 * Don't force consumers to implement methods they don't need
 */
public class T5_InterfaceSegregationPrinciple
{
    public static void Demo()
    {
        
    }
    
    private class Document
    {
        
    }
    
    private interface IMachine
    {
        void Print(Document document);
        void Fax(Document document);
        void Scan(Document document);
    }
    
    private class MultifunctionPrinter : IMachine
    {
        public void Print(Document document)
        {
            Console.WriteLine("I can print...");
        }

        public void Fax(Document document)
        {
            Console.WriteLine("I can fax...");
        }

        public void Scan(Document document)
        {
            Console.WriteLine("I can scan...");
        }
    }
    
    private class OldFashionedPrinter : IMachine
    {
        public void Print(Document document)
        {
            Console.WriteLine("I can only print...");
        }

        public void Fax(Document document)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document document)
        {
            throw new NotImplementedException();
        }
    }
    
    private interface IPrinter
    {
        void Print(Document document);
    }
    
    private interface IScanner
    {
        void Scan(Document document);
    }
    
    private interface IFaxer
    {
        void Fax(Document document);
    }
    
    private interface IMultifunctionDevice : IPrinter, IScanner, IFaxer
    {
        
    }
    
    private class PhotoCopier : IScanner, IPrinter
    {
        public void Scan(Document document)
        {
            Console.WriteLine("I can scan");
        }

        public void Print(Document document)
        {
            Console.WriteLine("I can print");
        }
    }
}