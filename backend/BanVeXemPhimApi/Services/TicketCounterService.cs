using AutoMapper;
using BanVeXemPhimApi.Common;
using BanVeXemPhimApi.Database;
using BanVeXemPhimApi.Dto;
using BanVeXemPhimApi.Models;
using BanVeXemPhimApi.Repositories;
using System;
using System.Linq;

namespace BanVeXemPhimApi.Services
{
    public class TicketCounterService
    {
        private readonly UserRepository _userRepository;
        private readonly OrderTicketRepository _orderTicketRepository;
        private readonly ScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public TicketCounterService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _orderTicketRepository = new OrderTicketRepository(apiOption, databaseContext, mapper);
            _scheduleRepository = new ScheduleRepository(apiOption, databaseContext, mapper);
            _mapper = mapper;
        }

        /// <summary>
        /// get order ticket is active
        /// </summary>
        /// <returns></returns>
        public object GetOrderTicket(int page, int limit, string? email, string? name, string? numberPhone)
        {
            try
            {
                var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                var scheduleIdList = _scheduleRepository.FindByCondition(row => row.PlaySchedule >= date).ToList().Select(row => row.Id).ToList();
                var query = this._orderTicketRepository.FindByCondition(row => scheduleIdList.Contains(row.ScheduleId));
                query = query.OrderByDescending(row => row.Id);

                if (!string.IsNullOrEmpty(numberPhone))
                {
                    query = query.Where(row => row.NumberPhone.Contains(numberPhone));
                }

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(row => row.Name.Contains(name));
                }

                if (!string.IsNullOrEmpty(email))
                {
                    query = query.Where(row => row.Email.Contains(email));
                }

                return new Pagination<OrderTicket>(query, limit, page);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Order ticket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object GetOrderTicketDetail(int id)
        {
            try
            {
                var orderTicket = _orderTicketRepository.FindOrFail(id);
                if(orderTicket == null)
                {
                    throw new ValidateError("id invalid!");
                }

                return orderTicket;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
