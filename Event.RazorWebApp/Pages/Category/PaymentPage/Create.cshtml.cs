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
            var registrations = await _registrationBusiness.GetAll();
            var tickets = await _ticketBusiness.GetAll();
            ViewData["RegistrationId"] = new SelectList(registrations.Data as IEnumerable<Registration>, "RegistrationId", "RegistrationId");
            ViewData["TicketId"] = new SelectList(tickets.Data as IEnumerable<Ticket>, "TicketId", "TicketId");
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

           /* _context.Payments.Add(Payment);
            await _context.SaveChangesAsync();*/

            return RedirectToPage("./Index");
        }
    }
}
