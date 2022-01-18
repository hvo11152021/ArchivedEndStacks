using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CanadaGames.Models
{
    public class Athlete : IValidatableObject
    {
        public int ID { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + (string.IsNullOrEmpty(MiddleName) ? " " : (" " + (char?)MiddleName[0] + ". ").ToUpper()) + LastName;
            }
        }

        public string Age
        {
            get
            {
                DateTime current = DateTime.Today;
                int age = current.Year - DOB.Year - ((current.Month < DOB.Month || (current.Month == DOB.Month && current.Day < DOB.Day) ? 1 : 0));
                return age.ToString();
            }
        }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "Max 50 characters only.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Max 50 characters only.")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "Max 100 characters only.")]
        public string LastName { get; set; }

        [Display(Name = "Athlete Code")]
        [Required(ErrorMessage = "Athlete code is required.")]
        [RegularExpression("^\\d{7}$", ErrorMessage = "The code must be exactly 7 digits.")]
        [StringLength(7)]
        public string AthleteCode { get; set; }

        [Display(Name = "Home Town")]
        [Required(ErrorMessage = "Home town is required.")]
        [StringLength(100, ErrorMessage = "Max 100 characters only.")]
        public string Hometown { get; set; }

        [Display(Name = "DOB")]
        [Required(ErrorMessage = "Date of birth is required.")]
        //date range validation
        [CustomDateRange(ErrorMessage = "Value for {0} must be between {1:d} and {2:d}.")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        [Display(Name = "Height (cm)")]
        [Required(ErrorMessage = "Height is required.")]
        [Range(64, 245, ErrorMessage = "Please enter an amount between 100 cm and 250 cm.")]
        public int Height { get; set; }

        [Display(Name = "Weight (kg)")]
        [Required(ErrorMessage = "Weight is required.")]
        [Range(18, 180, ErrorMessage = "Please enter an amount between 0 kg and 200 kg.")]
        public double Weight { get; set; }

        [Display(Name = "Years In Sport")]
        [Required(ErrorMessage = "Weight is required.")]
        public int YearsInSport { get; set; }

        [Display(Name = "Affiliation")]
        [Required(ErrorMessage = "An affiliation is required.")]
        [StringLength(255, ErrorMessage = "Max 255 characters only.")]
        public string Affiliation { get; set; }

        [Display(Name = "Goals")]
        [Required(ErrorMessage = "Home town is required.")]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "Minimum of 10 to maximum of 255 characters only.")]
        public string Goals { get; set; }

        [Display(Name = "Position")]
        [StringLength(50, ErrorMessage = "Max 50 characters only.")]
        public string? Position { get; set; }

        [Display(Name = "Role Model")]
        [StringLength(100, ErrorMessage = "Max 100 characters only.")]
        public string? RoleModel { get; set; }

        [Display(Name = "Medal Information")]
        [Required(ErrorMessage = "Medal information is required.")]
        [StringLength(2000, ErrorMessage = "Max 2000 characters only.")]
        public string MedalInfo { get; set; }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        //Foreign Keys
        [Display(Name = "Contingent")]
        [Required(ErrorMessage = "ID is required.")]
        public int ContingentID { get; set; }
        public Contingent Contingent { get; set; }

        [Display(Name = "Sport")]
        [Required(ErrorMessage = "A sport is required.")]
        public int SportID { get; set; }
        public Sport Sport { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "A gender is required.")]
        public int GenderID { get; set; }
        public Gender Gender { get; set; }

        [Display(Name = "Coach")]
        [Required(ErrorMessage = "A coach is required.")]
        public int? CoachID { get; set; }
        public Coach Coach { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            double BMI = Weight / Math.Pow(Height / 100.00, 2);
            if (BMI < 15)
            {
                yield return new ValidationResult("You're underweight!", new[] { "Weight" });
            }
            else if (BMI > 40)
            {
                yield return new ValidationResult("You're very severely obese!", new[] { "Weight" });
            }
        }
    }
}
