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
            new Student(Guid.NewGuid(),"student 01", 21, 12.00, Gender.Male, City.NewYork),
            new Student(Guid.NewGuid(),"student 02", 20, 15.50, Gender.Male, City.Chicago),
            new Student(Guid.NewGuid(),"student 03", 22, 17.50, Gender.Male, City.LosAngelos),
            new Student(Guid.NewGuid(),"student 04", 20, 16.50, Gender.Female, City.LosAngelos),
            new Student(Guid.NewGuid(),"student 05", 14, 19.00, Gender.Female, City.NewYork),
            new Student(Guid.Parse("BA9BD34C-13D1-4D53-B7DC-F5A6A92AA730"),"student 06", 26, 20.00, Gender.Male, City.NewYork),
            new Student(Guid.NewGuid(),"student 07", 27, 11.00, Gender.Male, City.Chicago),
            new Student(Guid.NewGuid(),"student 08", 28, 08.50, Gender.Female, City.NewYork),
            new Student(Guid.NewGuid(),"student 09", 19, 11.00, Gender.Male, City.LosAngelos),
            new Student(Guid.NewGuid(),"student 10", 21, 15.00, Gender.Female, City.NewYork),
            new Student(Guid.NewGuid(),"student 11", 20, 15.00, Gender.Female, City.Chicago),
            new Student(Guid.NewGuid(),"student 12", 18, 15.50, Gender.Male, City.Chicago),
            new Student(Guid.NewGuid(),"student 13", 29, 15.00, Gender.Male, City.LosAngelos),
            new Student(Guid.NewGuid(),"student 14", 20, 15.50, Gender.Female, City.NewYork),
            new Student(Guid.NewGuid(),"student 15", 17, 15.00, Gender.Male, City.Chicago),
            new Student(Guid.NewGuid(),"student 16", 20, 15.00, Gender.Female, City.LosAngelos),
            new Student(Guid.NewGuid(),"student 17", 12, 15.00, Gender.Female, City.NewYork),
            new Student(Guid.NewGuid(),"student 18", 23, 15.50, Gender.Male, City.Chicago),
            new Student(Guid.NewGuid(),"student 19", 22, 15.00, Gender.Female, City.LosAngelos),
        };
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

    //simple group by : using an existing key mechanism like Enum
    public class WhenGetGenderGroups
    {
        [Fact]
        public void ThenGenderGroupsReturned()
        {
            var actual = LinqExos.GetGenderGroupsLambda(StudentsRepo.Students);
            actual.Should().HaveCount(2);
            actual.Single(x => x.Key == Gender.Female).Should().HaveCount(9);
            actual.Single(x => x.Key == Gender.Male).Should().HaveCount(10);
        }
    }

    //group by a boolean key : teen/adult
    public class WhenGetAgeGroups
    {
        [Fact]
        public void ThenAgeGroupsReturned()
        {
            var actual = LinqExos.GetAgeGroups(StudentsRepo.Students);
            actual.Should().HaveCount(2);
            actual.Should().ContainSingle(x => x.Gender == Gender.Female && Math.Round(x.AverageAge, 2) == 19.67 && x.MembersCount == 9);
            actual.Should().ContainSingle(x => x.Gender == Gender.Male && x.AverageAge == 22.2 && x.MembersCount == 10);
        }
    }

    //group by gender using lambda
    public class WhenGetAgeGroupsUsingLambdaExpressionAndSingleKey
    {
        [Fact]
        public void ThenAgeGroupsReturned()
        {
            var actual = LinqExos.GetAgeGroupsLambda(StudentsRepo.Students);
            actual.Should().HaveCount(2);
            actual.Should().ContainSingle(x => x.Gender == Gender.Female && Math.Round(x.AverageAge, 2) == 19.67 && x.MembersCount == 9);
            actual.Should().ContainSingle(x => x.Gender == Gender.Male && x.AverageAge == 22.2 && x.MembersCount == 10);
        }
    }

    //groups using double key
    public class WhenGetGenderCityGroups
    {
        [Fact]
        public void ThenGenderCityGroupsReturned()
        {
            var actual = LinqExos.GetGenderCityGroups(StudentsRepo.Students);
        }
    }

    //groups using double key
    public class WhenGetGenderCityGroupsUsingLambda
    {
        [Fact]
        public void ThenGenderCityGroupsReturned()
        {
            var actual = LinqExos.GetGenderCityGroupsLamda(StudentsRepo.Students);
        }
    }
}
