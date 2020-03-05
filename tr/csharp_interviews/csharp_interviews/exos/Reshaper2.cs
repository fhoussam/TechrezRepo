using System.Linq;

namespace csharp_interviews.exos
{
    public static class Reshaper2
    {
        public static void Reshape2(string input, int fragCount)
        {
            string a = string.Empty, b = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                a += "";
                if (i % fragCount == 0) 
                {
                    b += a + "\n";
                    a = string.Empty;
                }
            }
            System.Console.WriteLine(a.TrimEnd('\n'));
        }
    }
}
