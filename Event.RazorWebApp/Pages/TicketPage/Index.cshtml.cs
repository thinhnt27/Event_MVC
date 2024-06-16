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
    public class IndexModel : PageModel
    {
        //private readonly Event.Data.Models.Net1704_221_3_EventContext _context;
        private TicketBusiness _ticketBusiness;

        public IndexModel()
        {
            _ticketBusiness ??= new TicketBusiness();
        }

        public IList<Ticket> Ticket { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var ticketResults = await _ticketBusiness.GetAll();
            if (ticketResults.Status > 0 && ticketResults.Data != null)
            {
                Ticket = (List<Ticket>)ticketResults.Data;
            }
        }
    }
}
