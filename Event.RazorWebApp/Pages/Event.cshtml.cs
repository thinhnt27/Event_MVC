using Event.Business.Category;
using Event.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event.RazorWebApp.Pages
{
    public class EventModel : PageModel
    {
        private readonly EventBusiness _eventBusiness = new EventBusiness();

        public IList<Events> Events { get; private set; }
        public IList<EventType> EventTypes { get; set; }
        public string SearchTerm { get; set; }
        public int? EventTypeId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; }

        [BindProperty]
        public Events Event { get; set; }

        public void OnGet()
        {
            Events = this.GetEvents();
            EventTypes = this.GetEventTypes();
        }

        public IActionResult OnPostCreateEvent()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var createdEvent = Create(Event);
            return RedirectToPage();
        }

        private List<Events> GetEvents()
        {
            var eventResults = _eventBusiness.GetAll();
            if (eventResults.Status > 0 && eventResults.Result.Data != null)
            {
                return (List<Events>)eventResults.Result.Data;
            }
            return new List<Events>();
        }

        private List<EventType> GetEventTypes()
        {
            var eventResults = _eventBusiness.GetEventTypes();
            if (eventResults.Status > 0 && eventResults.Result.Data != null)
            {
                return (List<EventType>)eventResults.Result.Data;
            }
            return new List<EventType>();
        }

        private Events Create(Events _event)
        {
            var eventResults = _eventBusiness.Save(_event);
            if (eventResults.Result.Status > 0 && eventResults.Result.Data != null)
            {
                return (Events)eventResults.Result.Data;
            }
            return new Events();
        }

        public IActionResult OnGetEditEvent(int id)
        {
            Event = GetEventById(id);
            if (Event == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPostEditEvent()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updatedEvent = Update(Event);
            return RedirectToPage();
        }

        private Events GetEventById(int id)
        {
            var eventResults = _eventBusiness.GetById(id);
            if (eventResults.Status > 0 && eventResults.Result.Data != null)
            {
                return (Events)eventResults.Result.Data;
            }
            return null;
        }

        private Events Update(Events _event)
        {
            var eventResults = _eventBusiness.Update(_event);
            if (eventResults.Result.Status > 0 && eventResults.Result.Data != null)
            {
                return (Events)eventResults.Result.Data;
            }
            return null;
        }
    }
}
