using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertApp.Data;
using ConcertApp.Models;
using ConcertApp.Models.CabinetViewModels;
using ConcertApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConcertApp.Controllers
{
    public class CabinetController : Controller
    {
        private TicketDbService TicketService;
        private readonly UserManager<ApplicationUser> UserManager;

        public CabinetController(UserManager<ApplicationUser> userManager,
                                 ApplicationDbContext context)
        {
            UserManager = userManager;
            TicketService = new TicketDbService(context);
        }

        public IActionResult Index()
        {
            string UserID = UserManager.GetUserId(User);
            IList<Ticket> BoughtTickets = TicketService.GetBoughtTickets(UserID).Result;
            IList<Ticket> BookedTickets = TicketService.GetBookedTickets(UserID).Result;
            var model = new TicketsViewModel
            {
                BoughtTickets = BoughtTickets,
                BookedTickets = BookedTickets
            };
            return View(model);
        }
    }
}