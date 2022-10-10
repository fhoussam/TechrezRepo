using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_interviews.Solid._2_LiskovSubstitution
{
    internal class LiskovSubstitutionBadWay
    {
        public static void MainMethod()
        {
            var rectangle = new Rectangle(5, 2);
            Console.WriteLine(rectangle);
            Console.WriteLine($"rectangle area is {GetArea(rectangle)}");

            Rectangle square = new Square(); //using pointer of type rectangle will call base setter and the area will be zero
            square.Width = 4; //check right way class for solution
            Console.WriteLine($"square has area of {GetArea(square)}");
        }

        public static int GetArea(Rectangle rectangle) => rectangle.Width * rectangle.Height;

        internal class Rectangle
        {
            public int Width { get; set; }
            public int Height { get; set; }

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
            public new int Width 
            { 
                set { base.Width = base.Height = value; } 
            }
            public new int Height 
            { 
                set { base.Height = base.Width = value; } 
            }
        }
    }
}
