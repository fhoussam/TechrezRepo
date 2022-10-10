using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_interviews.Solid._1_OpenClosed
{
    internal class Product
    {
        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }

        public string Name { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }

        public override string ToString()
        {
            return $"Name : {Name}, Size : {Size}, Color : {Color}";
        }

        public static List<Product> GetProducts() 
        {
            return new List<Product>()
            {
                new Product("Tomato", Color.Red, Size.Small),
                new Product("House", Color.White, Size.Large),
                new Product("Kiwi", Color.Green, Size.Small),
                new Product("Table", Color.White, Size.Medium),
                new Product("Stawberry", Color.Red, Size.Small),
            };
        }
    }

    internal enum Color 
    { 
        Red, Green, Blue, White
    }

    internal enum Size 
    { 
        Small, Medium, Large
    }
}
