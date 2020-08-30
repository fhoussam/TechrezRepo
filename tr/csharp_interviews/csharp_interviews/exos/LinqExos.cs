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
    }
}
