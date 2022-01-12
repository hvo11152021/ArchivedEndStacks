using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCode
{
    struct Course
    {
        private static string code = "PROG1224";
        private static double grade = 74.50;

        public string CourseCode
        {
            get => code; 
            set => code = value;
        }

        public double CourseGrade
        {
            get => grade; 
            set => grade = value;
        }

        public Course(string courseCode, double CourseGrade)
        {
            code = courseCode;
            grade = CourseGrade;
        }

    }
}
