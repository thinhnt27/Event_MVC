using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Event.Data.Models;
using Event.Business.Category;

namespace Event.RazorWebApp.Pages.TicketPage
{
    public class CreateModel : PageModel
    {
        
        private TicketBusiness _ticketBusiness;

        public CreateModel()
        {
            _ticketBusiness ??= new TicketBusiness();
        }

        public IActionResult OnGet()
        {
        //ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId");
            return Page();
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _ticketBusiness.Save(Ticket);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
