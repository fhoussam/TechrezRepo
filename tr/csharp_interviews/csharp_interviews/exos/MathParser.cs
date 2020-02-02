using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace csharp_interviews.exos
{
    public class MathParser
    {
        public int Parse(string input)
        {
            while (input.Contains("*"))
            {
                var match = new Regex(@"\d \* \d").Match(input);
                if (match.Success)
                {
                    var value = match.Value;
                    var sides = value.Split(new char[] { ' ', '*' }).Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    int intValue = int.Parse(sides[0]) * int.Parse(sides[1]);
                    input = input.Replace(value, intValue.ToString());
                }
                else
                    break;
            }

            return input.Replace("- ", "+ -").Split('+').Select(x => int.Parse(x)).Sum();
        }
    }
}
