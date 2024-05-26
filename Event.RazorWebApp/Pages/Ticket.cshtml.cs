using Event.Business.Category;
using Event.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event.RazorWebApp.Pages
{
    public class TicketModel : PageModel
    {
        private readonly TicketBusiness _ticketBusiness = new TicketBusiness();

        public List<Ticket> Tickets { get; private set; }

        [BindProperty]
        public Ticket Ticket { get; set; }

        public void OnGet()
        {
            Tickets = this.GetTickets();
        }

        public IActionResult OnPostCreateTicket()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var createdTicket = Create(Ticket);
            return RedirectToPage();
        }

        private List<Ticket> GetTickets()
        {
            var ticketResults = _ticketBusiness.GetAll();
            if (ticketResults.Status > 0 && ticketResults.Result.Data != null)
            {
                return (List<Ticket>)ticketResults.Result.Data;
            }
            return new List<Ticket>();
        }

        private Ticket Create(Ticket ticket)
        {
            var ticketResults = _ticketBusiness.Save(ticket);
            if (ticketResults.Result.Status > 0 && ticketResults.Result.Data != null)
            {
                return (Ticket)ticketResults.Result.Data;
            }
            return new Ticket();
        }


    }
}
