using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Event.Data.Models;
using Event.Business.Category;
using System.ComponentModel;
using Event.Business.Base;
using System.Reflection;

namespace Event.RazorWebApp.Pages.RegistrationPage
{
    public class IndexModel : PageModel
    {
        private readonly IRegistrationBusiness _business;

        //search
        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? evenId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? name { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? type { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? mail { get; set; }
        //pagination properties
        public const int ItemPerPage = 10;
        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }
        public int countPages {  get; set; }
        public IndexModel()
        {
            _business ??= new RegistrationBusiness();
        }
        public IList<Registration> Registrations { get;set; } = default!;

        public async Task OnGetAsync()
        {
            //search
            var result = await _business.GetRegistration(id, evenId, name, type, mail);
            
            if(result != null && result.Status > 0 && result.Data != null)
            {
                Registrations = result.Data as List<Registration>;
            }
            //paging
            int totalRegistration = Registrations.Count;
            countPages = (int)Math.Ceiling( (double)totalRegistration/ItemPerPage);
            if(currentPage < 1)
            {
                currentPage = 1;
            }
            Registrations = (result.Data as List<Registration>)
                .Skip((currentPage - 1 ) * ItemPerPage)
                .Take(ItemPerPage)
                .ToList(); // Explicitly convert to List<Registration>
        }
        
    }
}
