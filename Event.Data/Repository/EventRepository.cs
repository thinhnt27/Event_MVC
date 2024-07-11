using Abp.Linq.Expressions;
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
    }

    public class EventRepository : GenericRepository<Events>, IEventRepository
    {
        public EventRepository(Net1704_221_3_EventContext context)
        {
            this._context = context;
        }

        public async Task<EventType> GetByName(string? selectedEventType)
        {
            return await _context.Set<EventType>().FirstOrDefaultAsync(e => e.EventTypeName.Equals(selectedEventType));
        }

        public async Task<IList<EventType>> GetEventTypes()
        {
            return _context.Set<EventType>().ToList();
        }
    }
}
