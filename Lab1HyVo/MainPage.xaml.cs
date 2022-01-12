//Name: Hy Vo
//Done for: PROG1224 Lab 1 - Random/Loterry Number Generator =))

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

namespace Lab1HyVo
{   
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        int[] playerNumbers = new int[7];   //first array for player numbers
        int[] winningNumbers = new int[6];  //second array for winning numbers

        private void GenerateRandomNumbers(int[] numberArray)
        {
            Random random = new Random();
            int temp;
            for (int i = 0; i < numberArray.Length; i++)
            {
                do //prevent number repetition
                {
                    temp = random.Next(1, 50);
                } while (numberArray.Contains(temp));
                numberArray[i] = temp;
            }
        }

        private string BuildDisplayString(int[] numberArray)
        {
            string displayNum = "";
            for (int i = 0; i < numberArray.Length; i++)
            {   //add a number to a string variable with forced 2 digits coutomes and a space in between
                displayNum += numberArray[i].ToString("00") + " ";
            }
            return displayNum;
        }

        private int CompareTwoArrays(int[] array1, int[] array2)
        {

            int firstArr = 0;
            int similarNum = 0;
            for (int i = 0; i < array1.Length; i++)
            {
                firstArr = array1[i];   //store the value of the first array into an int
                for (int w = 0; w < array2.Length; w++)
                {
                    if (firstArr == array2[w])  //then compare the variable to all values in the second array
                    {
                        similarNum += 1;    //plus 1 match if similiarities between two arrays are found
                    }
                }
            }
            return similarNum;
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            GenerateRandomNumbers(playerNumbers);   //input player array into random generate method
            txtPlayerNum.Text = BuildDisplayString(playerNumbers); //output results from string number building method
        }

        private void btnGenerateWins_Click(object sender, RoutedEventArgs e)
        {
            GenerateRandomNumbers(winningNumbers);  //input winning array into random generate method
            txtWinningNum.Text = BuildDisplayString(winningNumbers);    //output built results
            txtNumOfMatches.Text = CompareTwoArrays(playerNumbers, winningNumbers).ToString("00"); //output matches
        }
    } 
}
