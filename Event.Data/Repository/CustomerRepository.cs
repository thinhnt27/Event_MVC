using Event.Data.Base;
using Event.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Data.Repository
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository()
        {

        }

        public CustomerRepository(Net1704_221_3_EventContext context) => _context = context;
    }
}
