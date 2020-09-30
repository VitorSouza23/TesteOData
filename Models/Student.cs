using System;

namespace TesteOData.Models
{
    public class Student
    {
        public Student() { }
        public Student(Guid id, string name, int score) =>
            (Id, Name, Score) = (id, name, score);

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}