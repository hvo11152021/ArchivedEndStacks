//Object Oriented Programming
//P Vanscoy
//SuperChequingAccount

using System;

namespace BankOfVanscoy
{
	public sealed class SuperChequingAccount : ChequingAccount
	{
		private decimal overDraft;

		public SuperChequingAccount(int number, DateTime dateOpen)
			:base(number, dateOpen)
		{
			this.overDraft = 0M;
		}
		public SuperChequingAccount(int number, DateTime dateOpen, string first, string last)
			:base(number, dateOpen, first, last)
		{
			this.overDraft = 0M;
		}
		public SuperChequingAccount(int number, DateTime dateOpen, string first, string last, decimal fee)
			:base(number, dateOpen, first, last, fee)
		{
			this.overDraft = 0M;
		}
		public SuperChequingAccount(int number, DateTime dateOpen, string first, string last, decimal fee, decimal overDraft)
			:base(number, dateOpen, first, last, fee)
		{
			this.OverDraft = overDraft;
		}

		public decimal OverDraft
		{
			get
			{
				return this.overDraft;
			}
			set
			{
				this.overDraft = (value >= 0M) ? value : 0M;
			}
		}

		public override string ToString()
		{
			return base.ToString() + String.Format("\n\tDraft: {0:C}",this.overDraft);
		}

		public override decimal WithDraw(decimal amount)
		{
			if(amount > 0M && (base.balance + this.overDraft) >= amount)
			{
				base.balance -= amount;
				return amount;
			}
			else
				return 0M;
		}
	}
}
