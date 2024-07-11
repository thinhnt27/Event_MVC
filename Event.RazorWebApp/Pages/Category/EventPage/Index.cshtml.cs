using Castle.MicroKernel.Registration;
using Event.Business.Category;
using Event.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Event.RazorWebApp.Pages.Category.EventPage
{
    public class IndexModel(IEventBusiness eventBusiness) : PageModel
    {
        private readonly IEventBusiness _eventBusiness = eventBusiness;

        public SelectList EventTypesList { get; set; }
        public SelectList EventSelectList { get; set; }

        // Search properties
        [BindProperty(SupportsGet = true)]
        public int? SelectedEventId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedEventName { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? SelectedEventDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedEventType { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedEventDescription { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedNumberTickets { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedSponsor { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedSubjectCode { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedLocation { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? SelectedMaxiumDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? SelectedMiniumDate { get; set; }

        // Pagination properties
        [BindProperty(SupportsGet = true)]
        public int ItemsPerPage { get; set; } = 5;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int CountPages { get; set; }
        public IList<Events> EventList { get; set; } = new List<Events>();

        public async Task<ActionResult> OnGetAsync([FromQuery] int? p)
        {
            var user = HttpContext.Session.Get("user");
            if (user == null)
            {
                return Redirect("../../../Index");
            }
            if (p.HasValue)
            {
                CurrentPage = p.Value;
            }
            await LoadEventsAsync();
            await LoadDropdownsAsync();
            return Page();
        }

        private async Task LoadEventsAsync()
        {
            var result = await _eventBusiness.GetEvents(
                SelectedEventId,
                SelectedEventType, 
                SelectedEventName,
                SelectedSponsor,
                SelectedSubjectCode,
                SelectedLocation);

            if (result != null && result.Data != null)
            {
                EventList = result.Data as List<Events> ?? new List<Events>();
            }
            else
            {
                EventList = new List<Events>();
            }


            int totalEvents = EventList.Count;
            CountPages = (int)Math.Ceiling((double)totalEvents / ItemsPerPage);

            if (CurrentPage < 1)
            {
                CurrentPage = 1;
            }
            else if (CurrentPage > CountPages)
            {
                CurrentPage = CountPages;
            }


            EventList = EventList
                .Skip((CurrentPage - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .ToList();
        }


        private async Task LoadDropdownsAsync()
        {
            var eventTypes = await _eventBusiness.GetEventTypes();
            if (eventTypes?.Data != null)
            {
                EventTypesList = new SelectList(eventTypes.Data as List<EventType> ?? new List<EventType>(), nameof(EventType.EventTypeId), nameof(EventType.EventTypeName));
            }
        }
    }
}
