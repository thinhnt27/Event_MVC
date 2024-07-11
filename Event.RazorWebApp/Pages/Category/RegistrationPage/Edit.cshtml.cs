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

namespace Event.RazorWebApp.Pages.RegistrationPage
{
    public class EditModel : PageModel
    {
        private readonly RegistrationBusiness _business;

        public EditModel()
        {
            _business ??= new RegistrationBusiness();
        }

        [BindProperty]
        public Registration Registration { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var user = HttpContext.Session.Get("user");
            if (user == null)
            {
                return Redirect("../../Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _business.GetById(id.Value);
            if (registration == null)
            {
                return NotFound();
            }
            Registration = registration.Data as Registration;

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
            Registration.RegistrationDate = DateTime.Now;
            if (Registration.Checkin == true)
            {
                Registration.CheckinTime = DateTime.Now;
            }
            else
            {
                Registration.CheckinTime = null;
            }
            await _business.Update(Registration);

            return RedirectToPage("./Index");
        }

    }
}
