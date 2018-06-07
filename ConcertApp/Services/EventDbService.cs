using Microsoft.EntityFrameworkCore;
using ConcertApp.Data;
using ConcertApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertApp.Services
{
    public class EventDbService
    {
        private readonly ApplicationDbContext _context;

        public EventDbService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClassicConcert> GetClassicConcert(int? id)
        {
            return await _context.ClassicConcert
                .Include(x => x.Location).Include(x => x.EventType)
                .Include(x => x.VocalType).SingleOrDefaultAsync(e => e.ID == id);
        }

        public async Task<OpenAir> GetOpenAir(int? id)
        {
            return await _context.OpenAir
                .Include(x => x.Location).Include(x => x.EventType)
                .SingleOrDefaultAsync(e => e.ID == id);
        }

        public async Task<Party> GetParty(int? id)
        {
            return await _context.Party
                .Include(x => x.Location).Include(x => x.EventType)
                .SingleOrDefaultAsync(e => e.ID == id);
        }

        public async Task<IList<ConcertEvent>> GetConcertEvents()
        {
            return await _context.ConcertEvent
                .Include(x => x.Location).Include(x => x.EventType)
                .ToListAsync();
        }

        public async Task<IList<ConcertEvent>> GetFiltredEvents(string Name, string EventTypeName)
        {
            return await _context.ConcertEvent
                .Include(x => x.Location).Include(x => x.EventType)
                .Where(x => x.Musician.Contains(Name) && x.EventType.Name == EventTypeName)
                .ToListAsync();
        }

        public void BuyTickets(ConcertEvent concert, int quantity=1)
        {
            concert.Tickets -= quantity;
            _context.ConcertEvent.Update(concert);
            _context.SaveChanges();
        }
    }
}
