using Event.Data.Models;
using Event.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Data
{
    public class UnitOfWork
    {
        private readonly Net1704_221_3_EventContext _context;
        private readonly TicketRepository _ticket;

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
    }
}
