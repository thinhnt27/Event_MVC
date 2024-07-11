using Event.Business.Category;
using Event.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event.RazorWebApp.Pages.Category.EventPage
{
    public class DetailsModel(IEventBusiness _eventBusiness) : PageModel
    {
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
    }
}
