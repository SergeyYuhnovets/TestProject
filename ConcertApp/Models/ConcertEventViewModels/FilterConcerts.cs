using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertApp.Models.ConcertEventViewModels
{
    public class FilterConcerts
    {
        public IList<ConcertEvent> ConcertEvents { get; set; }
        [BindProperty]
        [Display(Name = "Musician")]
        public string NameFilter { get; set; }
        [BindProperty]
        [Display(Name = "Event Type")]
        public string TypeFilter { get; set; }
        [BindProperty]
        public IList<EventType> AvailableTypes { get; set; }
        public bool IsAdmin { get; set; }
    }
}
