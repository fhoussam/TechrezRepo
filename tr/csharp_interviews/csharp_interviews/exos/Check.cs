using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csharp_interviews.exos
{
    public static class Check
    {
        public static bool ProcessBrackets(string input) 
        {
            while (input.Contains("[]") || input.Contains("()"))
                input = input.Replace("[]", "").Replace("()", "");

            return string.IsNullOrEmpty(input);
        }
    }
}
