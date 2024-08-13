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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BanVeXemPhimApi.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Manager")]
    public class UserManagementController : BaseApiController<UserManagementController>
    {
        private readonly UserManagementService _userManagementService;
        public UserManagementController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _userManagementService = new UserManagementService(apiConfig, databaseContext, mapper, webHost);
        }

        /// <summary>
        /// get user list
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("Get")]
        public IActionResult Get(int limit, int page, string? name, string? email)
        {
            try
            {
                var res = _userManagementService.GetUsers(limit, page, name, email);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// get user detail
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetUserDetail")]
        public IActionResult GetUserDetail(int userId)
        {
            try
            {
                var res = _userManagementService.GetUserDetail(userId);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
