using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Event.Data.Models;
using Event.Business.Category;

namespace Event.RazorWebApp.Pages.TicketPage
{
    public class CreateModel : PageModel
    {
        private TicketBusiness _ticketBusiness;
        private EventBusiness _eventBusiness;

        public CreateModel()
        {
            _ticketBusiness ??= new TicketBusiness();
            _eventBusiness ??= new EventBusiness();
        }

        public async Task<IActionResult> OnGet()
        {
            var user = HttpContext.Session.Get("user");
            if (user == null)
            {
                return Redirect("../../Index");
            }

            var eventData = await _eventBusiness.GetAll();
            var events = eventData.Data != null ? eventData.Data as List<Events> : new List<Events> { };
            ViewData["EventId"] = new SelectList(events, "EventId", "EventName");
            return Page();
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            // Custom validation logic
            if (Ticket.Price <= 0)
            {
                ModelState.AddModelError("Ticket.Price", "Price must be greater than 0");
            }

            if (Ticket.AvailableQuantity <= 0)
            {
                ModelState.AddModelError("Ticket.AvailableQuantity", "Available Quantity must be greater than 0");
            }

            if (Ticket.StartedTime >= Ticket.EndedTime)
            {
                ModelState.AddModelError("Ticket.StartedTime", "Start Time must be earlier than End Time");
            }
            if(Ticket.TicketType is null)
            {
                ModelState.AddModelError("Ticket.TicketType", "Ticket type must be not null");
            }
            if(Ticket.Price is null)
            {
                ModelState.AddModelError("Ticket.Price", "Price must be not null");

            }
            if(Ticket.AvailableQuantity is null)
            {
                ModelState.AddModelError("Ticket.AvailableQuantity", "Available Quantity must be not null");
            }
            if(Ticket.StartedTime is null)
            {
                ModelState.AddModelError("Ticket.StartedTime", "Start Time must be not null");
            }
            if (Ticket.EndedTime is null)
            {
                ModelState.AddModelError("Ticket.EndedTime", "Ended Time must be not null");
            }
            if (Ticket.CreatedBy is null)
            {
                ModelState.AddModelError("Ticket.CreatedBy", "Created must be not null");
            }

            if (!ModelState.IsValid)
            {
                var eventData = await _eventBusiness.GetAll();
                var events = eventData.Data != null ? eventData.Data as List<Events> : new List<Events> { };
                ViewData["EventId"] = new SelectList(events, "EventId", "EventName");
                return Page();
            }

            Ticket.CreatedDate = DateTime.Now;
            await _ticketBusiness.Save(Ticket);

            return RedirectToPage("./Index");
        }
    }
}
