using Event.Data.Models;
using Event.Data.Repository;

namespace Event.Data
{
    public class UnitOfWork
    {
        private readonly Net1704_221_3_EventContext _context;
        private readonly TicketRepository _ticket;
        private readonly EventRepository _event;

        public UnitOfWork()
        {

        }
        public TicketRepository TicketRepository
        {
            get
            {
                return _ticket ?? new TicketRepository();
            }
        }
        public EventRepository EventRepository
        {
            get
            {
                return _event ?? new EventRepository();
            }
        }
    }
}
