//Object Oriented Programming
//P Vanscoy
//SavingsAccount

using System;

namespace BankOfVanscoy
{
	public class SavingsAccount : BankAccount
	{
		private decimal rate;

		private static decimal defaultRate = 0.04M;

		private static decimal charge = 0.50M;

		public SavingsAccount(int number, DateTime dateOpen)
			:base(number, dateOpen)
		{
			this.rate = defaultRate;
		}
		public SavingsAccount(int number, DateTime dateOpen, string first, string last)
			:base(number, dateOpen, first, last)
		{
			this.rate = defaultRate;
		}
		public SavingsAccount(int number, DateTime dateOpen, string first, string last, decimal rate)
			:base(number, dateOpen, first, last)
		{
			this.Rate = rate;
		}

		public decimal Rate
		{
			get
			{
				return this.rate;
			}
			set
			{
				this.rate = (value >= 0M) ? value : defaultRate;
			}
		}

		public static decimal DefaultRate
		{
			get
			{
				return defaultRate;
			}
			set
			{
				defaultRate = (value > 1M) ? (value / 100) : (value > 0) ? value : defaultRate;
			}
		}
		public static decimal Charge
		{
			get
			{
				return charge;
			}
			set
			{
				charge = (value > 0M) ? value : charge;
			}
		}

		public decimal ApplyInterest()
		{
			decimal interest = base.balance * this.rate;
			base.balance += interest;
			return interest;
		}

		public override string ToString()
		{
			return base.ToString() + String.Format("\n\tRate: {0:P}",this.rate);
		}

		public override decimal WithDraw(decimal amount)
		{
			if(amount > 0M && base.balance >= (amount + charge))
			{
				base.balance -= (amount += charge);
				return amount;
			}
			else
				return 0M;
		}
	}
}
