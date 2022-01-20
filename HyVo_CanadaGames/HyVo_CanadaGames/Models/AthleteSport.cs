using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyVo_CanadaGames.Models
{
    public class AthleteSport
    {
        public int SportID { get; set; }
        public Sport Sport { get; set; }

        public int AthleteID { get; set; }
        public Athlete Athlete { get; set; }

    }
}
