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
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalPages { get; set; }

        [BindProperty]
        public Events Event { get; set; }

        public string? EventName { get; set; }
        public DateTime? EventDate { get; set; }

        public string? EventDescription { get; set; }
        public int? NumberTickets { get; set; }
        public string? Sponsor { get; set; }
        public string? SubjectCode { get; set; }
        public string? Location { get; set; }
        public ActionResult OnGet(int? EventTypeId,string? EventName, DateTime? EventDate, string? EventDescription, int? NumberTickets, string? Sponsor, string? SubjectCode, string? Location, int? PageIndex, int? PageSize)
        {
            var user = HttpContext.Session.Get("user");
            if (user == null)
            {
                return Redirect("../Login");
            }
            if (PageIndex.HasValue)
            {
                this.PageIndex = PageIndex.Value;
            }
            else
            {
                this.PageIndex = 1;
            }
            if (PageSize.HasValue)
            {
                this.PageSize = PageSize.Value;
            }
            else
            {
                this.PageSize = 5;
            }
            Events = this.GetEvents(); 
            EventTypes = this.GetEventTypes();
            this.EventTypeId = EventTypeId;
            this.EventName = EventName;
            this.EventDate= EventDate;
            this.EventDescription = EventDescription;
            this.NumberTickets = NumberTickets;
            this.Sponsor = Sponsor;
            this.SubjectCode = SubjectCode;
            this.Location = Location;
            this.PageIndex=PageIndex.HasValue? PageIndex.Value : this.PageIndex;
            this.PageSize = PageSize.HasValue ? PageSize.Value : this.PageSize;
            if(EventTypeId.HasValue) Events=Events.Where(x=>x.EventTypeId== this.EventTypeId).ToList();  
            if (!string.IsNullOrEmpty(this.EventName))
            {
                Events = Events.Where(x => x.EventName.ToLower().Contains(this.EventName.ToLower())).ToList();
            }

            if (this.EventDate.HasValue)
            {
                Events = Events.Where(x => x.EventDate.Date == this.EventDate.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(this.EventDescription))
            {
                Events = Events.Where(x => x.EventDescription.ToLower().Contains(this.EventDescription.ToLower())).ToList();
            }

            if (this.NumberTickets.HasValue)
            {
                Events = Events.Where(x => x.NumberTickets == this.NumberTickets.Value).ToList();
            }

            if (!string.IsNullOrEmpty(this.Sponsor))
            {
                Events = Events.Where(x => x.Sponsor.ToLower().Contains(this.Sponsor.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(this.SubjectCode))
            {
                Events = Events.Where(x => x.SubjectCode.ToLower().Contains(this.SubjectCode.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(this.Location))
            {
                Events = Events.Where(x => x.Location.ToLower().Contains(this.Location.ToLower())).ToList();
            }

            // Implement paging
            Events = Events.Skip((int)((this.PageIndex - 1) * this.PageSize)).Take(this.PageSize).ToList();
            return Page();
        
        }

        public IActionResult OnPostCreateEvent()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Event.CreatedDate=DateTime.Now;
            Event.UpdatedDate=DateTime.Now;
            Event.UpdatedBy = Event.CreatedBy;
            var createdEvent = Create(Event);
            Event = new Events();
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
            Event.UpdatedDate = DateTime.Now;
            var updatedEvent = Update(Event);
            Event = new Events();
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDelete(int eventId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result =await _eventBusiness.DeleteById(Event.EventId);
           
            Event = new Events();
            return RedirectToPage(); // Optionally redirect to another page
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
