using System;

namespace Lab3PartA_HyVo
{
    /// <summary>
    /// Name: Hy Vo
    /// Project: Lab 3A
    /// </summary>
    public abstract class Employee : Object
    {
        //encapsulate fields
        private string siNumber;
        private string first;
        private string last;
        private DateTime hiredDate;
        private DateTime doB;
        private string phone;
        private string address;
        private string email;

        //company name
        public static string companyName = "SolarCity";

        //constructor
        public Employee(string number, string fName, string lName, DateTime hired, DateTime birthDate, string phNumber, string Address, string Email)
            : base()
        {
            siNumber = number;
            first = fName;
            last = lName;
            hiredDate = hired;
            doB = birthDate;
            phone = phNumber;
            address = Address;
            email = Email;
        }

        //validation properties
        public string SINumber
        {
            get => siNumber;
            set
            {
                if (siNumber.Length <= 9)
                {
                    siNumber = value;
                }
            }
        }

        public string First
        {
            get => first;
            set
            {
                if (first.Length <= 10 && !int.TryParse(first, out int temp))
                {
                    first = value;
                }
            }
        }

        public string Last
        {
            get => last;
            set
            {
                if (last.Length <= 10 && !int.TryParse(last, out int temp))
                {
                    last = value;
                }
            }
        }

        public string FullName
        {
            get => $"{first} {last}";
        }

        public DateTime HiredDate
        {
            get => hiredDate;
        }

        public DateTime DateOfBirth
        {
            get => doB;
            set
            {
                if (DateTime.TryParse(doB.ToString(), out DateTime temp))
                {
                    doB = value;
                }
            }
        }

        public string Phone
        {
            get => phone;
            set
            {
                if (phone.Length <= 11)
                {
                    phone = value;
                }
            }
        }

        public string Address
        {
            get => address;
            set { address = value; }
        }

        public string Email
        {
            get => email;
            set { email = value; }
        }

        //methods
        public abstract decimal CalculatePay();

        public virtual decimal Bonus() => 0M;
        
        public override string ToString() => $"Name: {first} {last}. \nPhone Number: {phone}";
    }
}
