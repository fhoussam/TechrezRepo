﻿namespace csharp_interviews.exos
{
    using System;

    class Change
    {
        public long coin2 { get; set; } = 0;
        public long bill5 { get; set; } = 0;
        public long bill10 { get; set; } = 0;

        public static Change OptimalChange(long s)
        {
            var result = new Change();

            if (s == 0)
                return result;

            if (s < 0 || s == 1)
                return null;

            if (s >= 10)
            {
                result.bill10 = s / 10;
                s -= result.bill10 * 10;
            }

            if (s >= 5)
            {
                result.bill5 = s / 5;
                s -= result.bill5 * 5;
            }

            if (s >= 2)
            {
                result.coin2 = s / 2;
                s -= result.coin2 * 2;
            }

            if (s != 0)
            {
                if (s == 1)
                {
                    if (result.bill5 >= 1)
                    {
                        result.bill5--;
                        result.coin2 += 3;
                    }

                    else if (result.bill10 >= 1)
                    {
                        result.bill10--;
                        result.bill5++;
                        result.coin2 += 3;
                    }
                }
                else
                    return null;
            }

            return result;
        }

        public static void MainMethod()
        {
            long s = 31; 
            Change m = OptimalChange(s);

            if (m == null)
                Console.WriteLine("Impossible");
            else
            {
                Console.WriteLine("02 - Coin(s): " + m.coin2);
                Console.WriteLine("05 - Bill(s): " + m.bill5);
                Console.WriteLine("10 - Bill(s): " + m.bill10);

                long result = m.coin2 * 2 + m.bill5 * 5 + m.bill10 * 10;
                Console.WriteLine("\nChange given = " + result);
            }
        }
    }
}
