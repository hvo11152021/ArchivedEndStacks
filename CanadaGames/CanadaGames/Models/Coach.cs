using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CanadaGames.Models
{
    public class Coach
    {
        public Coach()
        {
            this.Athletes = new HashSet<Athlete>();
        }

        public string FullName
        {
            get
            {
                return FirstName + (string.IsNullOrEmpty(MiddleName) ? " " : (" " + (char?)MiddleName[0] + ". ").ToUpper()) + LastName;
            }
        }

        public int ID { get; set; }

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

        public ICollection<Athlete> Athletes { get; set; }
    }
}
