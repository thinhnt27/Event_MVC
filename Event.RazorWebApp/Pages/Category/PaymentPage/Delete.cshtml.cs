using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Event.Data.Models;
using Event.Business.Category;

namespace Event.RazorWebApp.Pages.Category.PaymentPage
{
    public class DeleteModel : PageModel
    {
        private readonly IPaymentBusiness _paymentBusiness;

        public DeleteModel()
        {
            _paymentBusiness ??=new PaymentBusiness();
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
            else
            {
                Payment = payment.Data as Payment;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _paymentBusiness.GetById(id.Value);
            if (payment!=null && payment.Data != null)
            {
                await _paymentBusiness.DeleteById(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
