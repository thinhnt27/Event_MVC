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
    public class DeleteModel : PageModel
    {
        private readonly ICustomerBussiness _business;

        public DeleteModel()
        {
            _business ??= new CustomerBussiness();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _business.GetById(id.Value);
            if (customer != null)
            {
                Customer = (Customer)customer.Data;
                await _business.DeleteById(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
