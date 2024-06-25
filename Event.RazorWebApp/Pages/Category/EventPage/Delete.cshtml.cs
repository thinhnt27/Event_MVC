using Event.Business.Category;
using Event.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event.RazorWebApp.Pages.Category.EventPage
{
    public class DeleteModel : PageModel
    {
        private readonly IEventBusiness _eventBusiness;

        public DeleteModel()
        {
            _eventBusiness ??= new EventBusiness();
        }

        [BindProperty]
        public Events Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _event = await _eventBusiness.GetById(id.Value);

            if (_event == null)
            {
                return NotFound();
            }
            else
            {
                Event = _event.Data as Events;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _event = await _eventBusiness.GetById(id.Value);
            if (_event != null && _event.Data != null)
            {
                await _eventBusiness.DeleteById(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
