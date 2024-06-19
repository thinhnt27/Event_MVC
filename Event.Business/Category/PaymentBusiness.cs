
using Event.Business.Base;
using Event.Common;
using Event.Data;
using Event.Data.Models;

namespace Event.Business.Category
{
    public interface IPaymentBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Update(Payment payment);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> Save(Payment payment);
        Task<IBusinessResult> GetList(int? pageIndex = null, int? pageSize = null, string? order = null, string? sorted = null, bool? status = null, int? ticketId = null, int? registrationId = null, string? type = null, decimal? amountPaid = null, int? quantity = null, DateTime? maxiumDate = null, DateTime? miniumDate = null);
    }
    public class PaymentBusiness : IPaymentBusiness
    {
        //private readonly PaymentDAO _DAO;
        private readonly UnitOfWork _unitOfWork;
        //public PaymentBusiness(PaymentDAO DAO)
        //{
        //    _DAO = DAO;
        //}

        public PaymentBusiness()
        {
            _unitOfWork ??= new UnitOfWork(); // ?? là nếu _unitOfWork = null thì mới khởi tạo

        }


        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                //var payment = await _DAO.GetByIdAsync(id);
                var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);
                if (payment != null)
                {
                    //var result = await  _DAO.RemoveAsync(payment);
                    var result = await _unitOfWork.PaymentRepository.RemoveAsync(payment);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }

            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                //var payments = await _DAO.GetAllAsync();
                var payments = await _unitOfWork.PaymentRepository.GetAllAsync();

                if (payments == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, payments);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                #region Business rule
                #endregion

                //var payment = await _DAO.GetByIdAsync(id);
                var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);

                if (payment == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, payment);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetList(int? pageIndex = null, int? pageSize = null, string? order = null, string? sorted = null, bool? status = null, int? ticketId = null, int? registrationId = null, string? type = null, decimal? amountPaid = null, int? quantity = null, DateTime? maxiumDate = null, DateTime? miniumDate = null)
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                //var payments = await _DAO.GetAllAsync();
                var payments = await _unitOfWork.PaymentRepository.GetPayments(pageIndex,pageSize,order,sorted,status,ticketId,registrationId,type,amountPaid,quantity,maxiumDate,miniumDate);

                if (payments == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, payments);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Payment payment)
        {
            try
            {
                //int result = await _DAO.CreateAsync(payment);
                int result = await _unitOfWork.PaymentRepository.CreateAsync(payment); //này chỉ ghi vào 1 bảng thì dùng


                //// Log the state of the entity before saving
                //Console.WriteLine("Payment state before save: " + _unitOfWork.PaymentRepository.GetEntityState(payment));
                ////này dùng cho save nhiều bảng
                //_unitOfWork.PaymentRepository.PrepareCreate(payment);

                //// Log the state of the entity after preparation
                //Console.WriteLine("Payment state after PrepareCreate: " + _unitOfWork.PaymentRepository.GetEntityState(payment));


                //int result = await _unitOfWork.PaymentRepository.SaveAsync();
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> Update(Payment payment)
        {
            try
            {
                //int result = await _DAO.UpdateAsync(payment);
                int result = await _unitOfWork.PaymentRepository.UpdateAsync(payment);
                if (result > 0)
                {
                    return new BusinessResult(Const.FAIL_UDATE, Const.SUCCESS_UDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UDATE, Const.FAIL_UDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
    }
}