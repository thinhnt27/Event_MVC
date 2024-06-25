using Event.Business.Category;
using Event.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Event.RazorWebApp.Pages.Category.EventPage
{
    public class CreateModel : PageModel
    {
        private readonly IEventBusiness _eventBusiness;

        [BindProperty]
        public Events Event { get; set; } = default!;

        public CreateModel()
        {
            _eventBusiness ??= new EventBusiness();
        }
        public async Task<IActionResult> OnGet()
        {
            var eventTypes = await _eventBusiness.GetEventTypes();

            ViewData["EventTypes"] = new SelectList(eventTypes.Data != null ? eventTypes.Data as List<EventType> : new List<EventType>(), nameof(EventType.EventTypeName), nameof(EventType.EventTypeName));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _eventBusiness.Save(Event);
            /*Clear input*/
            Event.EventDescription = null;
            Event.EventDate = null;
            Event.EventType = null;
            Event.Location = null;
            Event.SubjectCode = null;
            Event.Sponsor = null;
            Event.NumberTickets = null;
            Event.EventTypeId = null;
            Event.EventName = null;

            return RedirectToPage("./Index");
        }
    }
}
