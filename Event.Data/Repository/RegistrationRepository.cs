using Event.Data.Base;
using Event.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Data.Repository
{
    public class RegistrationRepository : GenericRepository<Registration>
    {
        public RegistrationRepository()
        {
        }
        public RegistrationRepository(Net1704_221_3_EventContext context)
        {
            this._context = context;
        }
    }
}
