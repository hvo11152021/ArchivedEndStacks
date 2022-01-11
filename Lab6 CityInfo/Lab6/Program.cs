using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* City Data Program
 * PROG 1124 Lab 6, Winter 2021
 * Created By: Melissa VanderLely /mv
 * Modified By: <<Hy Gia Vo>>
 */

namespace Lab6
{
    class Program
    {
        public const int NumberOfCities = 5;   //You can modify this to set the size of your array  /mv
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the City Data Program.\nBegin Data Input Below.\n");

            //Part B: Entering Information 
            City[] cityList = new City[NumberOfCities]; //cityList array declaration

            try
            {
                for (int i = 0; i < NumberOfCities; i++)
                {
                    //prompt for the four pieces of information required to create one City  /mv
                    Console.WriteLine($"\nEnter values for City #{i + 1}\n");

                    Console.Write("City Name: ");
                    string inputName = Console.ReadLine();

                    Console.Write("Year Incorporated: ");
                    string inputYear = Console.ReadLine();

                    Console.Write("Population: ");
                    string inputPopulation = Console.ReadLine();

                    Console.Write("Has Subway (Y/N): ");
                    string inputHasSubway = Console.ReadLine();

                    Console.Write("Country: ");
                    string inputCountry = Console.ReadLine();

                    //TODO:  Perform validation and data type conversions on these input values, as needed
                    if (String.IsNullOrEmpty(inputYear) || !int.TryParse(inputYear, out int y))
                    {    //validations for year and population input before conversions, checking for empty or non-numeric inputs
                        throw new Exception("Please enter a valid year.");
                    }
                    else if (String.IsNullOrEmpty(inputPopulation) || !int.TryParse(inputPopulation, out int p))
                    {
                        throw new Exception("Please enter a population figure.");
                    }

                    int yearIncorporated = Int32.Parse(inputYear);  //convert string input into an integer
                    int cityPopulation = Int32.Parse(inputPopulation);
                    bool containSubway = false; //set a new boolean for conversion
                    if (inputHasSubway.ToLower() == "yes" || inputHasSubway.ToLower() == "y")
                    {   //convert inputs like "yes" or "no" to true or false. without this, the user have to type "true" or "false" for the app to register
                        containSubway = true;
                    }
                    else if (inputHasSubway.ToLower() == "no" || inputHasSubway.ToLower() == "n")
                    {
                        containSubway = false;
                    }

                    //TODO:  If the data input is invalid, throw an exception with an error message
                    if (String.IsNullOrEmpty(inputName))
                    {   //throw exceptions when there's no input
                        throw new Exception("Please enter the name of a city.");
                    }
                    else if (String.IsNullOrEmpty(inputCountry))
                    {
                        throw new Exception("Please enter the name of a country.");
                    }

                    //TODO:  Create the City object based on the input values, and then add that city to your Array of Cities.
                    cityList[i] = new City(inputName, yearIncorporated, cityPopulation, containSubway, inputCountry);
                    //add the object of input values to array "cityList"

                }
            }
            catch (Exception ex)           //data input failed - exit gracefully with an error message     /mv
            {
                Console.WriteLine("\nThere was a problem with your data input.  Please try again later.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                return;
            }
            
            //Pause for user feedback before displaying the menu.   /mv
            Console.WriteLine("\nData Input Completed. Thank you!");
            Console.WriteLine("Press any key to display the menu.");
            Console.ReadKey();

            Console.Clear();

            //Part B: Interacting with Data

            //this loop forms the menu.  It repeats until the user enters the value "5" to exit.    /mv
            string menuSelection = "";
            do
            {
                Console.WriteLine("Choose a menu option from the following:");

                Console.WriteLine("  1.  Display cities and countries");
                Console.WriteLine("  2.  Average population of all cities");
                Console.WriteLine("  3.  Display cities from the 1800s");
                Console.WriteLine("  4.  Display cities with a subway system");
                Console.WriteLine("  5.  Exit Program");

                Console.Write("\nEnter your selection: ");
                menuSelection = Console.ReadLine();

                switch (menuSelection)
                {
                    case "1":
                        //TODO: perform actions for menu option 1
                        Console.WriteLine("The list of cities and countries: ");    //display 
                        foreach (City cty in cityList)
                        {   //display cities and countries of each elements in the array
                            Console.WriteLine(cty.Classification() + cty);
                        }
                        break;
                    case "2":
                        //TODO: perform actions for menu option 2
                        Console.WriteLine("The average population of all cities is: ");
                        int numPops = 0;
                        foreach (City cty in cityList)
                        {   //calculating the average population
                            numPops += cty.Population;  //add up the population of all cities
                        }
                        decimal avePops = numPops / NumberOfCities; //then divide by the number of cities
                        Console.WriteLine($"{avePops} people");
                        break;
                    case "3":
                        //TODO: perform actions for menu option 3
                        Console.WriteLine("Cities from the 1800s are: ");
                        foreach (City cty in cityList)
                        {   //calculate which cities were from the 1800s
                            if (cty.Year >= 1800 || cty.Year < 1900)
                            {   //display cities during the 19th century
                                Console.WriteLine(cty.Classification() + cty);
                            }
                        }
                        break;
                    case "4":
                        //TODO: perform actions for menu option 4
                        Console.WriteLine("Cities with a subway system are: ");
                        foreach (City cty in cityList)
                        {   //caculate which city have a subway system
                            if (cty.HasSubway == true)
                            {   //display cities with "hasSubway" result of true
                                Console.WriteLine(cty.Classification() + cty);
                            }
                        }
                        break;
                    case "5":
                        Console.WriteLine("Program terminated.  Goodbye!");
                        break;
                    default:
                        Console.WriteLine("That selection is not valid.\n");
                        break;
                }

            }
            while (menuSelection != "5");
            
        }
        
    }
}
