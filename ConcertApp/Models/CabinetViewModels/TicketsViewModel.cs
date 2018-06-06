using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertApp.Models.CabinetViewModels
{
    public class TicketsViewModel
    {
        public IList<Ticket> BoughtTickets { get; set; }
        public IList<Ticket> BookedTickets { get; set; }
    }
}
