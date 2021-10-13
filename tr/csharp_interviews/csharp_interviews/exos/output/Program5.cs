using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_interviews.exos.output
{
    public static class Program5
    {
        #region scenario 1
        //this block will not compile since ParentStruct1 is not a class or an interface
        //public struct ParentStruct1 
        //{
        //    public void PrintTrue() { Console.WriteLine(true); }
        //}

        //public struct ChildStruct1 : ParentStruct1 { }
        #endregion

        #region scenario 2

        public struct SimpleStruct_1
        {
            public int Value { get; set; }
        }

        private static void _Main_2()
        {
            var simpleStruct = new SimpleStruct_1 { Value = 5 };
            ChangeValue_2(simpleStruct);
            Console.WriteLine(simpleStruct.Value == 5);
        }

        static void ChangeValue_2(SimpleStruct_1 simpleStruct)
        {
            simpleStruct.Value = 10;
        }

        #endregion

        #region scenario 3

        //simpleStruct variable is a value type variable and therefore, needs to be intializzd before use

        //public struct SimpleStruct_2
        //{
        //    public int Value { get; set; }
        //}

        //private static void _Main_2()
        //{
        //    SimpleStruct_2 simpleStruct = null;
        //    Console.WriteLine(simpleStruct == null);
        //}

        #endregion

        #region scenario 4

        public interface ISimpleStruct_4
        {
            int Value { get; set; }
        }

        public class SimpleStruct_4 : ISimpleStruct_4
        {
            public int Value { get; set; }
        }

        private static void _Main_4()
        {
            var simpleStruct = new SimpleStruct_4 { Value = 5 };
            Console.WriteLine(simpleStruct.Value == 5);
        }

        #endregion

        public static void _Main() 
        {
            _Main_2();
            _Main_4();
        }
    }
}
