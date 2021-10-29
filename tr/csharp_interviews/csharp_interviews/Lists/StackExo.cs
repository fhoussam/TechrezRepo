using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_interviews.Lists
{
    public static class StackExo
    {
        public static void _Main() 
        {
            Stack<char> myStack = new Stack<char>();
            myStack.Push('H');
            myStack.Push('O');
            myStack.Push('U');
            myStack.Push('S');
            myStack.Push('S');
            myStack.Push('A');
            myStack.Push('M');
            foreach (var item in myStack)
            {
                Console.Write(item);
            }
            Console.ReadLine();
        }
    }
}
