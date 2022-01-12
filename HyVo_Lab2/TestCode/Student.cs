using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCode
{
    class Student : Person
    {
        private Course[] courses = new Course[6];
        private Status status;

        public double GetAverage { get; set; }

        public string GetHighestMark { get; set; }

        public int Length
        {
            get => courses.Length;
        }

        public Status StudentStatus
        {
            get => status;
        }

        public Course this[int i]
        {
            get => courses[i];
            set => courses[i] = value;
        }

        public Student(Status stats) : base(1111111, "John Doe")
        {
            status = stats;
        }

        public override string ToString()
        {
            return $"Student ToString(): \nName: {PersonName} \nID: {PersonID} \nStatus: {status}";
        }

    }
}
