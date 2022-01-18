using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CanadaGames.Models
{
    public class Contingent
    {
        public Contingent()
        {
            this.Athletes = new HashSet<Athlete>();
        }
        public int ID { get; set; }

        [Display(Name = "Contingent Code")]
        [Required(ErrorMessage = "A code is required.")]
        [StringLength(1, ErrorMessage = "Max 2 characters only.")]
        public string Code { get; set; }

        [Display(Name = "Contingent Name")]
        [Required(ErrorMessage = "A name is required.")]
        [StringLength(10, ErrorMessage = "Max 50 characters only.")]
        public string Name { get; set; }

        public ICollection<Athlete> Athletes { get; set; }
    }
}
