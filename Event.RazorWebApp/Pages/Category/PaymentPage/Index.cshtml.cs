using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Event.Data.Models;
using Event.Business.Category;

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
        public DateTime? SelectedPaymentDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? SelectedAmountPaid { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? SelectedStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? SelectedMaxiumDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? SelectedMiniumDate { get; set; }

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

        public async Task<ActionResult> OnGetAsync()
        {
            var user = HttpContext.Session.Get("user");
            if (user == null)
            {
                return Redirect("../../../Login");
            }
            await LoadDropdownsAsync();
            await LoadPaymentsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadDropdownsAsync();
            await LoadPaymentsAsync();
            return Page();
        }

        private async Task LoadPaymentsAsync()
        {
            var result = await _paymentBusiness.GetList("asc", "PaymentId", SelectedStatus, SelectedTicketId, SelectedRegistrationId, PaymentType, SelectedAmountPaid, SelectedTicketQuantity, SelectedMaxiumDate, SelectedMiniumDate);

            if (result.Status > 0 && result.Data != null)
            {
                Payments = result.Data as List<Payment> ?? new List<Payment>();
                TotalPage = (int)Math.Ceiling(Payments.Count / (double)PageSize);
                Payments = Payments.Skip((PageSize)*(PageIndex-1)).Take(PageSize).ToList();
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
            TicketList = new SelectList(tickets.Data as List<Ticket> ?? new List<Ticket>(), nameof(Ticket.TicketId), nameof(Ticket.TicketType));

            var registrations = await _registrationBusiness.GetAll();
            if (registrations?.Data != null)
            {
                RegistrationList = new SelectList(registrations.Data as List<Registration> ?? new List<Registration>(), nameof(Registration.RegistrationId), nameof(Registration.ParticipantName));
            }
        }
    }
}
