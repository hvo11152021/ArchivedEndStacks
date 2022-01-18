using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanadaGames.Models;

namespace CanadaGames.Data
{
    // Name: Hy Vo
    // Project: PROG1322 Part 1
    // Version2: Fixed DOB for athlete 4. Her previous DOB year was 1986, which was out of range.
    public static class CGSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CanadaGamesContext(
                serviceProvider.GetRequiredService<DbContextOptions<CanadaGamesContext>>()))
            {
                if (!context.Contingents.Any())
                {
                    var contingents = new List<Contingent>
                    {
                        new Contingent { Code = "ON", Name = "Ontario"},
                        new Contingent { Code = "PE", Name = "Prince Edward Island"},
                        new Contingent { Code = "NB", Name = "New Brunswick"},
                        new Contingent { Code = "BC", Name = "British Columbia"},
                        new Contingent { Code = "NL", Name = "Newfoundland and Labrador"},
                        new Contingent { Code = "SK", Name = "Saskatchewan"},
                        new Contingent { Code = "NS", Name = "Nova Scotia"},
                        new Contingent { Code = "MB", Name = "Manitoba"},
                        new Contingent { Code = "QC", Name = "Quebec"},
                        new Contingent { Code = "YT", Name = "Yukon"},
                        new Contingent { Code = "NU", Name = "Nunavut"},
                        new Contingent { Code = "NT", Name = "Northwest Territories"},
                        new Contingent { Code = "AB", Name = "Alberta"}
                    };
                    context.Contingents.AddRange(contingents);
                    context.SaveChanges();
                }

                if (!context.Genders.Any())
                {
                    var genders = new List<Gender>
                    {
                        new Gender { Code = "W", Name = "Women"},
                        new Gender { Code = "M", Name = "Men"}
                    };
                    context.Genders.AddRange(genders);
                    context.SaveChanges();
                }

                if (!context.Sports.Any())
                {
                    var sports = new List<Sport>
                    {
                        new Sport { Code = "AL", Name = "Athletics"},
                        new Sport { Code = "RW", Name = "Rowing"},
                        new Sport { Code = "SW", Name = "Swimming"},
                        new Sport { Code = "TN", Name = "Tennis"}
                    };
                    context.Sports.AddRange(sports);
                    context.SaveChanges();
                }

                if (!context.Coaches.Any())
                {
                    var coaches = new List<Coach>
                    {
                        new Coach { FirstName = "Georgie", MiddleName = "Alicia", LastName = "Anne"},
                        new Coach { FirstName = "Deborah", MiddleName = "Lachlan", LastName = "Iqra"},
                        new Coach { FirstName = "Tony", MiddleName = "Eugene", LastName = "Sharpe"},
                        new Coach { FirstName = "Brianna", MiddleName = "Margie", LastName = "Isabel"},
                        new Coach { FirstName = "Missy", MiddleName = "", LastName = "Franklin"}
                    };
                    context.Coaches.AddRange(coaches);
                    context.SaveChanges();
                }

                if (!context.Athletes.Any())
                {
                    var athletes = new List<Athlete>
                    {
                        new Athlete
                        {
                            FirstName = "Penny", 
                            MiddleName = "Sana", 
                            LastName = "Oleksiak", 
                            AthleteCode = "4344346", 
                            Hometown = "Toronto, Ontario", 
                            DOB = DateTime.Parse("2000-6-13"),
                            Height = 186, 
                            Weight = 82.5, 
                            YearsInSport = 10, 
                            Affiliation = "Ontario Regional Swimming Team", 
                            Goals = "GOLD Medals",
                            Position = "",
                            RoleModel = "",
                            MedalInfo = "Olympic Games: 2020 - BRONZE (200m freestyle), 4th (100m freestyle), SILVER (4x100m freestyle relay), BRONZE (4x100m medley relay), 4th (4x200m freestyle);",
                            ContingentID = context.Contingents.FirstOrDefault(d => d.Code == "ON" && d.Name == "Ontario").ID,
                            SportID = context.Sports.FirstOrDefault(d => d.Code == "SW" && d.Name == "Swimming").ID,
                            GenderID = context.Genders.FirstOrDefault(d => d.Code == "W" && d.Name == "Women").ID,
                            CoachID = context.Coaches.FirstOrDefault(d => d.FirstName == "Georgie" && d.LastName == "Anne").ID
                        },
                        new Athlete
                        {
                            FirstName = "Leylah", 
                            MiddleName = "Felicity", 
                            LastName = "Fernandez", 
                            AthleteCode = "3638144", 
                            Hometown = "Laval, Quebec", 
                            DOB = DateTime.Parse("2002-9-6"),
                            Height = 172, 
                            Weight = 69.8, 
                            YearsInSport = 13,
                            Affiliation = "Quebec Regional Tennis Team",
                            Goals = "GOLD Medals",
                            Position = "",
                            RoleModel = "",
                            MedalInfo = "Olympic Games: 2020 - R2 (singles) WTA Titles: 2021 - Monterrey Open(250 series)",
                            ContingentID = context.Contingents.FirstOrDefault(d => d.Code == "QC" && d.Name == "Quebec").ID,
                            SportID = context.Sports.FirstOrDefault(d => d.Code == "TN" && d.Name == "Tennis").ID,
                            GenderID = context.Genders.FirstOrDefault(d => d.Code == "W" && d.Name == "Women").ID,
                            CoachID = context.Coaches.FirstOrDefault(d => d.FirstName == "Deborah" && d.LastName == "Iqra").ID
                        },
                        new Athlete
                        {
                            FirstName = "Andre",
                            MiddleName = "Josiah",
                            LastName = "De Grasse",
                            AthleteCode = "4187894",
                            Hometown = "Ontario",
                            DOB = DateTime.Parse("1994-11-10"),
                            Height = 176,
                            Weight = 70,
                            YearsInSport = 10,
                            Affiliation = "Ontario Regional Athletics Team",
                            Goals = "GOLD Medals",
                            Position = "",
                            RoleModel = "",
                            MedalInfo = "Olympic Games: 2020 - GOLD (200m), BRONZE (100m), BRONZE (4x100m relay);",
                            ContingentID = context.Contingents.FirstOrDefault(d => d.Code == "ON" && d.Name == "Ontario").ID,
                            SportID = context.Sports.FirstOrDefault(d => d.Code == "AL" && d.Name == "Athletics").ID,
                            GenderID = context.Genders.FirstOrDefault(d => d.Code == "M" && d.Name == "Men").ID,
                            CoachID = context.Coaches.FirstOrDefault(d => d.FirstName == "Tony" && d.LastName == "Sharpe").ID
                        },
                        new Athlete
                        {
                            FirstName = "Andrea",
                            MiddleName = "Aysha",
                            LastName = "Proske",
                            AthleteCode = "7284391",
                            Hometown = "Langley, British Columbia",
                            DOB = DateTime.Parse("1996-6-27"),
                            Height = 185,
                            Weight = 82.5,
                            YearsInSport = 10,
                            Affiliation = "BC Regional Rowing Team",
                            Goals = "GOLD Medals",
                            Position = "",
                            RoleModel = "",
                            MedalInfo = "Olympic Games: 2020 - GOLD (100m butterfly), SILVER (4x100m freestyle relay), BRONZE (4x100m medley relay)",
                            ContingentID = context.Contingents.FirstOrDefault(d => d.Code == "BC" && d.Name == "British Columbia").ID,
                            SportID = context.Sports.FirstOrDefault(d => d.Code == "RW" && d.Name == "Rowing").ID,
                            GenderID = context.Genders.FirstOrDefault(d => d.Code == "W" && d.Name == "Women").ID,
                            CoachID = context.Coaches.FirstOrDefault(d => d.FirstName == "Brianna" && d.LastName == "Isabel").ID
                        },
                        new Athlete
                        {
                            FirstName = "Margaret",
                            MiddleName = "Leonie",
                            LastName = "Mac Neil",
                            AthleteCode = "8211964",
                            Hometown = "London, Ontario",
                            DOB = DateTime.Parse("2000-2-26"),
                            Height = 169,
                            Weight = 64.8,
                            YearsInSport = 12,
                            Affiliation = "London Aquatic Club",
                            Goals = "GOLD Medals",
                            Position = "",
                            RoleModel = "",
                            MedalInfo = "Olympic Games: 2020 - GOLD (100m butterfly), SILVER (4x100m freestyle relay), BRONZE (4x100m medley relay)",
                            ContingentID = context.Contingents.FirstOrDefault(d => d.Code == "ON" && d.Name == "Ontario").ID,
                            SportID = context.Sports.FirstOrDefault(d => d.Code == "SW" && d.Name == "Swimming").ID,
                            GenderID = context.Genders.FirstOrDefault(d => d.Code == "W" && d.Name == "Women").ID,
                            CoachID = context.Coaches.FirstOrDefault(d => d.FirstName == "Missy" && d.LastName == "Franklin").ID
                        }
                    };
                    context.Athletes.AddRange(athletes);
                    context.SaveChanges();
                }

            }
        }
    }
}
