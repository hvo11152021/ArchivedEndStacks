using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3PartA_HyVo
{
    /// <summary>
    /// Name: Hy Vo
    /// Project: Lab 3A
    /// </summary>
    public class Hourly : Employee
    {
        private int hourWorked;
        private decimal hourlyRate;

        public Hourly(string number, string fName, string lName, DateTime hired, DateTime birthDate, string phNumber, string Address, string Email, int hour, decimal rate)
            : base(number, fName, lName, hired, birthDate, phNumber, Address, Email)
        {
            hourWorked = hour;
            hourlyRate = rate;
        }

        public int HourWorked
        {
            get => hourWorked;
            set
            {
                if (int.TryParse(hourWorked.ToString(), out int temp))
                {
                    hourWorked = value;
                }
            }
        }

        public decimal HourlyRate
        {
            get => hourlyRate;
            set
            {
                if (int.TryParse(hourlyRate.ToString(), out int temp))
                {
                    hourlyRate = value;
                }
            }
        }

        public override decimal CalculatePay() => hourlyRate * hourWorked * 2;
    }
}
