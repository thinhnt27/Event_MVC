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
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        //public bool? Status {  get; set; }
        public int? EventId { get; set; }
        //public decimal? Price { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TicketType { get; set; }
        [BindProperty(SupportsGet = true)]
        public decimal? Price { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? AvailableQuantity { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool? Status { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CreatedBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public string UpdatedBy { get; set; }
        public IList<Events> events { get; set; }
        public EventBusiness _eventBusiness { get; set; }
        public IndexModel()
        {
            _ticketBusiness ??= new TicketBusiness();
            _eventBusiness ??= new EventBusiness();
            
        }

        public IList<Ticket> Ticket { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync(DateTime? StartTime, DateTime? EndTime, bool? Status, int? EventId, decimal? Price, int PageIndex = 1, int PageSize = 5)
        {
            var user = HttpContext.Session.Get("user");
            if (user == null)
            {
                return Redirect("../../Login");
            }
            this.PageIndex = PageIndex;
            this.PageSize = PageSize;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.Status = Status;
            this.EventId = EventId;
            this.Price = Price;

            events = (await _eventBusiness.GetAll()).Data as List<Events>;
            var ticketResults = await _ticketBusiness.GetAll();
            if (ticketResults.Data != null)
            {
                Ticket = (List<Ticket>)ticketResults.Data;

                if (!string.IsNullOrEmpty(TicketType))
                {
                    Ticket = Ticket.Where(x => x.TicketType.Contains(TicketType, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (Price.HasValue)
                {
                    Ticket = Ticket.Where(x => x.Price.HasValue && x.Price.Value == Price.Value).ToList();
                }

                if (AvailableQuantity.HasValue)
                {
                    Ticket = Ticket.Where(x => x.AvailableQuantity.HasValue && x.AvailableQuantity.Value == AvailableQuantity.Value).ToList();
                }

                //if (Status.HasValue)
                //{
                //    Ticket = Ticket.Where(x => x.Status.HasValue && x.Status.Value == Status.Value).ToList();
                //}

                if (!string.IsNullOrEmpty(CreatedBy))
                {
                    Ticket = Ticket.Where(x => x.CreatedBy.Contains(CreatedBy, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(UpdatedBy))
                {
                    Ticket = Ticket.Where(x => x.UpdatedBy.Contains(UpdatedBy, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                Ticket = Ticket.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            }
            return Page();
        }

    }
}
