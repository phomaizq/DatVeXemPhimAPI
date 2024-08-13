using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using BanVeXemPhimApi.Common;
using BanVeXemPhimApi.Common.Enum;
using BanVeXemPhimApi.Database;
using BanVeXemPhimApi.Dto;
using BanVeXemPhimApi.Models;
using BanVeXemPhimApi.Repositories;
using BanVeXemPhimApi.Request;
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BanVeXemPhimApi.Services
{
    public class DashboardService
    {
        private readonly CinemaRepository _cinemaRepository;
        private readonly AdminRepository _adminRepository;
        private readonly UserRepository _userRepository;
        private readonly OrderTicketRepository _orderTicketRepository;
        private readonly MovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public DashboardService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _cinemaRepository = new CinemaRepository(apiOption, databaseContext, mapper);
            _adminRepository = new AdminRepository(apiOption, databaseContext, mapper);
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _orderTicketRepository = new OrderTicketRepository(apiOption, databaseContext, mapper);
            _movieRepository = new MovieRepository(apiOption, databaseContext, mapper);
            _mapper = mapper;
        }

        /// <summary>
        /// Get schedule
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public object GetTotal()
        {
            try
            {
                return new
                {
                    TotalUser = _userRepository.FindAll().Count(),
                    TotalEmployee = _adminRepository.FindAll().Count(),
                    TotalCinema = _cinemaRepository.FindAll().Count(),
                    TotalTicket = _orderTicketRepository.FindAll().Count(),
                    TotalMovie = _movieRepository.FindAll().Count(),
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// statictis number ticket in 7 last days
        /// </summary>
        /// <returns></returns>
        public object StatisticNumberTicketIn7LastDays()
        {
            try
            {
                var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                var numberTicketList = new List<int>();

                for(int i = 1; i <= 7; i++)
                {
                    var date = today.AddDays(-i);
                    var finishDate = date.AddDays(1);
                    var ticketByDateList = _orderTicketRepository.FindByCondition(row=> row.CreatedDate > date && row.CreatedDate < finishDate).ToList();
                    var numberTicket = 0;
                    foreach(var item in ticketByDateList)
                    {
                        var numberSeatChoosed = item.SeatList.Split(",").Length;
                        numberTicket += numberSeatChoosed;
                    }
                    numberTicketList.Add(numberTicket);
                }
                return numberTicketList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
