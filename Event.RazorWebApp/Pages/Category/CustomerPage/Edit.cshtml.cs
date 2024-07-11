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

namespace Event.RazorWebApp.Pages.Category.CustomerPage
{
    public class EditModel : PageModel
    {
        private readonly ICustomerBussiness _business;

        public EditModel()
        {
            _business ??= new CustomerBussiness();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var user = HttpContext.Session.Get("user");
            if (user == null)
            {
                return Redirect("../../../Login");
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

            Customer = (Customer)customer.Data;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Customer.UpdatedDate = DateTime.Now;
            await _business.Update(Customer);

            return RedirectToPage("./Index");
        }

        private async Task<bool> CustomerExists(int id)
        {
            var result = await _business.GetById(id);
            if(result.Data == null)
            {
                return false;
            }
            return true;
        }
    }
}
