using Event.Data.Base;
using Event.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Data.Repository
{
    public class TicketRepository :GenericRepository<Ticket>
    {
        public TicketRepository()
        {
        }
        public TicketRepository(Net1704_221_3_EventContext context)
        {
            this._context = context;
        }
    }
}
