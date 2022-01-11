using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    //Part A: Write the City class

    class City
    {
        //city information private fields
        private string cityName;
        private int yearCreated;
        private int cityPopulation;
        private bool hasSubway;
        private string countryName;

        //public properties
        public int Year
        {
            get { return yearCreated; }
            set { yearCreated = value; }
        }

        public int Population
        {
            get { return cityPopulation; }
            set { cityPopulation = value; }
        }

        public bool HasSubway
        {
            get { return hasSubway; }
            set { hasSubway = value; }
        }

        //constructor
        public City()
        {
            cityName = "Unknown";
            yearCreated = 0;
            cityPopulation = 0;
            hasSubway = false;
            countryName = "Unknown";
        }

        //overloaded constructor
        public City(string city, int year, int pops, bool subway, string country)
        {
            cityName = city;
            yearCreated = year;
            cityPopulation = pops;
            hasSubway = subway;
            countryName = country;
        }

        //methods
        public override string ToString() //display the city and the country it belongs to
        {
            return $"{cityName} of {countryName}";
        }

        public string Classification() //this method place a category on a city based on its population number
        {
            if (cityPopulation < 30000)
            {
                return "[Rural] ";
            }
            else if (cityPopulation >= 30000 && cityPopulation < 100000)
            {
                return "[Town] ";
            }
            else
            {
                return "[City] ";
            }
        }
        
    }
}
