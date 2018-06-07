using ConcertApp.Data;
using ConcertApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertApp.Services
{
    public class CabinetDbService
    {
        private readonly ApplicationDbContext _context;

        public CabinetDbService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Cabinet GetCabinet(string UserID)
        {
            return _context.Cabinets.SingleOrDefault(c => c.UserID == UserID);
        }
    }
}
