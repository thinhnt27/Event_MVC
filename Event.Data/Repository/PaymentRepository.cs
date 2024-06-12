using Event.Data.Base;
using Event.Data.Models;

namespace Event.Data.Repository
{
    public class PaymentRepository : GenericRepository<Payment>
    {
        public PaymentRepository()
        {
        }
        public PaymentRepository(Net1704_221_3_EventContext context)
        {
            this._context=context;
        }
    }
}