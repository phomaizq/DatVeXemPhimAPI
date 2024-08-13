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
    public class OrderTicketManagementService
    {
        private readonly UserRepository _userRepository;
        private readonly OrderTicketRepository _orderTicketRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public OrderTicketManagementService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _orderTicketRepository = new OrderTicketRepository(apiOption, databaseContext, mapper);
            _mapper = mapper;
            _webHost = webHost;
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public object GetModels(int limit, int page)
        {
            try
            {
                var query = this._orderTicketRepository.FindAll();

                return new Pagination<OrderTicket>(query, limit, page);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get user detail
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public object GetModelDetail(int modelId)
        {
            try
            {
                return _orderTicketRepository.FindOrFail(modelId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
