using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CanadaGames.Models
{
    public class Hometown
    {
        public int ID { get; set; }

        [Display(Name = "Hometown Name")]
        [Required(ErrorMessage = "Hometown name is required")]
        [StringLength(255, ErrorMessage = "Hometown name cannot be more than 255 characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select a province")]
        [Display(Name = "Contingent")]
        public int ContingentID { get; set; }
        public Contingent Contingent { get; set; }

        [Display(Name = "Hometown")]
        [DisplayFormat(NullDisplayText = "No Hometown Specified")]
        public string CityState
        {
            get
            {
                return Name + ", " + Contingent?.Code;
            }
        }

        public ICollection<Athlete> Athletes { get; set; }

        public Hometown()
        {
            Athletes = new HashSet<Athlete>();
        }
    }
}
