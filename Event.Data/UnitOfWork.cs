using Event.Data.Models;
using Event.Data.Repository;

namespace Event.Data
{
    public class UnitOfWork
    {
        private readonly Net1704_221_3_EventContext _context;
        private readonly PaymentRepository _payment;

        public UnitOfWork()
        {

        }
        public PaymentRepository PaymentRepository
        {
            get
            {
                return _payment ?? new PaymentRepository();
            }
        }
    }
}