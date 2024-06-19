using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Event.Data.Models;
using Event.Business.Category;

namespace Event.RazorWebApp.Pages.RegistrationPage
{
    public class IndexModel : PageModel
    {
        private readonly IRegistrationBusiness _business;

        public IndexModel(IRegistrationBusiness business)
        {
            _business = business;
        }

        // Search properties
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? EvenId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Name { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Type { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Mail { get; set; }

        // Pagination properties
        public const int ItemsPerPage = 5;
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int CountPages { get; set; }

        public IList<Registration> Registrations { get; set; } = new List<Registration>();

        public async Task OnGetAsync()
        {
           
            var result = await _business.GetRegistration(Id, EvenId, Name, Type, Mail);

            if (result != null && result.Data != null)
            {
                Registrations = result.Data as List<Registration> ?? new List<Registration>();
            }
            else
            {
                Registrations = new List<Registration>();
            }

          
            int totalRegistrations = Registrations.Count;
            CountPages = (int)Math.Ceiling((double)totalRegistrations / ItemsPerPage);

            if (CurrentPage < 1)
            {
                CurrentPage = 1;
            }
            else if (CurrentPage > CountPages)
            {
                CurrentPage = CountPages;
            }


            Registrations = Registrations
                .Skip((CurrentPage - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .ToList();
        }
    }
}
