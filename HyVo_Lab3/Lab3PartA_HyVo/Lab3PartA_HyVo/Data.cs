using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3PartA_HyVo
{
    /// <summary>
    /// Name: Hy Vo
    /// Project: Lab 3A
    /// </summary>
    public static class Data
    {
        private static List<Employee> employee;

        static Data()
        {
            employee = new List<Employee>();
        }

        public static List<Employee> GetDataRecords()
        {
            employee.Add(new Hourly("977993106", "Keelan", "North", DateTime.Parse("2021-08-05"), DateTime.Parse("1980-12-13"), "6159985792", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 40, 20));
            employee.Add(new Hourly("665370318", "Eman", "England", DateTime.Parse("2021-08-16"), DateTime.Parse("1992-02-10"), "6971368629", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 40, 20));
            employee.Add(new Hourly("344115475", "Mikaela", "Lucero", DateTime.Parse("2021-09-08"), DateTime.Parse("1991-10-23"), "8596726839", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 40, 20));
            employee.Add(new Hourly("171914661", "Mcauley", "Marshall", DateTime.Parse("2021-09-15"), DateTime.Parse("1986-07-26"), "5849022271", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 40, 20));
            employee.Add(new Hourly("876962245", "Izabelle", "Lacey", DateTime.Parse("2021-10-13"), DateTime.Parse("1994-07-27"), "5001385952", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 40, 20));
            employee.Add(new Hourly("840921708", "Zakk", "Findlay", DateTime.Parse("2021-09-28"), DateTime.Parse("1983-01-04"), "5125405905", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 40, 20));

            employee.Add(new Supervisor("434310459", "Aviana", "Cantu", DateTime.Parse("2021-10-08"), DateTime.Parse("1986-06-24"), "3143762442", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 60420));
            employee.Add(new Supervisor("587537913", "Coen", "Romero", DateTime.Parse("2021-11-02"), DateTime.Parse("1994-03-09"), "6493356864", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 60420));
            employee.Add(new Supervisor("866081710", "Piers", "Mays", DateTime.Parse("2021-11-03"), DateTime.Parse("1997-01-14"), "5234503935", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 60420));
            employee.Add(new Supervisor("893582684", "Karl", "Devine", DateTime.Parse("2021-11-17"), DateTime.Parse("1984-11-17"), "8603575378", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 60420));
            employee.Add(new Supervisor("839007338", "Miyah", "Seymour", DateTime.Parse("2021-08-19"), DateTime.Parse("1995-05-28"), "5130342122", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 60420));
            employee.Add(new Supervisor("184137137", "Mina", "Arroyo", DateTime.Parse("2021-08-25"), DateTime.Parse("1985-09-13"), "1430113790", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 60420));

            employee.Add(new EngineeringManager("623771224", "Elizabeth", "Hicks", DateTime.Parse("2021-10-27"), DateTime.Parse("1978-05-09"), "8157972284", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 91800, 7650));
            employee.Add(new EngineeringManager("674581538", "Kara", "Fellows", DateTime.Parse("2021-11-22"), DateTime.Parse("1982-01-14"), "9012636916", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 91800, 7650));
            employee.Add(new EngineeringManager("630457538", "Keri", "Hays", DateTime.Parse("2021-12-10"), DateTime.Parse("1989-12-13"), "6617713206", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 91800, 7650));
            employee.Add(new EngineeringManager("214661918", "Evelina", "Estes", DateTime.Parse("2021-08-25"), DateTime.Parse("1981-11-12"), "3184614912", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 91800, 7650));
            employee.Add(new EngineeringManager("511168618", "Stephan", "Blackwell", DateTime.Parse("2021-09-09"), DateTime.Parse("1978-01-23"), "8748555514", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 91800, 7650));
            employee.Add(new EngineeringManager("786174145", "Lewys", "Griffith", DateTime.Parse("2021-09-28"), DateTime.Parse("1983-01-31"), "4451844420", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 91800, 7650));

            employee.Add(new GlobalSupplyManager("384874482", "Maximilian", "Sadler", DateTime.Parse("2021-10-18"), DateTime.Parse("1971-11-25"), "2913658154", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 110000));
            employee.Add(new GlobalSupplyManager("258004520", "Huw", "Coombes", DateTime.Parse("2021 -10-25"), DateTime.Parse("1995-08-25"), "7740739086", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 110000));
            employee.Add(new GlobalSupplyManager("417418582", "Essa", "Blackburn", DateTime.Parse("2021-08-02"), DateTime.Parse("1981-06-18"), "1814375916", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 110000));
            employee.Add(new GlobalSupplyManager("658645062", "Marnie", "Farmer", DateTime.Parse("2021-08-12"), DateTime.Parse("1972-03-12"), "1876335241", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 110000));
            employee.Add(new GlobalSupplyManager("906483746", "Rudi", "Barr", DateTime.Parse("2021-09-22"), DateTime.Parse("1973-03-23"), "4958586997", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 110000));
            employee.Add(new GlobalSupplyManager("151287189", "Emerson", "Travers", DateTime.Parse("2021-10-12"), DateTime.Parse("1975-04-14"), "1131645323", "391 Foster City Blvd, Foster City, CA 94404, United States", "randomperson@gmail.com", 110000));

            return employee;
        }
    }
}
