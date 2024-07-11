using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Event.Data.Models;
using Event.Business.Category;

namespace Event.RazorWebApp.Pages.Category.CustomerPage
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerBussiness _business;

        public CreateModel()
        {
            _business ??= new CustomerBussiness();
        }

        public IActionResult OnGet()
        {
            var user = HttpContext.Session.Get("user");
            if (user == null)
            {
                return Redirect("../../../Index");
            }
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Customer.UpdatedBy = Customer.CreatedBy;
            Customer.CreatedDate = DateTime.Now;
            Customer.UpdatedDate = DateTime.Now;
            await _business.Save(Customer);

            return RedirectToPage("./Index");
        }
    }
}
