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
    public class AdminAuthenticateController : BaseApiController<ScheduleManagementController>
    {
        private readonly AdminAuthenticateService _adminAuthenticateService;
        public AdminAuthenticateController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _adminAuthenticateService = new AdminAuthenticateService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// Get list schedule
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost("AdminLogin")]
        [AllowAnonymous]
        public IActionResult AdminLogin(AdminLoginRequest request)
        {
            try
            {
                var res = _adminAuthenticateService.AdminLogin(request);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
