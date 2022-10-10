using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace csharp_interviews.Solid._1_OpenClosed
{
    internal class OpenClosedRightWay
    {
        public static void MainMethod()
        {
            var redColorSpecification = new ColorSpecification(Color.Red);
            var filterResult_1 = new Filter().GetResult(Product.GetProducts(), redColorSpecification);
            Console.WriteLine("\nprinting red products");
            foreach (var item in filterResult_1)
            {
                Console.WriteLine(item);
            }

            var largeSizeSpecification = new SizeSpecification(Size.Large);
            var filterResult_2 = new Filter().GetResult(Product.GetProducts(), largeSizeSpecification);
            Console.WriteLine("\nprinting large products");
            foreach (var item in filterResult_2)
            {
                Console.WriteLine(item);
            }

            var smallSizeSpecification = new SizeSpecification(Size.Small);
            var combinedSpecification = new ColorAndSizeSpecification(redColorSpecification, smallSizeSpecification);
            var filterResult_3 = new Filter().GetResult(Product.GetProducts(), combinedSpecification);
            Console.WriteLine("\nprinting small and red products");
            foreach (var item in filterResult_3)
            {
                Console.WriteLine(item);
            }
        }

        public interface IFilter<T>
        {
            public IEnumerable<T> GetResult(IEnumerable<T> products, ISpecification<T> specification);
        }

        public interface ISpecification<T>
        {
            public bool IsStatisfied(T t);
        }

        public class ColorSpecification : ISpecification<Product>
        {
            public Color Color { get; set; }

            public ColorSpecification(Color color)
            {
                Color = color;
            }

            public bool IsStatisfied(Product product)
            {
                return product.Color == Color;
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            public Size Size { get; set; }

            public SizeSpecification(Size size)
            {
                Size = size;
            }

            bool ISpecification<Product>.IsStatisfied(Product product)
            {
                return product.Size == Size;
            }
        }

        public class ColorAndSizeSpecification : ISpecification<Product>
        {
            public ColorAndSizeSpecification(ISpecification<Product> firstSpecification, ISpecification<Product> secondSpecification)
            {
                FirstSpecification = firstSpecification;
                SecondSpecification = secondSpecification;
            }

            public ISpecification<Product> FirstSpecification { get; set; }
            public ISpecification<Product> SecondSpecification { get; set; }

            public bool IsStatisfied(Product t)
            {
                return FirstSpecification.IsStatisfied(t) && SecondSpecification.IsStatisfied(t);
            }
        }

        public class Filter : IFilter<Product>
        {
            public IEnumerable<Product> GetResult(IEnumerable<Product> products, ISpecification<Product> specification)
            {
                foreach (var product in products)
                {
                    if (specification.IsStatisfied(product))
                        yield return product;
                }
            }
        }
    }
}
