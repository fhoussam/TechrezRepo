using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_interviews.Solid._1_OpenClosed
{
    internal class OpenClosedBadWay
    {
        public static void MainMethod()
        {
            var filterResult_1 = OpenClosedBadWay.ColorFilter.Filter
                (Product.GetProducts(), Color.Red);

            Console.WriteLine("\nprinting red products");

            foreach (var item in filterResult_1)
            {
                Console.WriteLine(item);
            }

            var filterResult_2 = OpenClosedBadWay.SizeFilter.Filter
                (Product.GetProducts(), Size.Large);

            Console.WriteLine("\nprinting large products");

            foreach (var item in filterResult_2)
            {
                Console.WriteLine(item);
            }

            var filterResult_3 = OpenClosedBadWay.SizeAndColorFilter.Filter
                (Product.GetProducts(), Color.Red, Size.Small);

            Console.WriteLine("\nprinting small and red products");

            foreach (var item in filterResult_3)
            {
                Console.WriteLine(item);
            }
        }

        public class ColorFilter
        {
            public static IEnumerable<Product> Filter
                (IEnumerable<Product> products, Color color)
            {
                foreach (var product in products)
                {
                    if (product.Color == color)
                        yield return product;
                }
            }
        }
        public class SizeFilter
        {
            public static IEnumerable<Product> Filter
                (IEnumerable<Product> products, Size size)
            {
                foreach (var product in products)
                {
                    if (product.Size == size)
                        yield return product;
                }
            }
        }
        public class SizeAndColorFilter
        {
            public static IEnumerable<Product> Filter
                (IEnumerable<Product> products, Color color, Size size)
            {
                foreach (var product in products)
                {
                    if (product.Size == size && product.Color == color)
                        yield return product;
                }
            }
        }
    }
}
