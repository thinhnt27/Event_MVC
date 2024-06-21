using Event.Business.Base;
using Event.Data;
using Event.Data.DAO;
using Event.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Business.Category
{
    public interface ICustomerBussiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int code);
        Task<IBusinessResult> Save(Customer customer);
        Task<IBusinessResult> Update(Customer customer);
        Task<IBusinessResult> DeleteById(int code);
    }

    public class CustomerBussiness : ICustomerBussiness
    {
        private readonly UnitOfWork _unitOfWork;
        //private readonly CustomerDAO _DAO;

        public CustomerBussiness()
        {
            //_DAO = new CustomerDAO();
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                var customers = await _unitOfWork.CustomerRepository.GetAllAsync();

                if (customers == null)
                {
                    return new BusinessResult(4, "No customer data");
                }
                else
                {
                    return new BusinessResult(4, "Get customer list success", customers);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int code)
        {
            try
            {

                //var currencies = _DAO.GetAll();
                var customerId = await _unitOfWork.CustomerRepository.GetByIdAsync(code);

                if (customerId == null)
                {
                    return new BusinessResult(4, "No customer Id");
                }
                else
                {
                    return new BusinessResult(1, "Get customer ID success", customerId);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Customer customer)
        {
            try
            {
                _unitOfWork.CustomerRepository.PrepareCreate(customer);
                int result = await _unitOfWork.CustomerRepository.SaveAsync();
                if (result > 0)
                {
                    return new BusinessResult(4, "Save customer success");
                }
                else
                {
                    return new BusinessResult(4, "Save customer fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> Update(Customer customer)
        {
            try
            {
                int result = await _unitOfWork.CustomerRepository.UpdateAsync(customer);
                if (result > 0)
                {
                    return new BusinessResult(4, "Update customer success");
                }
                else
                {
                    return new BusinessResult(4, "Update customer fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> DeleteById(int code)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(code);
                if (customer != null)
                {
                    var result = await _unitOfWork.CustomerRepository.RemoveAsync(customer);
                    if (result)
                    {
                        return new BusinessResult(4, "Delete customer success");
                    }
                    else
                    {
                        return new BusinessResult(4, "Delete customer fail");
                    }
                }
                else
                {
                    return new BusinessResult(4, "No customer Id");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }
    }
}
