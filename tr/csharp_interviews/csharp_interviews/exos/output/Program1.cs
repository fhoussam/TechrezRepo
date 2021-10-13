using System;

namespace csharp_interviews.exos.output
{
    public class SimpleClass
    {
        public string Value { get; set; }
    }

    public static class Program1
    {
        public static void _Main()
        {
            try
            {
                try
                {
                    SimpleClass instance = null;
                    var value = instance.Value;
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine("Local: NullReferenceException");
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Local: Exception");
                    throw;
                }
                finally
                {
                    Console.WriteLine("Local: Finally");
                }

                Console.WriteLine("End of try");
            }
            catch (Exception e)
            {

                Console.WriteLine($"Global: {e.GetType().Name}");
            }

            Console.ReadLine();
        }
    }
}
