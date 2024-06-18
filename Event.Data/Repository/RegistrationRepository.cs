using Event.Data.Base;
using Event.Data.Models;

namespace Event.Data.Repository
{
    public class RegistrationRepository : GenericRepository<Registration>
    {
        public RegistrationRepository()
        {

        }
        public RegistrationRepository(Net1704_221_3_EventContext context) => _context = context;
    }
}
