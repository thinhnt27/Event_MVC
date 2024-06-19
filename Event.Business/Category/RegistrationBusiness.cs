using Event.Business.Base;
using Event.Data;
using Event.Data.DAO;
using Event.Data.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Business.Category
{
    public interface IRegistrationBusiness
    {
        Task<IBusinessResult> GetAll(/*int? id, int? eventId, string? name, string? type, string? mail*/);
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Save(Registration registration);
        Task<IBusinessResult> Update(Registration registration);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> GetRegistration(int? id, int? eventId, string? name, string? type, string? mail);
    }
    public class RegistrationBusiness : IRegistrationBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public RegistrationBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetRegistration(int? id, int? eventId, string? name, string? type, string? mail)
        {
            try
            {
                var registrations = await _unitOfWork.RegistrationRepository.GetAllAsync();

                if (registrations == null)
                {
                    return new BusinessResult(4, "No Registration data");
                }

                if (id.HasValue)
                {
                    registrations = registrations.Where(x => x.RegistrationId == id.Value).ToList();
                }

                if (eventId.HasValue)
                {
                    registrations = registrations.Where(x => x.EventId == eventId.Value).ToList();
                }

                if (!string.IsNullOrEmpty(name))
                {
                    registrations = registrations.Where(x => x.ParticipantName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(type))
                {
                    registrations = registrations.Where(x => x.ParticipantType.Contains(type, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(mail))
                {
                    registrations = registrations.Where(x => x.AttendeeEmail.Contains(mail, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                var registrationList = registrations.ToList(); 

                if (!registrationList.Any())
                {
                    return new BusinessResult(4, "No Registration data");
                }
                else
                {
                    return new BusinessResult(4, "Get Registration success", registrationList);
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

                var registration = await _unitOfWork.RegistrationRepository.GetAllAsync();
                
                if (registration == null)
                {
                    return new BusinessResult(4, "No Registraion data");
                }
                else
                {
                    return new BusinessResult(4, "Get Registration success", registration);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var registration = await _unitOfWork.RegistrationRepository.GetByIdAsync(id);

                if (registration == null)
                {
                    return new BusinessResult(4, "No Registration data");
                }
                else
                {
                    return new BusinessResult(4, "Get Registraion success", registration);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Registration registration)
        {
            try
            {
               
                int result = await _unitOfWork.RegistrationRepository.CreateAsync(registration);
                if (result > 0)
                {
                    return new BusinessResult(4, "Save Registration success");
                }
                else
                {
                    return new BusinessResult(4, "Save Registration is not success");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Update(Registration registration)
        {
            try
            {
                int result = await _unitOfWork.RegistrationRepository.UpdateAsync(registration);
                if (result > 0)
                {
                    return new BusinessResult(4, "Update Registration success");
                }
                else
                {
                    return new BusinessResult(4, "Update Registration is not success");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                var registration = _unitOfWork.RegistrationRepository.GetById(id);

                if (registration != null)
                {
                    _unitOfWork.RegistrationRepository.PrepareRemove(registration);
                    var result = await _unitOfWork.RegistrationRepository.SaveAsync();

                    if (result == 1)
                    {
                        return new BusinessResult(4, "Delete Registration success");
                    }
                    else
                    {
                        return new BusinessResult(4, "Delete Registration is not success");
                    }
                }
                else
                {
                    return new BusinessResult(4, "No data of Registration");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

    }
}