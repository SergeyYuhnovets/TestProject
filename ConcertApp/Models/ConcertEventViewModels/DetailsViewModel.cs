using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertApp.Models.ConcertEventViewModels
{
    public class DetailsViewModel
    {
        public ConcertEvent ConcertEvent { get; set; }
        [BindProperty]
        [Range(1, 10)]
        [Required]
        public int Quantity { get; set; }
    }
}
