using Product = DesignPatterns.Solid.T2_OpenClosedPrinciple.Product;
using Color = DesignPatterns.Solid.T2_OpenClosedPrinciple.Color;
using Size = DesignPatterns.Solid.T2_OpenClosedPrinciple.Size;

namespace DesignPatterns.Solid;

/*
 * Specification Pattern - Enterprise pattern, not from gang of four
 * Potential solution to ProductFilter implementation from OpenClosedPrinciple violation example
 */
public class T3_SpecificationPattern
{
    public static void Demo()
    {
        var apple = new Product("Apple", Color.Green, Size.Small);
        var tree = new Product("Tree", Color.Green, Size.Large);
        var house = new Product("house", Color.Blue, Size.Large);

        Product[] products = [apple, tree, house];
        
        var betterFilter = new BetterFilter();
        Console.WriteLine("Green products");
        foreach (var product in betterFilter.Filter(products, new ColorSpecification(Color.Green)))
        {
            Console.WriteLine($" - {product.Name} is green");
        }

        Console.WriteLine("Large, blue items");
        foreach (var product in betterFilter.Filter(products, new AndSpecification<Product>(
                     new ColorSpecification(Color.Blue),
                     new SizeSpecification(Size.Large)
                     )))
        {
            Console.WriteLine($" - {product.Name} is large and blue");
        }
    }
    
    private interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }
    
    private interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    private class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> products, ISpecification<Product> spec)
        {
            foreach (var product in products)
                if (spec.IsSatisfied(product))
                    yield return product;
        }
    }
    
    private class ColorSpecification(Color color) : ISpecification<Product>
    {
        public bool IsSatisfied(Product t) => t.Color == color;
    }

    private class SizeSpecification(Size size) : ISpecification<Product>
    {
        public bool IsSatisfied(Product p) => p.Size == size;
    }

    private class AndSpecification<T>(ISpecification<T> firstSpec, ISpecification<T> secondSpec) : ISpecification<T>
    {
        public bool IsSatisfied(T t)
        {
            return firstSpec.IsSatisfied(t) && secondSpec.IsSatisfied(t);
        }
    }
}