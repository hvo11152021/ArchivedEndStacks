//Gia Hy Vo - Lab 4 Fitness App

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

namespace Fitness_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool typeBMI, typeMHR, typeBMR, maleOption, femaleOption = false;

        public MainPage()
        {
            this.InitializeComponent();
            txtAge.IsEnabled = false;       //disable all textboxes and radiobuttons before the user choose a calculation
            radMale.IsEnabled = false;
            radFemale.IsEnabled = false;
            txtWeight.IsEnabled = false;
            txtHeight.IsEnabled = false;
        }
        
        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string type = e.AddedItems[0].ToString();       //add and set values from comboboxes to a string
            switch (type)
            {
                //this limit available input
                case "Body Mass Index":     //only weight and height inputs are allowed
                    typeBMI = true;
                    typeMHR = false;        //ensure that the program don't perform calculations for max heart rate
                    typeBMR = false;        //ensure that the program don't perform calculations for basal metabolic rate
                    txtAge.IsEnabled = false;       //textbox to input age is disable because BMI doesn't need age
                    radMale.IsEnabled = false;      //gender option is disable
                    radFemale.IsEnabled = false;
                    txtWeight.IsEnabled = true;     //weight and height values are allowed
                    txtHeight.IsEnabled = true;
                    break;
                case "Maximum Heart Rate": //only age input is allowed for MHR
                    typeBMI = false;
                    typeMHR = true;
                    typeBMR = false;
                    txtAge.IsEnabled = true;
                    radMale.IsEnabled = false;
                    radFemale.IsEnabled = false;
                    txtWeight.IsEnabled = false;
                    txtHeight.IsEnabled = false;
                    break;
                case "Basal Matabolic Rate": //all inputs are allowed for BMR
                    typeBMI = false;
                    typeMHR = false;
                    typeBMR = true;
                    txtAge.IsEnabled = true;
                    radMale.IsEnabled = true;
                    radFemale.IsEnabled = true;
                    txtWeight.IsEnabled = true;
                    txtHeight.IsEnabled = true;
                    break;
            }
        }

        void radMale_Click(object sender, RoutedEventArgs e)        
        {
            maleOption = true;      //set option for gender male true and false for female
            femaleOption = false;
        }

        void radFemale_Click(object sender, RoutedEventArgs e)      
        {
            maleOption = false;     //set option for gender female true and false for male
            femaleOption = true;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)       
        {
            txtAnswer.Text = String.Empty;      //clear text in answer textbox and enable calculate button
            btnCalculate.IsEnabled = true;
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            int userAge = int.Parse(txtAge.Text);       //a variable for age input
            double userWeight = double.Parse(txtWeight.Text);       //a variable for weight input
            double userHeight = double.Parse(txtHeight.Text);       //a variable for height input

            if (typeBMI)
            {
                //display a message when there's no input
                if (txtWeight.Text == "0" || txtHeight.Text == "0")
                {       
                    txtAnswer.Text += "Please enter a value for weight or height";  
                }
                //display the result from BMI method
                else
                {
                    txtAnswer.Text += "Your Body Mass Index is " + BMI(userWeight, userHeight);
                }
            }
            else if (typeMHR)
            {
                //display a message when the user input invalid age
                if (userAge < 2 || userAge > 120)
                {
                    txtAnswer.Text += "Invalid Age";
                }
                else
                //display the result from MHR method
                {
                    txtAnswer.Text += "Your Maximum Heart Rate is " + MHR(userAge);
                }
            }
            else if (typeBMR)
            {
                //display a message when the user input invalid age
                if (userAge < 2 || userAge > 120)
                {
                    txtAnswer.Text += "Invalid Age";
                }
                //display a message when there's no input
                else if (txtWeight.Text == "0" || txtHeight.Text == "0")
                {
                    txtAnswer.Text += "Please enter a value for weight or height";
                }
                //display the result from maleBMR or femaleBMR method based on the gender chosen
                else
                {
                    if (maleOption)
                    {
                        txtAnswer.Text += "Your Basal Metabolic Rate is " + maleBMR(userAge, userWeight, userHeight);
                    }
                    else if (femaleOption)
                    {
                        txtAnswer.Text += "Your Basal Metabolic Rate is " + femaleBMR(userAge, userWeight, userHeight);
                    }
                    else
                    {
                        txtAnswer.Text += "No gender selected";
                    }
                }
            }
            btnCalculate.IsEnabled = false;     //disable calculate button to avoid result duplication
        }

        //calculate body mass index
        double BMI(double weight, double height) => Math.Round(weight / (height / 100 * (height / 100)), 2);  //round up the number to 2 decimal points
        //calculate max heart rate
        double MHR(int age) => Math.Round(208 - (0.7 * age), 2);
        //calculate basal metabolic rate for men
        double maleBMR(int age, double weight, double height) => Math.Round(66.47 + (13.75 * weight) + (5 * height) - (6.75 * age), 2);
        //calculate basal metabolic rate for women
        double femaleBMR(int age, double weight, double height) => Math.Round(665.09 + (9.56 * weight) + (1.84 * height) - (4.67 * age), 2);

    }
}
