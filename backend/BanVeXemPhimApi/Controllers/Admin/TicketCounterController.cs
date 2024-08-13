using AutoMapper;
using BanVeXemPhimApi.Common;
using BanVeXemPhimApi.Database;
using BanVeXemPhimApi.Dto;
using BanVeXemPhimApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BanVeXemPhimApi.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Employee, Manager")]
    public class TicketCounterController : BaseApiController<TicketCounterController>
    {
        private readonly TicketCounterService _ticketCounterService;
        public TicketCounterController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _ticketCounterService = new TicketCounterService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// Get order ticket
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="numberPhone"></param>
        /// <returns></returns>
        [HttpGet("GetOrderTicket")]
        public IActionResult GetOrderTicket(int page, int limit, string? email, string? name, string? numberPhone)
        {
            try
            {
                var res = _ticketCounterService.GetOrderTicket(page, limit, email, name, numberPhone);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// get order ticket detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetOrderTicketDetail")]
        public IActionResult GetOrderTicketDetail(int id)
        {
            try
            {
                var res = _ticketCounterService.GetOrderTicketDetail(id);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
