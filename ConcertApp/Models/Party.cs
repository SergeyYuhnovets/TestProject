using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertApp.Models
{
    public class Party : ConcertEvent
    {
        [Display(Name = "Minimum Age")]
        public int MinimumAge { get; set; }
    }
}
