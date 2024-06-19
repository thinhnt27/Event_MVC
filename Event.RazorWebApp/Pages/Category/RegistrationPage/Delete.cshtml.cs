using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Event.Data.Models;
using Event.Business.Category;

namespace Event.RazorWebApp.Pages.RegistrationPage
{
    public class DeleteModel : PageModel
    {
        private readonly RegistrationBusiness _business;

        public DeleteModel()
        {
            _business ??= new RegistrationBusiness();
        }

        [BindProperty]
        public Registration Registration { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _business.GetById(id.Value);

            if (registration == null)
            {
                return NotFound();
            }
            else
            {
                Registration = registration.Data as Registration;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _business.DeleteById(id.Value);
            

            return RedirectToPage("./Index");
        }
    }
}
