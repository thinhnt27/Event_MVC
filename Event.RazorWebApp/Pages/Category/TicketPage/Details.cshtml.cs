using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Event.Data.Models;
using Event.Business.Category;

namespace Event.RazorWebApp.Pages.TicketPage
{
    public class DetailsModel : PageModel
    {
        private readonly TicketBusiness _ticketBusiness;
        private readonly EventBusiness _eventBusiness;

        public DetailsModel()
        {
            _ticketBusiness ??= new TicketBusiness();
            _eventBusiness = new EventBusiness();
        }

        public Ticket Ticket { get; set; } = default!;
        public Events Event { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
            var user = HttpContext.Session.Get("user");
            if (user == null)
            {
                return Redirect("../../Login");
            }
            var ticket = await _ticketBusiness.GetById(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }
            
            else
            {
                Ticket = ticket.Data as Ticket;
                var evnt = await _eventBusiness.GetById((int)Ticket.EventId);
                Ticket.Event = evnt.Data as Events;
            }
            return Page();
        }
    }
}
