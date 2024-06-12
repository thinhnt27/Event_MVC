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
    public class IndexModel : PageModel
    {
        private readonly IPaymentBusiness _paymentBusiness;

        public IndexModel()
        {
            _paymentBusiness ??= new PaymentBusiness();
        }

        public IList<Payment> Payment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var payments = _paymentBusiness.GetAll();
            if (payments.Status > 0 && payments.Result.Data != null)
                Payment = payments.Result.Data as List<Payment>;    
        }
    }
}
