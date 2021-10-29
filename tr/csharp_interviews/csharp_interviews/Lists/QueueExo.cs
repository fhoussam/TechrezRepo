using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_interviews.Lists
{
    public static class QueueExo
    {
        public static void _Main() 
        {
            Queue<char> myStack = new Queue<char>();
            myStack.Enqueue('H');
            myStack.Enqueue('O');
            myStack.Enqueue('U');
            myStack.Enqueue('S');
            myStack.Enqueue('S');
            myStack.Enqueue('A');
            myStack.Enqueue('M');
            foreach (var item in myStack)
            {
                Console.Write(item);
            }
            Console.ReadLine();
        }
    }
}
