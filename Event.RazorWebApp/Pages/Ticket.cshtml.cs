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

        public Ticket Ticket { get; private set; }

        public void OnGet()
        {
            Tickets = this.GetTickets();
        }

        public void OnPost()
        {
        }

        private List<Ticket> GetTickets()
        {
            var ticketResults = _ticketBusiness.GetAll();
            if(ticketResults.Status > 0 && ticketResults.Result.Data != null)
            {
                return (List<Ticket>)ticketResults.Result.Data;
            }
            return new List<Ticket>();
        }



    }
}
