using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3PartA_HyVo
{
    /// <summary>
    /// Name: Hy Vo
    /// Project: Lab 3A
    /// </summary>
    public class Supervisor : Employee
    {
        private decimal spvrSalary;

        public Supervisor(string number, string fName, string lName, DateTime hired, DateTime birthDate, string phNumber, string Address, string Email, decimal wage)
            : base(number, fName, lName, hired, birthDate, phNumber, Address, Email)
        {
            spvrSalary = wage;
        }

        public decimal SpvrSalary
        {
            get => spvrSalary;
            set
            {
                if (decimal.TryParse(spvrSalary.ToString(), out decimal temp))
                {
                    spvrSalary = value;
                }
            }
        }

        public override decimal Bonus() => spvrSalary + (spvrSalary * 0.10M);

        public override decimal CalculatePay() => spvrSalary / 26;
    }
}
