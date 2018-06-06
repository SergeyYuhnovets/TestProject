using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertApp.Models
{
    public class ClassicConcert : ConcertEvent
    {
        [Display(Name = "Vocal Type")]
        public VocalType VocalType { get; set; }
        public int VocalTypeID { get; set; }
        public string Composer { get; set; }
        public string Name { get; set; }
    }
}
