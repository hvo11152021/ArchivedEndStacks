//Hy Vo Gia - Pizza Ordering Program - Lab 1

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
using Windows.UI.Popups;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Pizza_Ordering_UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //An event handler for text blocks.
        }

        private void TextBlock_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            //An event handler for text blocks.
        }

        private void nameTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //An event handler for text boxes.
        }

        private void sizeBox1_Click(object sender, RoutedEventArgs e)
        {
            if (sizeBox1.IsChecked == true)     //Checking whether the checkbox for pizza type is checked.
            {                                   //If it's checked, $8 is charged. The process is reverse if the box is unchecked.
                price += 8;
            }
            else
            {
                price -= 8;
            }
        }

        private void sizeBox2_Click(object sender, RoutedEventArgs e)
        {
            if (sizeBox2.IsChecked == true)     //Checking whether the checkbox for pizza type is checked.
            {                                   //If it's checked, $12 is charged. The process is reverse if the box is unchecked.
                price += 12;
            }
            else
            {
                price -= 12;
            }
        }

        double price = 0; //A variable to store potential decimals.

        private void checkBox1_Click(object sender, RoutedEventArgs e)
        { 
            if (checkBox1.IsChecked == true)    //Checking whether the checkbox for pizza toppings are checked.
            {                                   //If they're checked, $1 is charged. The process is reverse if the box is unchecked.
                price += 1;                     //This occurs with all pizza topping choices.
            }
            else
            {
                price -= 1;
            }
        }

        private void checkBox2_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox2.IsChecked == true)
            {
                price += 1;
            }
            else
            {
                price -= 1;
            }
        }

        private void checkBox3_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox3.IsChecked == true)
            {
                price += 1;
            }
            else
            {
                price -= 1;
            }
        }

        private void checkBox4_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox4.IsChecked == true)
            {
                price += 1;
            }
            else
            {
                price -= 1;
            }
        }

        private void checkBox5_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox5.IsChecked == true)
            {
                price += 1;
            }
            else
            {
                price -= 1;
            }
        }

        private void checkBox6_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox6.IsChecked == true)
            {
                price += 1;
            }
            else
            {
                price -= 1;
            }
        }

        private void checkBox7_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox7.IsChecked == true)
            {
                price += 1;
            }
            else
            {
                price -= 1;
            }
        }

        private void checkBox8_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox8.IsChecked == true)
            {
                price += 1;
            }
            else
            {
                price -= 1;
            }
        }

        private void drinkBox1_Click(object sender, RoutedEventArgs e)
        {
            if (drinkBox1.IsChecked == true)    //Checking whether the checkbox for beverages' choices are checked.
            {                                   //If they're checked, $2 is charged. The process is reverse if the box is unchecked.
                price += 2;                     //This occurs with all pizza topping choices.
            }
            else
            {
                price -= 2;
            }
        }

        private void drinkBox2_Click(object sender, RoutedEventArgs e)
        {
            if (drinkBox2.IsChecked == true)
            {
                price += 2;
            }
            else
            {
                price -= 2;
            }
        }

        private void drinkBox3_Click(object sender, RoutedEventArgs e)
        {
            if (drinkBox3.IsChecked == true)
            {
                price += 2;
            }
            else
            {
                price -= 2;
            }
        }

        private void btnClearOrder_Click(object sender, RoutedEventArgs e)
        {
            if (sizeBox1.IsChecked == true)     //When the customer makes mistakes or starting a fresh order, pressing "Clear Order" button will uncheck all checkboxes and clear data in text boxes and the receipt below.
            {
                sizeBox1.IsChecked = false;
            }
            if (sizeBox2.IsChecked == true)     //If a checkbox was checked, ie true, it will be unchecked, ie false.
            {
                sizeBox2.IsChecked = false;
            }

            if (checkBox1.IsChecked == true)
            {
                checkBox1.IsChecked = false;
            }
            if (checkBox2.IsChecked == true)
            {
                checkBox2.IsChecked = false;
            }
            if (checkBox3.IsChecked == true)
            {
                checkBox3.IsChecked = false;
            }
            if (checkBox4.IsChecked == true)
            {
                checkBox4.IsChecked = false;
            }
            if (checkBox5.IsChecked == true)
            {
                checkBox5.IsChecked = false;
            }
            if (checkBox6.IsChecked == true)
            {
                checkBox6.IsChecked = false;
            }
            if (checkBox7.IsChecked == true)
            {
                checkBox7.IsChecked = false;
            }
            if (checkBox8.IsChecked == true)
            {
                checkBox8.IsChecked = false;
            }

            if (drinkBox1.IsChecked == true)
            {
                drinkBox1.IsChecked = false;
            }
            if (drinkBox2.IsChecked == true)
            {
                drinkBox2.IsChecked = false;
            }
            if (drinkBox3.IsChecked == true)
            {
                drinkBox3.IsChecked = false;
            }

            nameTxtBox.Text = String.Empty;     //Clear the content from the variable.
            addressTxtBox.Text = String.Empty;

            completionMsg.Text = String.Empty;
            nameMsg.Text = String.Empty;
            addressMsg.Text = String.Empty;

            pizzaSizes.Text = String.Empty;
            pizzaToppings.Text = String.Empty;
            drinks.Text = String.Empty;

            totalFees.Text = String.Empty;
            thankingMsg.Text = String.Empty;

            btnConfirm_Order.IsEnabled = true;      //Enable "Confirm_Order" button, because it was disable for the reason listed at the very bottom.
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            completionMsg.Text = "Your order has completed.";   //When the customer press "Confirm Order", the program will takes in informations to produce a receipt.
            nameMsg.Text = "Your name: " + nameTxtBox.Text;     //Display name of the customer at final checkout.
            addressMsg.Text = "Your address: " + addressTxtBox.Text;    //Display address of the customer at final checkout.

            pizzaSizes.Text = "Your Selected Size Are: ";   //Display the selected size of pizza at final checkout.

            string sizes = string.Empty;
            CheckBox[] sizeList = new CheckBox[]    //New string to hold checkboxes.
            {
                sizeBox1, sizeBox2
            };
            foreach (var choose in sizeList)    //A new variable to hold information from the string.
            {
                if (choose.IsChecked == true)   //If a checkbox is checked, then display the information of that specific checkbox.
                {
                    sizes += choose.Content + ", ";
                }
            }
            pizzaSizes.Text += sizes;

            pizzaToppings.Text = "Your Selected Toppings Are: ";

            string toppings = string.Empty;
            CheckBox[] toppingsList = new CheckBox[]
            {
                checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7, checkBox8
            };
            foreach (var choose in toppingsList)
            {
                if (choose.IsChecked == true)
                {
                    toppings += choose.Content + ", ";
                }
            }
            pizzaToppings.Text += toppings;

            drinks.Text = "Your Selected Beverages Are: ";
            string beverages = string.Empty;
            CheckBox[] drinkboxes = new CheckBox[]
            {
                drinkBox1, drinkBox2, drinkBox3
            };
            foreach (var choose in drinkboxes)
            {
                if (choose.IsChecked == true)
                {
                    beverages += choose.Content + ", ";
                }
            }
            drinks.Text += beverages;

            totalFees.Text = "Your final cost: " + price; //Showing the final cost of all choices from the customer.

            thankingMsg.Text = "Thanks for ordering at Pizza Express. Your ordered items will be ready in 30 minutes.";

            btnConfirm_Order.IsEnabled = false;     //The "Confirm Order" button is disable after use so the customer won't accidentally click on this button and repeat the process the second time.
        }
    }
}
