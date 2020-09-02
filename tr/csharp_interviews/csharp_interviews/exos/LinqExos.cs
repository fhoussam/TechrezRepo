using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csharp_interviews.exos
{
    public static class LinqExos
    {
        public static Student GetMajor(IEnumerable<Student> students)
        {
            return students.OrderByDescending(x => x.Exam).FirstOrDefault();
        }
        public static List<IGrouping<Gender, Student>> GetGenderGroupsLambda(IEnumerable<Student> students)
        {
            return students.GroupBy(x => x.Gender).ToList();
        }

        public static List<IGrouping<bool, Student>> GetAgeGroupsLambdaRaw(IEnumerable<Student> students)
        {
            return students.GroupBy(x => x.Age < 18).ToList();
        }

        public static IEnumerable<GenderViewModel> GetAgeGroupsLambda(IEnumerable<Student> students)
        {
            var query = students
                .GroupBy(x => x.Gender)
                .Select(x => new GenderViewModel()
                {
                    Gender = x.Key,
                    AverageAge = x.Average(x => x.Age),
                    MembersCount = x.Count(),
                });

            return query.ToList();
        }

        public static IEnumerable<GenderViewModel> GetAgeGroups(IEnumerable<Student> students)
        {
            var query = from s in students
                        group s by s.Gender into g
                        select new GenderViewModel
                        {
                            Gender = g.Key,
                            AverageAge = g.Average(x => x.Age),
                            MembersCount = g.Count()
                        };

            return query.ToList();
        }

        public static IEnumerable<GenderCityViewModel> GetGenderCityGroups(IEnumerable<Student> students) 
        {
            var query = from s in students
                        group s by new { s.Gender, s.City } into g
                        select new GenderCityViewModel 
                        { 
                            City = g.Key.City,
                            Gender = g.Key.Gender,
                            AverageAge = g.Average(x=>x.Age),
                            MembersCount = g.Count()
                        };

            return query;
        }

        public static IEnumerable<GenderCityViewModel> GetGenderCityGroupsLamda(IEnumerable<Student> students) 
        {
            var query = students.GroupBy(x => new { x.Gender, x.City })
                .Select(x => new GenderCityViewModel() 
                { 
                    Gender = x.Key.Gender,
                    City = x.Key.City,
                    AverageAge = x.Average(x=>x.Age),
                    MembersCount = x.Count(),
                });

            return query.ToList();
        }
    }

    public class GenderViewModel
    {
        public Gender Gender { get; set; }
        public int MembersCount { get; set; }
        public double AverageAge { get; set; }
    }

    public class GenderCityViewModel
    {
        public Gender Gender { get; set; }
        public City City { get; set; }
        public int MembersCount { get; set; }
        public double AverageAge { get; set; }
    }
}
