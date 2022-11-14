using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Name: Hy Vo
//PROG1621: Lab 2

namespace HyVo_Lab2
{
    public class Goal
    {
        private string name;
        private Timeline timePriority;
        private Goal next;

        public Timeline TimePriority => timePriority;

        public Goal Next
        {
            get => next;
            set => next = value;
        }

        public Goal(string name, Timeline time)
        {
            this.name = name;
            this.timePriority = time;
            this.next = null;
        }

        public override string ToString() => $"{name} - {timePriority}\n\n";
    }
}
