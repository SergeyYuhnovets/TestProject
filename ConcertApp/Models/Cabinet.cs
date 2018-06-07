using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertApp.Models
{
    public class Cabinet
    {
        public int ID { get; set; }
        public string UserID { get; set; } //@TODO link user table
        public List<Ticket> Tickets { get; set; }
    }
}
