namespace csharp_interviews.exos
{
    public class Factorial
    {
        //dummy way
        public static long GetFactorialSimple(int number)
        {
            long result = 1;
            int multiplier = number;

            while (multiplier > 1)
            {
                result = result * multiplier;
                multiplier--;
            }

            return result;
        }

        //using state
        private long _facorial;

        public Factorial(long facorial)
        {
            _facorial = facorial;
        }

        public long GetFactorialRecursionWithState()
        {
            return _facorial == 0 ? 1 : _facorial-- * GetFactorialRecursionWithState();
        }

        //without using state but with delegate
        private delegate long FactorialDelegate();
        public static long GetFactorialRecursionWithoutState(int number)
        {
            FactorialDelegate getFactorial = null;
            getFactorial = () => number == 0 ? 1 : number-- * getFactorial();
            return getFactorial();
        }

        //wihtout using state or delegate
        public static long GetFactorialRecursion(int number)
        {
            return number == 0 ? 1 : number == 1 || number == 2 ? number : number * GetFactorialRecursion(number - 1);
        }
    }
}