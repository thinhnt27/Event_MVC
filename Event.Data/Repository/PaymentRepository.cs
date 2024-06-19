using Abp.Domain.Uow;
using Abp.Linq.Expressions;
using Event.Data.Base;
using Event.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<List<Payment>> GetPayments(int? pageIndex = null, int? pageSize = null, string? order = null, string? sorted = null, bool? status = null, int? ticketId = null, int? registrationId = null, string? type = null, decimal? amountPaid = null, int? quantity = null, DateTime? maxiumDate = null, DateTime? miniumDate = null)
        {
            var predicate = PredicateBuilder.New<Payment>(true);
            if (status.HasValue) predicate = predicate.And(x => x.Status == status.Value);
            if (ticketId.HasValue) predicate = predicate.And(x => x.TicketId == ticketId.Value);
            if (registrationId.HasValue) predicate = predicate.And(x => x.RegistrationId == registrationId.Value);
            if (!string.IsNullOrEmpty(type)) predicate = predicate.And(x => x.PaymentType.ToLower().Contains(type.ToLower()));
            if (amountPaid.HasValue) predicate = predicate.And(x => x.AmountPaid == amountPaid.Value);
            if (maxiumDate.HasValue) predicate = predicate.And(x => x.PaymentDate <= maxiumDate.Value);
            if (miniumDate.HasValue) predicate = predicate.And(x => x.PaymentDate >= miniumDate.Value);

            Func<IQueryable<Payment>, IOrderedQueryable<Payment>> orderBy = null;
            if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(sorted))
            {
                orderBy = query =>
                {
                    if (order.ToLower().Equals("desc"))
                    {
                        query = query.OrderByDescending(x => EF.Property<object>(x, sorted));
                    }
                    else
                    {
                        query = query.OrderBy(x => EF.Property<object>(x, sorted));
                    }
                    return (IOrderedQueryable<Payment>)query;
                };
            }

            var paymentsQuery = this.Get(predicate, orderBy, x => x.Include("Registration").Include("Ticket"));

            if (pageIndex.HasValue && pageSize.HasValue)
            {
                int validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
                int validPageSize = pageSize.Value > 0 ? pageSize.Value : 10;

                paymentsQuery = paymentsQuery.Skip(validPageIndex * validPageSize).Take(validPageSize);
            }

            return await paymentsQuery.ToListAsync();
        }
    }
}