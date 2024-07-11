using Castle.MicroKernel.Registration;
using Event.Business.Category;
using Event.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Event.RazorWebApp.Pages.Category.EventPage
{
    public class EditModel(IEventBusiness _eventBusiness) : PageModel
    {
        [BindProperty]
        public Events Event { get; set; } = default!;

        [BindProperty]
        public string? SelectedEventType { get; set; }

        public SelectList EventTypeList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _event = await _eventBusiness.GetById(id.Value);
            if (_event?.Data is not Events eventData)
            {
                return NotFound();
            }

            Event = eventData;
            SelectedEventType = Event.EventType.EventTypeName;

            var eventTypes = await _eventBusiness.GetEventTypes();
            EventTypeList = new SelectList(eventTypes.Data as List<EventType> ?? new List<EventType>(), nameof(EventType.EventTypeName), nameof(EventType.EventTypeName));

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var eventType = await _eventBusiness.GetEventTypeByName(SelectedEventType);
            if (eventType?.Data is EventType eventTypeData)
            {
                Event.EventType = eventTypeData;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Selected event type is invalid.");
                return Page();
            }

            await _eventBusiness.Update(Event);

            return RedirectToPage("./Index");
        }
    }
}
