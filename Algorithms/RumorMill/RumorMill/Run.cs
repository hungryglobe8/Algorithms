using System;
using System.Collections.Generic;

namespace RumorMill
{
    public class Run
    {
        public static void Main()
        {
            InputReader reader = new InputReader();
            reader.ReadInput();
            IList<Student> students = reader.GetStudents();
            IList<Student> rumors = reader.GetRumors();

            School school = new School(students);
            school.GenerateReports(rumors);
        }
    }
}
