//Object Oriented Programming
//P Vanscoy
//Base class

using System;

namespace BankOfVanscoy
{
	/// <summary>
	/// Name: Hy Vo
	/// Project: Lab 4 Part A
	/// </summary>

	public abstract class BankAccount : Object
	{
		public delegate void AlertHandler(object sender, EventArgs args);  //delegate
		public event AlertHandler HighInput; //event

		private readonly int number;		
		private string first;				
		private string last;				
		protected decimal balance;			
		
		private readonly DateTime dateOpen;	
		
		private static int count = 0;		
		
		private const string BANKNAME = "Bank Of Vanscoy";

		public BankAccount(int number, DateTime dateOpen)
		{
			setAccount();
			this.number = (number > 0) ? number : 0;
			this.dateOpen = (dateOpen <= DateTime.Today) ? dateOpen : DateTime.Today;
		}

        public BankAccount(int number, DateTime dateOpen, string first, string last)
		{
			setAccount();
			this.number = (number > 0) ? number : 0;
			this.dateOpen = (dateOpen <= DateTime.Today) ? dateOpen : DateTime.Today;
			this.First = first;
			this.Last = last;
		}

        private void setAccount()
		{
			this.first = "Unknown";
			this.last = "Unknown";
			this.balance = 0M;
			count++;
		}

		public string First
		{
			get
			{
				return this.first;
			}
			set
			{
				string temp = value;
				this.first = (temp.Length > 0) ? temp : this.first;
			}
		}
		public string Last
		{
			get
			{
				return this.last;
			}
			set
			{
				string temp = value;
				this.last = (temp.Length > 0) ? temp : this.last;
			}
		}

		public int Number
		{
			get
			{
				return this.number;
			}
		}
		public string DateOpen 
		{
			get
			{
				return this.dateOpen.ToLongDateString();
			}
		}
		public string Balance
		{
			get
			{
				return this.balance.ToString("c");
			}
		}

		public static string BankName
		{
			get
			{
				return BANKNAME;
			}
		}
		public static int Count
		{
			get
			{
				return count;
			}
		}

		public decimal Deposit(decimal amount)
		{
			if (amount >= 10000M)
            {
				HighInput?.Invoke(this, EventArgs.Empty); //alert for any deposit above $10,000
				this.balance += amount;
				return this.balance;
			}
			else if (amount > 0M)
			{
				this.balance += amount;
				return this.balance;
			}
			else
				return 0M;
		}

		public override string ToString()
		{
			return String.Format("\n\tAccount: {0}\n\tName: {1}\n\tBalance: {2:C}\n\tOpened: {3}\n",
				this.Number,this.First + " " + this.Last,this.Balance,this.DateOpen);
		}

		public override bool Equals(object obj)
		{
			BankAccount temp = (BankAccount) obj;
			return this.number==temp.number;
			
		}

		public abstract decimal WithDraw(decimal amount);

	}
}
