using Microsoft.EntityFrameworkCore;
using ConcertApp.Data;
using ConcertApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertApp.Services
{
    public class EventTypeDbService
    {
        private readonly ApplicationDbContext _context;

        public EventTypeDbService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<EventType>> GetEventTypes()
        {
            return await _context.EventType.ToListAsync();
        }

        public int GetTypeById(int? id)
        {
            return _context.ConcertEvent.SingleOrDefault(e => e.ID == id).EventTypeID;
        }

        public int GetActiveComboItemIndex(string Name)
        {
            return _context.EventType.SingleOrDefault(e => e.Name == Name).ID;
        }
    }
}
