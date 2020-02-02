using System.Linq;

namespace csharp_interviews.exos
{
    public class Reshaper
    {
        //142 5 54 963 212 225 190
        //14 25 54 96 32 12 22 51 90
        //14 buzz fizz fizz 32 fizz 22 fizz fizzbuzz
        public string Reshape(string input, int fragCount)
        {
            var raw = input.Replace(" ", "").ToList();
            string result = string.Empty;
            string frag = string.Empty;
            for (int i = 0; i < raw.Count; i++)
            {
                frag += raw[i];
                if ((i + 1) % fragCount == 0 & i != 0)
                {
                    int n = int.Parse(frag);
                    result += (n % 3 == 0 && n % 5 == 0 ? "fizzbuzz" : n % 3 == 0 ? "fizz" : n % 5 == 0 ? "buzz" : n.ToString()) + " ";
                    frag = string.Empty;
                }
            }

            return result.TrimEnd(' ');
        }
    }
}
