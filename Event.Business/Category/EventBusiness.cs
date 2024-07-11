﻿using Event.Business.Base;
using Event.Common;
using Event.Data;
using Event.Data.DAO;
using Event.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Event.Business.Category
{
    public interface IEventBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetEventTypes();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Update(Events _event);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> Save(Events _event);
        Task<IBusinessResult> GetEvents(int? id, string? type, string? name, string? sponsor, string? subjectCode, string? location);
        Task<IBusinessResult> GetEventTypeByName(string? selectedEventType);
    }
    public class EventBusiness : IEventBusiness
    {
        //private readonly EventDAO _DAO;
        private readonly UnitOfWork _unitOfWork;
        //public EventBusiness(EventDAO DAO)
        //{
        //    _DAO = DAO;
        //}
        public EventBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                //var _event = await _DAO.GetByIdAsync(id);
                var _event = await _unitOfWork.EventRepository.GetByIdAsync(id);
                if (_event != null)
                {
                    //var result = await  _DAO.RemoveAsync(ticket);
                    var result = await _unitOfWork.EventRepository.RemoveAsync(_event);
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
                //var _events = await _DAO.GetAllAsync();
                var events = await _unitOfWork.EventRepository.GetAllAsync();

                if (events == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, events);
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

                //var _event = await _DAO.GetByIdAsync(id);
                var _event = await _unitOfWork.EventRepository.GetByIdAsync(id);

                if (_event == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, _event);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetEventTypes()
        {
            try
            {
                var eventTypes = await _unitOfWork.EventRepository.GetEventTypes();

                if (eventTypes == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, eventTypes);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetEvents(int? id, string? type, string? name, string? sponsor, string? subjectCode, string? location)
        {
            try
            {
                var events = await _unitOfWork.EventRepository.GetAllAsync();

                if (events == null)
                {
                    return new BusinessResult(4, "No Registration data");
                }

                if (id.HasValue)
                {
                    events = events.Where(x => x.EventId == id.Value).ToList();
                }

                if (!string.IsNullOrEmpty(type))
                {
                    events = events.Where(x => x.EventType != null && x.EventType.EventTypeName != null && x.EventType.EventTypeName.Contains(type)).ToList();
                }


                if (!string.IsNullOrEmpty(name))
                {
                    events = events.Where(x => x.EventName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(sponsor))
                {
                    events = events.Where(x => x.Sponsor.Contains(sponsor, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(subjectCode))
                {
                    events = events.Where(x => x.SubjectCode.Contains(subjectCode, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(location))
                {
                    events = events.Where(x => x.Location.Contains(location, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                var eventList = events.ToList();

                if (!eventList.Any())
                {
                    return new BusinessResult(4, "No Registration data");
                }
                else
                {
                    return new BusinessResult(4, "Get Registration success", eventList);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Events _event)
        {
            try
            {
                //int result = await _DAO.CreateAsync(ticket);
                int result = await _unitOfWork.EventRepository.CreateAsync(_event); //này chỉ ghi vào 1 bảng thì dùng


                //// Log the state of the entity before saving
                //Console.WriteLine("Event state before save: " + _unitOfWork.EventRepository.GetEntityState(_event));
                ////này dùng cho save nhiều bảng
                //_unitOfWork.TicketRepository.PrepareCreate(ticket);

                //// Log the state of the entity after preparation
                //Console.WriteLine("Event state after PrepareCreate: " + _unitOfWork.EventRepository.GetEntityState(_event));


                //int result = await _unitOfWork.EventRepository.SaveAsync();
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

        public async Task<IBusinessResult> Update(Events _event)
        {
            try
            {
                //int result = await _DAO.UpdateAsync(ticket);
                int result = await _unitOfWork.EventRepository.UpdateAsync(_event);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UDATE, Const.SUCCESS_UDATE_MSG);
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

        public async Task<IBusinessResult> GetEventTypeByName(string? selectedEventType)
        {
            try
            {
                #region Business rule
                #endregion

                //var _event = await _DAO.GetByIdAsync(id);
                var _event = await _unitOfWork.EventRepository.GetByName(selectedEventType);

                if (_event == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ, Const.SUCCESS_READ_MSG, _event);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
    }
}
