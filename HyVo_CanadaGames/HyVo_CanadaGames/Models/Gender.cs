using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HyVo_CanadaGames.Models
{
    public class Gender
    {
        public int ID { get; set; }

        [Display(Name = "Gender Code")]
        [Required(ErrorMessage = "A code is required.")]
        [StringLength(1, ErrorMessage = "Max 1 characters only.")]
        public string Code { get; set; }

        [Display(Name = "Gender Name")]
        [Required(ErrorMessage = "A gender name is required.")]
        [StringLength(10, ErrorMessage = "Max 10 characters only.")]
        public string Name { get; set; }

        public ICollection<Athlete> Athletes { get; set; }

        public Gender()
        {
            this.Athletes = new HashSet<Athlete>();
        }
    }
}
