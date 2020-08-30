using System;

namespace csharp_interviews
{
    public class Student
    {
        public Student(Guid id, string fullName, int age, double exam, Gender gender)
        {
            Id = id;
            FullName = fullName;
            Age = age;
            Exam = exam;
            Gender = gender;
        }

        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public int Age { get; private set; }
        public double Exam { get; private set; }
        public Gender Gender { get; private set; }
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
    }
}
