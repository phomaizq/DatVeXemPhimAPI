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
using Microsoft.AspNetCore;
using static System.Net.Mime.MediaTypeNames;

namespace BanVeXemPhimApi.Services
{
    public class StatictisService
    {
        private readonly UserRepository _userRepository;
        private readonly OrderTicketRepository _orderTicketRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public StatictisService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _orderTicketRepository = new OrderTicketRepository(apiOption, databaseContext, mapper);
            _mapper = mapper;
            _webHost = webHost;
        }

        /// <summary>
        /// StatictisOrderTicket
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public object StatictisOrderTicket(DateTime dateFrom, DateTime dateTo, int limit, int page)
        {
            try
            {
                var orderTicketList = _orderTicketRepository.FindByCondition(row => row.CreatedDate >= dateFrom && row.CreatedDate <= dateTo);

                var totalMoney = orderTicketList.Sum(row => row.TotalPrice);
                return new
                {
                    List = new Pagination<OrderTicket>(orderTicketList, limit, page),
                    totalMoney = totalMoney
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
