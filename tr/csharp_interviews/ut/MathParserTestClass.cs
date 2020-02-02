using csharp_interviews.exos;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace ut
{
    [TestFixture]
    public class MathParserTestClass
    {
        MathParser MathParser;

        [SetUp]
        public void Setup() 
        {
            MathParser = new MathParser();
        }

        [TestCase("2 * 3", 6)]
        [TestCase("8 + 4", 12)]
        [TestCase("4 * 8", 32)]
        [TestCase("8 - 2", 6)]
        [TestCase("2 * 3 * 8", 48)]

        public void UniOperator(string input, int expected)
        {
            int result = MathParser.Parse(input);
            Assert.AreEqual(expected, result);
        }
        
        [TestCase("3 + 6 - 9 + 5 + 6 * 8 + 12 + 24 - 6 + 6 * 3 + 8 + 1 * 2 * 3", 115)]
        public void MultiOperator(string input, int expected)
        {
            int result = MathParser.Parse(input);
            Assert.AreEqual(expected, result);
        }
    }
}
