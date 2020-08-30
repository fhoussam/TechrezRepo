using csharp_interviews;
using csharp_interviews.exos;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ut.exos
{
    public static class StudentsRepo 
    {
        public static Student[] Students =
        {
            new Student(Guid.NewGuid(),"student 01", 21, 12.00, Gender.Male),
            new Student(Guid.NewGuid(),"student 02", 20, 15.50, Gender.Male),
            new Student(Guid.NewGuid(),"student 03", 22, 17.50, Gender.Male),
            new Student(Guid.NewGuid(),"student 04", 20, 16.50, Gender.Female),
            new Student(Guid.NewGuid(),"student 05", 24, 19.00, Gender.Female),
            new Student(Guid.Parse("BA9BD34C-13D1-4D53-B7DC-F5A6A92AA730"),"student 06", 26, 20.00, Gender.Male),
            new Student(Guid.NewGuid(),"student 07", 27, 11.00, Gender.Male),
            new Student(Guid.NewGuid(),"student 08", 28, 08.50, Gender.Female),
            new Student(Guid.NewGuid(),"student 09", 19, 11.00, Gender.Male),
            new Student(Guid.NewGuid(),"student 10", 21, 15.00, Gender.Female),
            new Student(Guid.NewGuid(),"student 11", 20, 15.00, Gender.Female),
            new Student(Guid.NewGuid(),"student 12", 18, 15.50, Gender.Male),
            new Student(Guid.NewGuid(),"student 13", 29, 15.00, Gender.Male),
            new Student(Guid.NewGuid(),"student 14", 20, 15.50, Gender.Female),
            new Student(Guid.NewGuid(),"student 15", 17, 15.00, Gender.Male),
            new Student(Guid.NewGuid(),"student 16", 20, 15.00, Gender.Female),
            new Student(Guid.NewGuid(),"student 17", 22, 15.00, Gender.Female),
            new Student(Guid.NewGuid(),"student 18", 23, 15.50, Gender.Male),
            new Student(Guid.NewGuid(),"student 19", 22, 15.00, Gender.Female),
        };

        public static Student GetStudentById(Guid id) 
        {
            return Students.SingleOrDefault(x => x.Id == id);
        }
    }

    public class WhenGetMajor
    {
        [Fact]
        public void ThenMajorIsReturned()
        {
            var actual = LinqExos.GetMajor(StudentsRepo.Students);
            var expected = StudentsRepo.GetStudentById(Guid.Parse("BA9BD34C-13D1-4D53-B7DC-F5A6A92AA730"));
            actual.Should().Be(expected);
        }
    }
}
