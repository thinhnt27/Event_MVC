using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Event.Data.Models;
using Event.Business.Category;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Event.RazorWebApp.Pages.Category.PaymentPage
{
    public class IndexModel : PageModel
    {
        private readonly IPaymentBusiness _paymentBusiness;
        private readonly ITicketBusiness _ticketBusiness;
        private readonly IRegistrationBusiness _registrationBusiness;

        public SelectList TicketList { get; set; }
        public SelectList RegistrationList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedTicketId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedRegistrationId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedTicketQuantity { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly? SelectedPaymentDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? SelectedAmountPaid { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? SelectedStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly? SelectedMaxiumDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly? SelectedMiniumDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? AmountPaid { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PaymentType { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 5;
        public int TotalPage { get; set; }
        public IList<Payment> Payments { get; set; } = new List<Payment>();
  

        public IndexModel()
        {
            _paymentBusiness ??= new PaymentBusiness();
            _ticketBusiness ??= new TicketBusiness();
            _registrationBusiness ??= new RegistrationBusiness();
        }

        public async Task OnGetAsync()
        {
            await LoadDropdownsAsync();
            await LoadPaymentsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadDropdownsAsync();
            await LoadPaymentsAsync();
            return Page();
        }

        private async Task LoadPaymentsAsync()
        {   
            var result = await _paymentBusiness.GetList(PageIndex, PageSize, "asc", "PaymentId", SelectedStatus, SelectedTicketId, SelectedRegistrationId, PaymentType, AmountPaid, SelectedTicketQuantity, SelectedMaxiumDate, SelectedMiniumDate);

            if (result.Status > 0 && result.Data != null)
            {
                Payments = result.Data as List<Payment> ?? new List<Payment>();
                if (Payments.Count % 2 == 0)
                    TotalPage = (int)Math.Ceiling(Payments.Count / (double)PageSize);
                else
                    TotalPage = (int)Math.Ceiling(Payments.Count / (double)PageSize)+1;

            }
            else
            {
                Payments = new List<Payment>();
                TotalPage = 1;
            }
        }

        private async Task LoadDropdownsAsync()
        {
            var tickets = await _ticketBusiness.GetAll();

            TicketList = new SelectList(tickets.Data as List<Ticket> ?? new List<Ticket>(), nameof(Ticket.TicketId), nameof(Ticket.TicketId));


            var registrations = await _registrationBusiness.GetAll();
            if (registrations?.Data != null)
            {
                RegistrationList = new SelectList(registrations.Data as List<Registration> ?? new List<Registration>(), nameof(Registration.RegistrationId), nameof(Registration.RegistrationId));
            }
        }
    }
}
