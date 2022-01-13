using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3PartA_HyVo
{
    /// <summary>
    /// Name: Hy Vo
    /// Project: Lab 3A
    /// </summary>
    public class EngineeringManager : Supervisor
    {
        private decimal monthlyRate;

        public EngineeringManager(string number, string fName, string lName, DateTime hired, DateTime birthDate, string phNumber, string Address, string Email, decimal wage, decimal monthlyWage)
            : base(number, fName, lName, hired, birthDate, phNumber, Address, Email, wage)
        {
            monthlyRate = monthlyWage;
        }

        public decimal MonthlyWage
        {
            get => monthlyRate;
            set
            {
                if (decimal.TryParse(monthlyRate.ToString(), out decimal temp))
                {
                    monthlyRate = value;
                }
            }
        }

        public override decimal Bonus() => monthlyRate + (monthlyRate * 0.05M);

        public override decimal CalculatePay() => monthlyRate / 2;
    }
}
