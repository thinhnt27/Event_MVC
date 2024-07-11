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
    public class DeleteModel : PageModel
    {
        private readonly ITicketBusiness _ticketBusiness;
        private readonly EventBusiness _eventBusiness;

        public DeleteModel()
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
                return Redirect("../../Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _ticketBusiness.GetById(id.Value);


            if (ticket == null || ticket.Data ==null)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _ticketBusiness.GetById(id.Value);
            if (ticket != null && ticket.Data!=null)
            {
                
                await _ticketBusiness.DeleteById(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
