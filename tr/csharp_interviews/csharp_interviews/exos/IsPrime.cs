using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_interviews.exos
{
    public class IsPrime
    {
		public static bool MainMethod(int n)
		{
			if ((n & 1) == 0)
			{
				if (n == 2)
					return true;
				else
					return false;
			}

			for (int i = 3; (i * i) <= n; i += 2)
			{
				if ((n % i) == 0)
					return false;
			}

			return n != 1;
		}
	}
}
