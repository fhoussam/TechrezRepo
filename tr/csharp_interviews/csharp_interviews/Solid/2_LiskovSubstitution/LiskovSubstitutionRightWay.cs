using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_interviews.Solid._2_LiskovSubstitution
{
    internal class LiskovSubstitutionRightWay
    {
        public static void MainMethod()
        {
            var rectangle = new Rectangle(5, 2);
            Console.WriteLine(rectangle);
            Console.WriteLine($"rectangle area is {GetArea(rectangle)}");

            Rectangle square = new Square(); 
            square.Width = 4; 
            Console.WriteLine($"square has area of {GetArea(square)}");
        }

        public static int GetArea(Rectangle rectangle) => rectangle.Width * rectangle.Height;

        internal class Rectangle
        {
            public virtual int Width { get; set; }
            public virtual int Height { get; set; }

            public Rectangle()
            {

            }

            public Rectangle(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public override string ToString()
            {
                return base.ToString();
            }
        }

        internal class Square : Rectangle
        {
            public override int Width
            {
                set { base.Width = base.Height = value; }
            }
            public override int Height
            {
                set { base.Height = base.Width = value; }
            }
        }
    }
}
