using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BankOfVanscoy;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BankManagingApp
{
    /// <summary>
	/// Name: Hy Vo
	/// Project: Lab 4 Part B
	/// </summary>
    public sealed partial class MainPage : Page
    {
        private IDictionary<string, BankAccount> accounts = new Dictionary<string, BankAccount>(); //dictionary collection
        private BankAccount valueList; //class variable

        public MainPage()
        {
            //add bank accounts
            this.InitializeComponent();
            accounts.Add("9549447", new ChequingAccount(9549447, DateTime.Parse("2021-08-03"), "Keelan", "North"));
            accounts.Add("3127464", new ChequingAccount(3127464, DateTime.Parse("2021-08-03"), "Eman", "England"));
            accounts.Add("7796772", new ChequingAccount(7796772, DateTime.Parse("2021-08-03"), "Mikaela", "Lucero"));
            accounts.Add("3302134", new ChequingAccount(3302134, DateTime.Parse("2021-08-03"), "Mcauley", "Marshall"));
            accounts.Add("7663145", new SuperChequingAccount(7663145, DateTime.Parse("2021-08-03"), "Izabelle", "Lacey"));
            accounts.Add("6901820", new SuperChequingAccount(6901820, DateTime.Parse("2021-08-03"), "Zakk", "Findlay"));
            accounts.Add("3963378", new SuperChequingAccount(3963378, DateTime.Parse("2021-08-03"), "Aviana", "Cantu"));
            accounts.Add("4394534", new SavingsAccount(4394534, DateTime.Parse("2021-08-03"), "Coen", "Romero"));
            accounts.Add("9291071", new SavingsAccount(9291071, DateTime.Parse("2021-08-03"), "Piers", "Mays"));
            accounts.Add("1063592", new SavingsAccount(1063592, DateTime.Parse("2021-08-03"), "Hy", "Vo"));

        }

        private void txtAccountNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (accounts.ContainsKey(txtAccountNum.Text))
            {   //enable textbox and deposit button when a valid account is found
                txtError.Text = "Valid account";    
                txtMoneyInput.IsEnabled = true;
                btnDeposit.IsEnabled = true;
            }
            else
            {   //if not, disable both
                txtError.Text = "Account number does not exist";
                txtMoneyInput.IsEnabled = false;
                btnDeposit.IsEnabled = false;
            }
        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            if (accounts.ContainsKey(txtAccountNum.Text))
            {   //set dictionary TValue as a BankAccount object if input account number match TKey
                valueList = accounts[txtAccountNum.Text];
            }
            valueList.HighInput += HandleNotify;    //subscribe to event
            valueList.Deposit(decimal.Parse(txtMoneyInput.Text));   //add money into deposit method
            txtConfirm.Text = $"${txtMoneyInput.Text} was deposited into your account." +
                              $"\nCurrent balance: {valueList.Balance}";   //confirm transaction and update balance
        }

        public void HandleNotify(object sender, EventArgs args)
        {   //display an alert when the customer deposit more than $10k. display fullname, deposit amount and current balance
            var messageDialog = new MessageDialog($"You have successfully deposited ${txtMoneyInput.Text}." +
                "\nAs with any transaction above $10000, your activity will be reported to the IRS." +
                "\nDon't be alarmed however, as this is regular procedure intended to prevent illegal activities." +
                $"\nFull name: {valueList.First} {valueList.Last} " +
                $"\nAmount deposit: ${txtMoneyInput.Text}" +
                $"\nCurrent balance: {valueList.Balance}").ShowAsync();
        }

        private void txtMoneyInput_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter) { }
        }
    }
}
