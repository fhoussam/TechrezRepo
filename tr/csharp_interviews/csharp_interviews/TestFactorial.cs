using csharp_interviews.exos;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ut
{
    public class TestFactorialSimple
    {
        [Fact]
        public void ThenFactorialOf5Is120()
        {
            var actual = Factorial.GetFactorialSimple(5);
            var expected = 120;
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(5, 120)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void ThenResutShouldBeOk(int input, long expected)
        {
            var actual = Factorial.GetFactorialSimple(input);
            actual.Should().Be(expected);
        }
    }

    public class TestFactorialRecursionWithState
    {
        [Theory]
        [InlineData(5, 120)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void ThenResutShouldBeOk(int number, long expected)
        {
            var actual = new Factorial(number).GetFactorialRecursionWithState();
            actual.Should().Be(expected);
        }
    }

    public class TestFactorialRecursionWithoutState
    {
        [Theory]
        [InlineData(5, 120)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void ThenResutShouldBeOk(int number, long expected)
        {
            var actual = Factorial.GetFactorialRecursionWithoutState(number);
            actual.Should().Be(expected);
        }
    }

    public class TestFactorialRecursionWithoutStateOrDelegate
    {
        [Theory]
        [InlineData(5, 120)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void ThenResutShouldBeOk(int input, long expected)
        {
            var actual = Factorial.GetFactorialRecursion(input);
            actual.Should().Be(expected);
        }
    }
}
