using Event.Business.Base;
using Event.Common;
using Event.Data;
using Event.Data.Models;

namespace Event.Business.Category
{
    public interface ITicketBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Update(Ticket ticket);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> Save(Ticket ticket);
    }
    public class TicketBusiness : ITicketBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        

        public TicketBusiness()
        {
            _unitOfWork ??= new UnitOfWork(); // ?? là nếu _unitOfWork = null thì mới khởi tạo

        }


        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                //var ticket = await _DAO.GetByIdAsync(id);
                var ticket = await _unitOfWork.TicketRepository.GetByIdAsync(id);
                if (ticket != null)
                {
                    //var result = await  _DAO.RemoveAsync(ticket);
                    var result = await _unitOfWork.TicketRepository.RemoveAsync(ticket);
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
                //var tickets = await _DAO.GetAllAsync();
                var tickets = await _unitOfWork.TicketRepository.GetAllAsync();

                if (tickets == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, tickets);
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

                //var ticket = await _DAO.GetByIdAsync(id);
                var ticket = await _unitOfWork.TicketRepository.GetByIdAsync(id);

                if (ticket == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, ticket);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Ticket ticket)
        {
            try
            {
                //int result = await _DAO.CreateAsync(ticket);
                int result = await _unitOfWork.TicketRepository.CreateAsync(ticket); //này chỉ ghi vào 1 bảng thì dùng


                //// Log the state of the entity before saving
                //Console.WriteLine("Ticket state before save: " + _unitOfWork.TicketRepository.GetEntityState(ticket));
                ////này dùng cho save nhiều bảng
                //_unitOfWork.TicketRepository.PrepareCreate(ticket);

                //// Log the state of the entity after preparation
                //Console.WriteLine("Ticket state after PrepareCreate: " + _unitOfWork.TicketRepository.GetEntityState(ticket));


                //int result = await _unitOfWork.TicketRepository.SaveAsync();
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

        public async Task<IBusinessResult> Update(Ticket ticket)
        {
            try
            {
                //int result = await _DAO.UpdateAsync(ticket);
                int result = await _unitOfWork.TicketRepository.UpdateAsync(ticket);
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
