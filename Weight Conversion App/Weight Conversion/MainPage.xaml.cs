//Hy Vo Gia - Weight Conversion Program - Lab 2

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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Weight_Conversion
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

        bool verify = false; //a boolean variable to verify if the user selected a unit or not
        bool isValueKg; //a boolean variable making sure if the chosen unit to convert is either kg or pounds
        string value; //a variable to store values that the user type in
        double result; //store values of variable "value" after converted from string to float
        

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //an even handler for the textbox
        }

        private void cboUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string unitName = e.AddedItems[0].ToString(); //add values from the combo box to a string called unitName
            switch (unitName) //a switch statement with an expression
            {
                case "Kilogram": //value "Kilogram" is compared with values of the expression
                    verify = true; //if unitName's value match with "Kilogram", then code blocks inside will execute
                    isValueKg = true; //isValueKg is true because the user select kilogram to convert from kg to pound first
                    break; //stop execute order from the case
                case "Pound": //value "Pound" is compared with values of the expression
                    verify = true; //varify is true when a case is executed
                    isValueKg = false; //now isValueKg is false because the user select pound to convert to kg
                    break;
            }
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            double inputValue = 0; //a variable to store values from textbox
            bool isNumber = false; //a variable to determine if user input is solely a number

            isNumber = double.TryParse(txtInput.Text, out inputValue); //try and parse the data in the text box, asking if the data from the textbox can be converted into a double value

            if (txtInput.Text.Length == 0) //if there's no data in the box, the program display text urging the user to enter at least something
            {                                     //this expression prevent the program from crashing when execute
                txtResults.Text = "Please input a number.";
            }
            else if (!isNumber) //isNumber is false by default. if the user type only number, then the program can convert it to a double value, making isNumber true
            {                   //so if isNumber is true, the program will skip this part, but when isNumber is false, the program display text telling the user to input just a number
                txtResults.Text = "Please enter only number.";
            }
            else if (!verify) //return a message telling the user to choose a unit for conversion even if they have input values into the textbox
            {
                txtResults.Text = "Please select a unit.";
            }
            else
            {
                value = txtInput.Text; //store input values in value variable
                result = Convert.ToDouble(value); //convert value's values from string to double
                double bothResults = 0; //store the result of the conversion
                if (isValueKg) //if the user selected kilogram for conversion
                {
                    bothResults = result * 2.205; //convert input values into pounds
                    txtResults.Text = value + " kg = " + bothResults + " lbs."; //display the result
                }
                else //if the user choose another unit instead of kg for conversion
                {
                    bothResults = result / 2.205; //convert input values into kg
                    txtResults.Text = value + " lbs = " + bothResults + " kg.";
                }
            }
        }
    }
}
