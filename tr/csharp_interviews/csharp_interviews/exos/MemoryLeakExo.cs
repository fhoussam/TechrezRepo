using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace csharp_interviews.exos
{
	public class MemoryLeakExo
	{
		// keep these two fields as they​​​​​​‌​​​‌​‌‌​‌‌​​‌​​​‌​​‌​‌‌​ are
		private Object[] elements;
		private int size = 0;

		public MemoryLeakExo(int initialCapacity)
		{
			elements = new Object[initialCapacity];
		}

		public void Push(object o)
		{
			EnsureCapacity();
			elements[size++] = o;
		}

		public object Pop()
		{
			if (size == 0)
			{
				throw new InvalidOperationException();
			}

			var x = elements.ToList();
			x.Remove(x.Count - 1);
			return x.ToArray();
		}

		private void EnsureCapacity()
		{
			if (elements.Length == size)
			{
				Object[] old = elements;
				elements = new Object[2 * size + 1];
				old.CopyTo(elements, 0);
			}
		}

		public static void MainMethod() 
		{
			MemoryLeakExo stack = new MemoryLeakExo(10000);

			Console.WriteLine("Memory Use (approx.): " + (GC.GetTotalMemory(true) / 1024) + " KBytes");

			for (int i = 0; i < 10000; i++) // fill the stack
				stack.Push("a large, large, large, large, string " + i);

			for (int i = 0; i < 10000; i++) // empty the stack
				stack.Pop();

			Console.WriteLine("Memory Use (approx.): " + (GC.GetTotalMemory(true) / 1024) + " KBytes");
		}
	}
}
