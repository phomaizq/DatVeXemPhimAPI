using AutoMapper;
using AutoMapper.Configuration;
using BanVeXemPhimApi.Common;
using BanVeXemPhimApi.Controllers;
using BanVeXemPhimApi.Database;
using BanVeXemPhimApi.Dto;
using BanVeXemPhimApi.Models;
using BanVeXemPhimApi.Request;
using BanVeXemPhimApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BanVeXemPhimApi.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Manager, Employee, Accountant")]
    public class DashboardController : BaseApiController<DashboardController>
    {
        private readonly DashboardService _dashboardService;
        public DashboardController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _dashboardService = new DashboardService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// Get total
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotal")]
        public IActionResult GetTotal()
        {
            try
            {
                var res = _dashboardService.GetTotal();
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet("StatisticNumberTicketIn7LastDays")]
        public IActionResult StatisticNumberTicketIn7LastDays()
        {
            try
            {
                var res = _dashboardService.StatisticNumberTicketIn7LastDays();
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
