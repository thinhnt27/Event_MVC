using Event.Data.Base;
using Event.Data.Models;

namespace Event.Data.Repository
{
    public class TicketRepository : GenericRepository<Ticket>
    {
        public TicketRepository()
        {
        }

        public TicketRepository(Net1704_221_3_EventContext context) => _context = context;
    }
}
