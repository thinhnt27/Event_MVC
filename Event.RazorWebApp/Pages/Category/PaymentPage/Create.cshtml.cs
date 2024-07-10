using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Event.Data.Models;
using Event.Business.Category;

namespace Event.RazorWebApp.Pages.Category.PaymentPage
{
    public class CreateModel : PageModel
    {
        private readonly IPaymentBusiness _paymentBusiness;
        private readonly ITicketBusiness _ticketBusiness;
        private readonly IRegistrationBusiness _registrationBusiness;

        public CreateModel()
        {
            _paymentBusiness ??=new PaymentBusiness();
            _ticketBusiness ??=new TicketBusiness();
            _registrationBusiness ??=new RegistrationBusiness();
        }

        public async Task<IActionResult> OnGet()
        {
            var user = HttpContext.Session.Get("user");
            if (user == null)
            {
                return Redirect("../../../Index");
            }
            var registrations = await _registrationBusiness.GetAll();
            var tickets = await _ticketBusiness.GetAll();
    
            ViewData["RegistrationId"] = new SelectList(registrations.Data!= null ? registrations.Data as List<Registration> : new List<Registration>(), "RegistrationId", "RegistrationId");
            ViewData["TicketId"] = new SelectList(tickets.Data != null ?  tickets.Data as List<Ticket> : new List<Ticket>(), "TicketId", "TicketId");
            return Page();
        }

        [BindProperty]
        public Payment Payment { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Payment.UpdatedBy = Payment.CreatedBy;
            Payment.CreatedDate = Payment.UpdatedDate = DateTime.Now;   
            await _paymentBusiness.Save(Payment);
            Payment.Status = null;
            Payment.TicketQuantity = null;
            Payment.Ticket = null;
            Payment.PaymentType = null;
            Payment.PaymentDate = null;
            
            return RedirectToPage("./Index");
        }
    }
}
