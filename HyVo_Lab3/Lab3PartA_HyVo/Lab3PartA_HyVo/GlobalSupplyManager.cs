using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3PartA_HyVo
{
    /// <summary>
    /// Name: Hy Vo
    /// Project: Lab 3A
    /// </summary>
    public class GlobalSupplyManager : Employee
    {
        private decimal bsmSalary;

        public GlobalSupplyManager(string number, string fName, string lName, DateTime hired, DateTime birthDate, string phNumber, string Address, string Email, decimal wage)
            : base(number, fName, lName, hired, birthDate, phNumber, Address, Email)
        {
            bsmSalary = wage;
        }

        public decimal BsmSalary
        {
            get => bsmSalary;
            set
            {
                if (decimal.TryParse(bsmSalary.ToString(), out decimal temp))
                {
                    bsmSalary = value;
                }
            }
        }

        public override decimal Bonus() => bsmSalary + (bsmSalary* 0.05M);

        public override decimal CalculatePay() => bsmSalary / 26;
    }
}
