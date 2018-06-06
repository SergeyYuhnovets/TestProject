using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertApp.Models
{
    public class OpenAir : ConcertEvent
    {
        [Display(Name = "How to get")]
        public string HowToGet { get; set; }
        public string Headliner { get; set; }
    }
}
