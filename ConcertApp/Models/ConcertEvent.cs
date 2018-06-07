using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertApp.Models
{
    public class ConcertEvent
    {
        public int ID { get; set; }
        public string Musician { get; set; }
        public int Tickets { get; set; }
        [DataType(DataType.Date)] //?
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public Location Location { get; set; }
        public int LocationID { get; set; }
        [Display(Name = "Event Type")]
        public EventType EventType { get; set; }
        public int EventTypeID { get; set; }
        public int Price { get; set; }
    }
}
