using ConcertApp.Data;
using ConcertApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertApp.Services
{
    public class TicketDbService
    {
        private readonly ApplicationDbContext _context;

        public TicketDbService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Ticket>> GetBoughtTickets(string UserID)
        {
            return await _context.Tickets
                .Include(t => t.Cabinet)
                .Where(t => t.IsBought == true && t.Cabinet.UserID == UserID)
                .ToListAsync();
        }

        public async Task<IList<Ticket>> GetBookedTickets(string UserID)
        {
            return await _context.Tickets
                .Include(t => t.Cabinet)
                .Where(t => t.IsBought == false && t.Cabinet.UserID == UserID)
                .ToListAsync();
        }
    }
}
