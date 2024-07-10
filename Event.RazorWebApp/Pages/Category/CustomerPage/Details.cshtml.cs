using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Event.Data.Models;
using Event.Business.Category;

namespace Event.RazorWebApp.Pages.Category.CustomerPage
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerBussiness _business;

        public DetailsModel()
        {
            _business ??= new CustomerBussiness();
        }

        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var user = HttpContext.Session.Get("user");
            if (user == null)
            {
                return Redirect("../../../Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _business.GetById(id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = (Customer)customer.Data;
            }
            return Page();
        }
    }
}
