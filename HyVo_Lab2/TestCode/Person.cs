using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCode
{
    class Person
    {
        private int id;
        private string name;

        public int PersonID
        {
            get => id; 
            set => id = value;
        }

        public string PersonName
        {
            get => name; 
            set => name = value;
        }

        public Person()
        {
            id = 7654321;
            name = "Peter Vanscoy";
        }

        public Person(int personID, string personName)
        {
            this.id = personID;
            this.name = personName;
        }

        public override string ToString()
        {
            return $"Person ToString(): \nName: {name} \nID: {id}";
        }

    }
}
