﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Event.Data.Models;
using Event.Business.Category;
using Microsoft.VisualBasic;

namespace Event.RazorWebApp.Pages.RegistrationPage
{
    public class CreateModel : PageModel
    {
        private readonly RegistrationBusiness _business;

        public CreateModel()
        {
            _business ??= new RegistrationBusiness();
        }
        [BindProperty]
        public Registration Registration { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
                Registration.RegistrationDate = DateOnly.FromDateTime(DateTime.Now);
                if (Registration.Checkin == true)
                {
                    Registration.CheckinTime = DateTime.Now;
                }
                else
                {
                    Registration.CheckinTime = null;
                }
                await _business.Save(Registration);

            return RedirectToPage("./Index");
        }
    }
}
