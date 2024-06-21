using Event.Data.Base;
using Event.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Data.Repository
{
    interface IEventRepository
    {
        Task<IList<EventType>> GetEventTypes();
        Task<IList<Events>> GetEvents();
    }

    public class EventRepository : GenericRepository<Events>, IEventRepository
    {
        protected readonly Net1704_221_3_EventContext _context;
        protected readonly DbSet<EventType> _dbSetEventType;
        protected readonly DbSet<Events> _dbSetEvents;

        public EventRepository(Net1704_221_3_EventContext _context)
        {
            _context ??= new Net1704_221_3_EventContext();
            _dbSetEventType ??= _context.Set<EventType>();
            _dbSetEvents ??= _context.Set<Events>();
        }

        public async Task<IList<EventType>> GetEventTypes()
        {
            return _dbSetEventType.ToList();
        }

        public async Task<IList<Events>> GetEvents()
        {
            return _dbSetEvents.Include(e => e.EventType).ToList();
        }
    }
}
