using Event.Business.Base;
using Event.Common;
using Event.Data.Models;
using Event.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Business.Category
{
    public interface IRegistrationBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Update(Registration payment);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> Save(Registration payment);
    }
    public class RegistrationBusiness : IRegistrationBusiness
    {
        //private readonly RegistrationDAO _DAO;
        private readonly UnitOfWork _unitOfWork;
        //public RegistrationBusiness(RegistrationDAO DAO)
        //{
        //    _DAO = DAO;
        //}

        public RegistrationBusiness()
        {
            _unitOfWork ??= new UnitOfWork(); // ?? là nếu _unitOfWork = null thì mới khởi tạo

        }


        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                //var payment = await _DAO.GetByIdAsync(id);
                var payment = await _unitOfWork.RegistrationRepository.GetByIdAsync(id);
                if (payment != null)
                {
                    //var result = await  _DAO.RemoveAsync(payment);
                    var result = await _unitOfWork.RegistrationRepository.RemoveAsync(payment);
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
                var payments = await _unitOfWork.RegistrationRepository.GetAllAsync();

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
                var payment = await _unitOfWork.RegistrationRepository.GetByIdAsync(id);

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

        public async Task<IBusinessResult> Save(Registration payment)
        {
            try
            {
                //int result = await _DAO.CreateAsync(payment);
                int result = await _unitOfWork.RegistrationRepository.CreateAsync(payment); //này chỉ ghi vào 1 bảng thì dùng


                //// Log the state of the entity before saving
                //Console.WriteLine("Registration state before save: " + _unitOfWork.RegistrationRepository.GetEntityState(payment));
                ////này dùng cho save nhiều bảng
                //_unitOfWork.RegistrationRepository.PrepareCreate(payment);

                //// Log the state of the entity after preparation
                //Console.WriteLine("Registration state after PrepareCreate: " + _unitOfWork.RegistrationRepository.GetEntityState(payment));


                //int result = await _unitOfWork.RegistrationRepository.SaveAsync();
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

        public async Task<IBusinessResult> Update(Registration payment)
        {
            try
            {
                //int result = await _DAO.UpdateAsync(payment);
                int result = await _unitOfWork.RegistrationRepository.UpdateAsync(payment);
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
