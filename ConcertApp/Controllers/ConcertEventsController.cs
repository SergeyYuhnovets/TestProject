using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConcertApp.Data;
using ConcertApp.Models;
using ConcertApp.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using ConcertApp.Models.ConcertEventViewModels;
using Microsoft.AspNetCore.Identity;

namespace ConcertApp.Controllers
{
    [Authorize("AdminOnly")]
    public class ConcertEventsController : Controller
    {
        private readonly EventTypeDbService EventTypeService;
        private readonly EventDbService EventService;
        private readonly TicketDbService TicketService;
        private readonly CabinetDbService CabinetService;
        private readonly UserManager<ApplicationUser> UserManager;

        public ConcertEventsController(UserManager<ApplicationUser> userManager,
                                        ApplicationDbContext context)
        {
            UserManager = userManager;
            EventTypeService = new EventTypeDbService(context);
            EventService = new EventDbService(context);
            TicketService = new TicketDbService(context);
            CabinetService = new CabinetDbService(context);
        }

        public IList<ConcertEvent> ConcertEvents { get; set; }
        [BindProperty]
        [Display(Name = "Musician")]
        public string NameFilter { get; set; }
        [BindProperty]
        [Display(Name = "Event Type")]
        public string TypeFilter { get; set; }
        [BindProperty]
        [Range(1, 10)]
        public int Quantity { get; set; }
        [BindProperty]
        public IList<EventType> AvailableTypes { get; set; }

        public ConcertEvent ConcertEvent { get; set; }
        public int EventType { get; set; }

        // GET: ConcertEvents
        [AllowAnonymous]
        public IActionResult Index()
        {
            ConcertEvents = EventService.GetConcertEvents().Result;
            AvailableTypes = EventTypeService.GetEventTypes().Result;
            ViewData["active"] = 1;

            var model = new FilterConcerts
            {
                ConcertEvents = ConcertEvents,
                NameFilter = NameFilter,
                TypeFilter = TypeFilter,
                AvailableTypes = AvailableTypes,
                IsAdmin = User.IsInRole("Admin")
            };

            return View(model);
        }

        [HttpPost, ActionName("Index")]
        [AllowAnonymous]
        public IActionResult Filter()
        {
            NameFilter = NameFilter ?? "";
            ConcertEvents = EventService.GetFiltredEvents(NameFilter, TypeFilter).Result;
            AvailableTypes = EventTypeService.GetEventTypes().Result;
            ViewData["active"] = EventTypeService.GetActiveComboItemIndex(TypeFilter);

            FilterConcerts model = new FilterConcerts
            {
                ConcertEvents = ConcertEvents,
                NameFilter = NameFilter,
                TypeFilter = TypeFilter,
                AvailableTypes = AvailableTypes,
                IsAdmin = User.IsInRole("Admin")
            };

            return View(model);
        }

        // GET: ConcertEvents/Details/5
        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["TicketsError"] = false;

            EventType = EventTypeService.GetTypeById(id);

            ConcertEvent = null;    //do i need it?
            DefineConcert(id);

            if (ConcertEvent == null)
            {
                return NotFound();
            }

            DetailsViewModel model = new DetailsViewModel
            {
                ConcertEvent = ConcertEvent,
                Quantity = Quantity
            };

            return View(model);
        }



        [HttpPost]
        //[Authorize("UserOnly")]   //why not work?
        [AllowAnonymous]
        public IActionResult Buy(int id)
        {
            DefineConcert(id);

            if (ConcertEvent == null)
            {
                NotFound();
            }

            ViewData["TicketsError"] = false;

            if (ConcertEvent.Tickets - Quantity >= 0)
            {
                BuyTickets(ConcertEvent, Quantity);
            }
            else
            {
                ViewData["TicketsError"] = true;
            }

            DetailsViewModel model = new DetailsViewModel
            {
                ConcertEvent = ConcertEvent,
                Quantity = Quantity
            };

            // @TODO add page
            return View("Details", model);
        }

        [HttpPost]
        //[Authorize("UserOnly")]   //why not work?
        [AllowAnonymous]
        public IActionResult Book(int id)
        {
            DefineConcert(id);

            if (ConcertEvent == null)
            {
                NotFound();
            }

            ViewData["TicketsError"] = false;

            if (ConcertEvent.Tickets - Quantity >= 0)
            {
                BuyTickets(ConcertEvent);

                EmailSender emailer = new EmailSender();
                string Email = User.Identity.Name; //@TODO get real email
                string Subject = "Your booking confirmation";
                string Message = String.Format("You just booked a ticket to {0}. Date: {1}. Location: {2}",
                    ConcertEvent.Musician, ConcertEvent.Date, ConcertEvent.Location.Name);
                emailer.SendEmail(Email, Subject, Message);
            }
            else
            {
                ViewData["TicketsError"] = true;
            }

            DetailsViewModel model = new DetailsViewModel
            {
                ConcertEvent = ConcertEvent,
                Quantity = Quantity
            };

            // @TODO add page
            return View("Details", model);
        }

        private void BuyTickets(ConcertEvent concert, int quantity=1)
        {
            EventService.BuyTickets(concert, quantity);
            string userID = UserManager.GetUserId(User);
            Cabinet cabinet = CabinetService.GetCabinet(userID); //@TODO null?
            Ticket ticket = new Ticket
            {
                IsBought = true,
                Cabinet = cabinet,
                ConcertEvent = concert
            };
            TicketService.CreateTicket(ticket);
        }

        private void BookTickets(ConcertEvent concert)
        {
            EventService.BuyTickets(concert);
            var user = UserManager.GetUserId(User);
            Cabinet cabinet = CabinetService.GetCabinet(user); //@TODO null?
            Ticket ticket = new Ticket
            {
                IsBought = false,
                Cabinet = cabinet,
                ConcertEvent = concert
            };
            TicketService.CreateTicket(ticket);
        }

        private void DefineConcert(int? id)
        {
            EventType = EventTypeService.GetTypeById(id);

            ConcertEvent = null;    //do i need it?
            if (EventType == 1) // Classics
            {
                ConcertEvent = EventService.GetClassicConcert(id).Result;
            }
            else if (EventType == 2) // Party
            {
                ConcertEvent = EventService.GetParty(id).Result;
            }
            else if (EventType == 3) // OpenAir
            {
                ConcertEvent = EventService.GetOpenAir(id).Result;
            }
        }

        [AllowAnonymous]
        public IActionResult Map()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult GetCoords()
        {
            IList<ConcertEvent> concerts = EventService.GetConcertEvents().Result;
            return new JsonResult(concerts);
        }
    }
}
