//Object Oriented Programming
//P Vanscoy
//ChequingAccount

using System;

namespace BankOfVanscoy
{
	public class ChequingAccount : BankAccount
	{
		private decimal fee;
		private static decimal defaultFee = 5M;
		
		public ChequingAccount(int number, DateTime dateOpen)
			:base(number, dateOpen)
		{
			this.fee = defaultFee;
		}
		public ChequingAccount(int number, DateTime dateOpen, string first, string last)
			:base(number, dateOpen, first, last)
		{
			this.fee = defaultFee;
		}
		public ChequingAccount(int number, DateTime dateOpen, string first, string last, decimal fee)
			:base(number, dateOpen, first, last)
		{
			this.Fee = fee;
		}

		public decimal Fee
		{
			get
			{
				return this.fee;
			}
			set
			{
				this.fee = (value >= 0M) ? value : defaultFee;
			}
		}
		
		public static decimal DefaultFee
		{
			get
			{
				return defaultFee;
			}
			set
			{
				defaultFee = (value > 0M) ? value : defaultFee;
			}
		}

		public decimal ApplyFee()
		{
			base.balance -= this.fee;
			return this.balance;
		}

		public override string ToString()
		{
			return base.ToString() + String.Format("\n\tFee: {0:C}",this.fee);
		}

		public override decimal WithDraw(decimal amount)
		{
			if(amount > 0M && base.balance >= amount)
			{
				base.balance -= amount;
				return amount;
			}
			else
				return 0M;
		}
	}
}
