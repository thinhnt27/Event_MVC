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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Ticket.UpdatedBy = Ticket.CreatedBy;
            Ticket.CreatedDate= DateTime.Now;
            Ticket.UpdatedDate= DateTime.Now;
            await _ticketBusiness.Save(Ticket);
           

            return RedirectToPage("./Index");
        }
    }
}
