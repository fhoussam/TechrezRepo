using System;
using System.Linq;

namespace csharp_interviews.exos
{
    public static class Reshaper2
    {
        public static void Reshape2(string input, int fragCount)
        {
            string a = string.Empty, b = string.Empty;
            string inputNoSpace = input.Replace(" ", "");
            for (int i = 0; i < inputNoSpace.Length; i++)
            {
                a += inputNoSpace[i].ToString();
                if ((i + 1) % fragCount == 0) 
                {
                    b += a + "\n";
                    a = string.Empty;
                }
            }
            Console.WriteLine(b.TrimEnd('\n'));
        }
    }
}
