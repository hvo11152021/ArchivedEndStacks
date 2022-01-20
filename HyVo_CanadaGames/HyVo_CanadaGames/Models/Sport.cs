using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HyVo_CanadaGames.Models
{
    public class Sport
    {
        public int ID { get; set; }

        [Display(Name = "Sport Code")]
        [Required(ErrorMessage = "A code is required.")]
        [StringLength(3, ErrorMessage = "Max 3 characters only.")]
        public string Code { get; set; }
        
        [Display(Name = "Sport Name")]
        [Required(ErrorMessage = "A sport name is required.")]
        [StringLength(50, ErrorMessage = "Max 50 characters only.")]
        public string Name { get; set; }

        public ICollection<Athlete> Athletes { get; set; }

        [Display(Name = "Athlete Sport")]
        public ICollection<AthleteSport> AthleteSports { get; set; }

        public Sport()
        {
            this.Athletes = new HashSet<Athlete>();
            this.AthleteSports = new HashSet<AthleteSport>();
        }
    }
}
