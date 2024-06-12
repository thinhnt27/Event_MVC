using Event.Business.Category;
using Event.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event.RazorWebApp.Pages.Category
{
    public class PaymentPageModel : PageModel
    {
        private readonly IPaymentBusiness _payment;

        public PaymentPageModel(IPaymentBusiness paymentBussiness)
        {
            _payment = paymentBussiness;
        }

        [BindProperty]
        public List<Payment> Payments { get; set; }
        [BindProperty]
        public Payment payment { get; set; } = new Payment();

        public async Task OnGetAsync()
        {
            var result = await _payment.GetAll();
            if (result != null && result.Data != null)
            {
                Payments = (List<Payment>)result.Data;
            }
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {

            await _payment.Save(payment);
            payment = new Payment() { };
            return RedirectToPage("PaymentPage");
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {

            await _payment.Update(payment);
            payment = new Payment() { };
            return RedirectToPage("PaymentPage");
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var deletedPayment = await _payment.DeleteById(id);
            payment = new Payment() { };
            return RedirectToPage("PaymentPage");
        }
    }
}
