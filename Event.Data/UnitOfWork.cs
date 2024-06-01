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
        private Net1704_221_3_EventContext _unitOfWorkContext;

        private CustomerRepository _customer;

        public UnitOfWork()
        {

        }

        public CustomerRepository CustomerRepository
        {
            get
            {
                return _customer ??= new Repository.CustomerRepository();
            }
        }
    }
}
