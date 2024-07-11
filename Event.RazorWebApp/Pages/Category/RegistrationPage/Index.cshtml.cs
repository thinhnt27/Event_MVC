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
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 5;
        public int TotalPages { get; set; }

        public IList<Registration> Registrations { get; set; } = new List<Registration>();

        public async Task<ActionResult> OnGetAsync()
        {
            var user = HttpContext.Session.Get("user");
            if (user == null)
            {
                return Redirect("../../../Index");
            }
            var result = await _business.GetRegistration(Id, EvenId, Name, Type, Mail);

            if (result != null && result.Data != null)
            {
                Registrations = result.Data as List<Registration> ?? new List<Registration>();
            }
            else
            {
                Registrations = new List<Registration>();
            }
            Registrations = Registrations
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToList();
            return Page();
        }
    }
}
