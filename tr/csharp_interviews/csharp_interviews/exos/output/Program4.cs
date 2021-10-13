using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp_interviews.exos.output
{
    public static class Program4
    {
        public class Person { public string Name { get; set; } }

        private static void Variant_1()
        {
            var original = new List<Person>()
            {
                new Person() { Name = "Mike" },
                new Person() { Name = "John" },
                new Person() { Name = "David" }
            };

            var duplicate = original.Select(p => p).ToList();
            original.RemoveAt(0);
            Console.Write("1.");
            Console.WriteLine(string.Join(" and ", duplicate.Select(p => p.Name)));
        }

        private static void Variant_2()
        {
            var original = new List<Person>()
            {
                new Person() { Name = "Mike" },
                new Person() { Name = "David" }
            };

            var duplicate = original;
            original[0].Name = "John";
            Console.Write("2.");
            Console.WriteLine(string.Join(" and ", duplicate.Select(p => p.Name)));
        }

        private static void Variant_3()
        {
            var original = new List<Person>()
            {
                new Person() { Name = "Mike" },
                new Person() { Name = "John" },
                new Person() { Name = "David" }
            };

            var duplicate = original;
            original.RemoveAt(0);
            Console.Write("3.");
            Console.WriteLine(string.Join(" and ", duplicate.Select(p => p.Name)));
        }

        private static void Variant_4()
        {
            var original = new List<Person>()
            {
                new Person() { Name = "John" },
                new Person() { Name = "David" }
            };

            var person1 = original[0];
            var person2 = original[1];
            original = null;
            Console.Write("4.");
            Console.WriteLine($"{person1.Name} and {person2.Name}");
        }

        private static void Variant_5()
        {
            var original = new List<Person>()
            {
                new Person() { Name = "John" },
                new Person() { Name = "David" }
            };

            var somebody = original[0];
            somebody.Name = "Linda";
            Console.Write("5.");
            Console.WriteLine(string.Join(" and ", original.Select(p => p.Name)));
        }

        public static void _Main()
        {
            Variant_1();
            Variant_2();
            Variant_3();
            Variant_4();
            Variant_5();
        }
    }
}
