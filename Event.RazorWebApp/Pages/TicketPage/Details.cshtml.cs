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

        public DetailsModel()
        {
            _ticketBusiness ??= new TicketBusiness();
        }

        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var ticket = await _ticketBusiness.GetById(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }
            else
            {
                Ticket = ticket.Data as Ticket;
            }
            return Page();
        }
    }
}
