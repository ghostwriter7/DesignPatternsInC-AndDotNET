﻿namespace DesignPatterns.Solid;

public class T2_OpenClosedPrinciple
{
    public static void Demo()
    {
        var apple = new Product("Apple", Color.Green, Size.Small);
        var tree = new Product("Tree", Color.Green, Size.Large);
        var house = new Product("house", Color.Blue, Size.Large);

        Product[] products = [apple, tree, house];

        Console.WriteLine("Green products:");
        foreach (var product in ProductFilter.FilterByColor(products, Color.Green))
        {
            Console.WriteLine($" - {product.Name} is green");
        }
    }

    private enum Color
    {
        Red,
        Green, Blue
    }

    private enum Size
    {
        Small,
        Medium,
        Large
    }

    private class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
                throw new ArgumentNullException(paramName: nameof(name));
            
            Name = name;
            Color = color;
            Size = size;
        }
    }

    private class ProductFilter
    {
        public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var product in products)
            {
                if (product.Size == size)
                    yield return product;
            }
        }
        
        public static IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var product in products)
            {
                if (product.Color == color)
                    yield return product;
            }
        }
        
    }
}