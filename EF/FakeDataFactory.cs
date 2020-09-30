using System;
using System.Collections.Generic;
using TesteOData.Models;

namespace TesteOData.EF
{
    internal static class FakeDataFactory
    {
        public static IEnumerable<Student> CreateStudents(int count = 50)
        {
            for(int counter = 0; counter < count; counter++)
            {
                yield return new Student(Guid.NewGuid(), $"Teste {counter}", counter);
            }
        }

        public static void SaveFakeStudents(IEnumerable<Student> students, ApiContext apiContext)
        {
            apiContext.Students.AddRange(students);
            apiContext.SaveChanges();
        }
    }
}