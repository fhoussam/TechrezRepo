using System;

namespace csharp_interviews.exos.output
{
    public class Shape
    {
        public string GetName() { return "shape"; }
    }

    public class Ball : Shape
    {
        public new string GetName() { return "ball"; }
    }

    public class Pet
    {
        public virtual string GetName() { return "pet"; }
    }

    public class Cat : Pet
    {
        public override string GetName() { return "cat"; }

    }

    public static class Program2
    {
        public static void _Main()
        {
            Pet pet = new Cat();
            Shape shape = new Ball();

            Console.WriteLine("My {0} is playing with a {1}", pet.GetName(), shape.GetName());
        }
    }
}
