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

namespace ConcertApp.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class ConcertEventsController : Controller
    {
        private EventTypeDbService EventTypeService;
        private EventDbService EventService;

        public ConcertEventsController(ApplicationDbContext context)
        {
            EventTypeService = new EventTypeDbService(context);
            EventService = new EventDbService(context);
        }

        public IList<ConcertEvent> ConcertEvents { get; set; }
        [BindProperty]
        [Display(Name = "Musician")]
        public string NameFilter { get; set; }
        [BindProperty]
        [Display(Name = "Event Type")]
        public string TypeFilter { get; set; }
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

        // GET: ConcertEvents/Details/5
        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                NotFound();
            }
            ViewData["TicketsError"] = false;

            EventType = EventTypeService.GetTypeById(id);

            ConcertEvent = null;    //do i need it?
            DefineConcert(id);

            if (ConcertEvent == null)
            {
                NotFound();
            }

            return View(ConcertEvent);
        }

        [HttpPost, ActionName("Details")]
        [AllowAnonymous]
        public IActionResult BuyOrBook(int? id, string button)
        {
            if (id == null)
            {
                NotFound();
            }

            DefineConcert(id);

            if (ConcertEvent == null)
            {
                NotFound();
            }

            // @TODO separate buying and booking logic
            if (button == "buy")
            {
                if (ConcertEvent.Tickets > 0)
                {
                    EventService.BuyTicket(ConcertEvent);
                    ViewData["TicketsError"] = false;
                }
                else
                {
                    ViewData["TicketsError"] = true;
                }

                // @TODO add page
                return View(ConcertEvent);
            }
            else if (button == "book")
            {
                if (ConcertEvent.Tickets > 0)
                {
                    EventService.BuyTicket(ConcertEvent);
                    ViewData["TicketsError"] = false;

                    EmailSender emailer = new EmailSender();
                    string Email = User.Identity. Name; //@TODO get real email
                    //string Email = "njnjnj1@dispostable.com"; //@TODO login++
                    string Subject = "Your booking confirmation";
                    string Message = "You are just booked a ticket to the concert"; //@TODO beautify message
                    emailer.SendEmail(Email, Subject, Message);
                }
                else
                {
                    ViewData["TicketsError"] = true;
                }

                // @TODO add page
                return View(ConcertEvent);
            }
            // @TODO add page
            return Redirect("/Index");
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
