using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Event.Data.Models;
using Event.Business.Category;
using System.Diagnostics.CodeAnalysis;

namespace Event.RazorWebApp.Pages.TicketPage
{
    public class EditModel : PageModel
    {
        private readonly TicketBusiness _ticketBusiness;
        private readonly EventBusiness _eventBusiness;
        public EditModel()
        {
            _ticketBusiness ??= new TicketBusiness();
            _eventBusiness ??= new EventBusiness();
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var user = HttpContext.Session.Get("user");
            if (user == null)
            {
                return Redirect("../../Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var ticket =  await _ticketBusiness.GetById(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }
            Ticket = ticket.Data as Ticket;
            var eventData = await _eventBusiness.GetAll();
            var events = eventData.Data != null ? eventData.Data as List<Events> : new List<Events>();
            ViewData["EventId"] = new SelectList(events, "EventId", "EventName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
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

            if (Ticket.TicketType == null)
            {
                ModelState.AddModelError("Ticket.TicketType", "Ticket type must be not null");
            }

            if (Ticket.Price == null)
            {
                ModelState.AddModelError("Ticket.Price", "Price must be not null");
            }

            if (Ticket.AvailableQuantity == null)
            {
                ModelState.AddModelError("Ticket.AvailableQuantity", "Available Quantity must be not null");
            }

            if (Ticket.StartedTime == null)
            {
                ModelState.AddModelError("Ticket.StartedTime", "Start Time must be not null");
            }

            if (Ticket.EndedTime == null)
            {
                ModelState.AddModelError("Ticket.EndedTime", "Ended Time must be not null");
            }

            if (Ticket.UpdatedBy == null)
            {
                ModelState.AddModelError("Ticket.UpdatedBy", "Updated must be not null");
            }

            if (!ModelState.IsValid)
            {
                var eventData = await _eventBusiness.GetAll();
                var events = eventData.Data != null ? eventData.Data as List<Events> : new List<Events>();
                ViewData["EventId"] = new SelectList(events, "EventId", "EventName");
                return Page();
            }
            try
            {   
                Ticket.UpdatedDate = DateTime.Now;
                await _ticketBusiness.Update(Ticket);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await TicketExists(Ticket.TicketId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> TicketExists(int id)
        {
            var ticket = await _ticketBusiness.GetById(id);
            return ticket.Data != null;
        }
    }
}
