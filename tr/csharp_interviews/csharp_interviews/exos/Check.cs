using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace csharp_interviews.exos
{
    public class Check
    {
        public static void CoreFunction()
        {
            bool result = CheckAll("[](()()[[]])()[]([])");
            Console.WriteLine(result);
        }

        private static bool CheckAll(string str) 
        {
            string strWithBigBrackets = new string(str.Where(x => x == '[' || x == ']').ToArray());
            string strWithSmallBrackets = new string(str.Where(x => x == '(' || x == ')').ToArray());
            bool checkSmallBrackets = CheckBrakets(strWithSmallBrackets);
            if (checkSmallBrackets)
            {
                return CheckBrakets(strWithBigBrackets);
            }
            else
                return false;
        }

        private static bool CheckBrakets(string str)
        {
            bool isBigBracket = str.All(x => x == '[' || x == ']');
            char openingBracket = isBigBracket ? '[' : '(';
            char closingBracket = isBigBracket ? ']' : ')';
            string stringToProcess = new string(str.Where(x => x == openingBracket || x == closingBracket).ToArray());

            while (!string.IsNullOrEmpty(stringToProcess))
            {
                try
                {
                    Shrink(ref stringToProcess, openingBracket, closingBracket);
                }
                catch (MyCustomException)
                {
                    return false;
                }
            }

            return string.IsNullOrEmpty(stringToProcess);
        }

        private static void Shrink(ref string str, char openingBracket, char closingBracket) 
        {
            var firstClosingIndex = str.IndexOf(closingBracket);
            var firstOpeningIndex = str.IndexOf(openingBracket);
            if (firstClosingIndex == -1 || firstOpeningIndex == -1)
                throw new MyCustomException();
            else
            {
                str = RemoveCharAt(str, firstClosingIndex);
                str = RemoveCharAt(str, firstOpeningIndex);
            }
        }

        private static string RemoveCharAt(string str, int index) 
        {
            var aa = str.ToList();
            aa.RemoveAt(index);
            return new string(aa.ToArray());
        }
    }

    public class MyCustomException : Exception{ }
}
