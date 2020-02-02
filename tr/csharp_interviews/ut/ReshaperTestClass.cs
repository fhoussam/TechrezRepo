using csharp_interviews.exos;
using NUnit.Framework;
using System;

namespace Ut
{
    [TestFixture]
    public class ReshaperTestClass
    {
        public Reshaper Reshaper { get; set; }

        [SetUp]
        public void Setup()
        {
            //re-arrange for every test case
            Reshaper = new Reshaper();
        }

        [TearDown]
        public void Teardown()
        {
            //post execution of every test cas, for disposing disposable objects or clearing some file or memery cleaning
        }

        [TestCase("142 5 54 963 212 225 190", 4616516, "")]
        [TestCase("142 5 54 963 212 225 190", 2, "14 buzz fizz fizz 32 fizz 22 fizz fizzbuzz")]
        public void ShouldBeOk(string input, int fragCount, string expected)
        {
            if (string.IsNullOrEmpty(input)) throw new NullReferenceException();

            //act
            string result = Reshaper.Reshape(input, fragCount);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(null, null)]
        public void ShouldNotBeOk(string input, int fragCount)
        {
            Assert.That(() => Reshaper.Reshape(input, fragCount), Throws.TypeOf<NullReferenceException>());
        }
    }
}
