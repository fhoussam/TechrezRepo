using csharp_interviews.exos;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ut.exos
{
    public class CheckValidString
    {
        [Fact]
        public void ThenShouldReturnTrue() 
        {
            var expected = true;
            var input = "[](()()[[]])()[]([])";
            var actual = Check.ProcessBrackets(input);
            actual.Should().Be(expected);
        }
    }
}
