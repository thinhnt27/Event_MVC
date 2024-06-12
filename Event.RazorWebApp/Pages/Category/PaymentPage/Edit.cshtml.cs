using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Event.Data.Models;
using Event.Business.Category;

namespace Event.RazorWebApp.Pages.Category.PaymentPage
{
    public class EditModel : PageModel
    {
        private readonly IPaymentBusiness _paymentBusiness;
        private readonly IRegistrationBusiness _registrationBusiness;
        private readonly ITicketBusiness _ticketBusiness;
        public EditModel()
        {
            _paymentBusiness ??= new PaymentBusiness();
            _registrationBusiness ??= new RegistrationBusiness();
            _ticketBusiness ??= new TicketBusiness();
        }

        [BindProperty]
        public Payment Payment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _paymentBusiness.GetById(id.Value);
            if (payment == null)
            {
                return NotFound();
            }
            Payment = payment.Data as Payment;
            var registrations = await _registrationBusiness.GetAll();
            var tickets = await _ticketBusiness.GetAll();
            ViewData["RegistrationId"] = new SelectList(registrations.Data as IEnumerable<Registration>, "RegistrationId", "RegistrationId");
            ViewData["TicketId"] = new SelectList(tickets.Data as IEnumerable<Ticket>, "TicketId", "TicketId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
            
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                await _paymentBusiness.Update(Payment);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await PaymentExists(Payment.PaymentId)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async  Task<bool> PaymentExists(int id)
        {
            return _paymentBusiness.GetById(id).Result.Data != null ;
        }
    }
}
