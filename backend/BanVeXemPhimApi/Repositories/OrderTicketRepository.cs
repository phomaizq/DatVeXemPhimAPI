using AutoMapper;
using BanVeXemPhimApi.Common;
using BanVeXemPhimApi.Database;
using BanVeXemPhimApi.Models;
using BanVeXemPhimApi.Request;
using BanVeXemPhimApi.Respositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BanVeXemPhimApi.Repositories
{
    public class OrderTicketRepository : BaseRespository<OrderTicket>
    {
        private IMapper _mapper;
        public OrderTicketRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }
    }
}
