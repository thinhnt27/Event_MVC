using Azure.Core;
using Event.Business.Category;
using Event.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel;

namespace Event.RazorWebApp.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly IRegistrationBusiness _registrationBusiness;
        public RegistrationModel(IRegistrationBusiness registrationBusiness)
        {
            _registrationBusiness = registrationBusiness;
        }

        [BindProperty]
        public List<Registration> Registrations { get; set; } = default!;
        [BindProperty]
        public Registration Registration { get; set; }
        [BindProperty(SupportsGet =true)]
        public int id { get; set; }
        public async Task OnGetAsync()
        {
            //Registrations = GetRegistrationsAsync();
            //if (id != 0)
            //{
            //    Registration = await GetRegistrationByIdAsync(id);

            //    if (Registration != null)
            //    {
            //        Registrations.Clear();
            //        Registrations.Add(Registration);
            //    }
            //}
            //else
            //{
            //    Registrations.ToString();
            //}
            
            Registrations = GetRegistrationsAsync();
        }
        public async Task<IActionResult> OnPostCreate()
        {
            if (!ModelState.IsValid || Registration == null)
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
            _registrationBusiness.Save(Registration);
            Registration = null;
            return RedirectToPage("Registration");
            
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!ModelState.IsValid || Registration == null)
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
            await _registrationBusiness.Update(Registration);
            Registration = null;
            return RedirectToPage("Registration");
            
        }
        public List<Registration> GetRegistrationsAsync()
        {
            var result = _registrationBusiness.GetAll();
            if (result.Status > 0 && result.Result.Data != null)
            {
                Registrations = (List<Registration>)result.Result.Data;
                return Registrations;
            }
            return new List<Registration>();
        }
        public async Task<Registration> GetRegistrationByIdAsync(int id)
        {
            var result = await _registrationBusiness.GetById(id);
            if (result.Status > 0 && result.Data != null)
            {
                return (Registration)result.Data;
            }
            return null;
        } 
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            
            var result = await _registrationBusiness.DeleteById(id);
            if (result.Status > 0)
            {
                return RedirectToPage("Registration");
            }
            else
            {
                return Page();
            }
        }
    }
}
