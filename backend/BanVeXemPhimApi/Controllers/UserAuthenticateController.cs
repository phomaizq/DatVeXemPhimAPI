using AutoMapper;
using AutoMapper.Configuration;
using BanVeXemPhimApi.Common;
using BanVeXemPhimApi.Controllers;
using BanVeXemPhimApi.Database;
using BanVeXemPhimApi.Dto;
using BanVeXemPhimApi.Request;
using BanVeXemPhimApi.Services;
using BanVeXemPhimApi.SocketHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace BanVeXemPhimApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAuthenticateController : BaseApiController<UserAuthenticateController>
    {
        private readonly UserAuthenticateService _userAuthenticateService;
        private readonly ConnectionManager _connectionManager;
        public UserAuthenticateController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, ConnectionManager connectionManager)
        {
            _userAuthenticateService = new UserAuthenticateService(apiConfig, databaseContext, mapper);
            _connectionManager = connectionManager;
        }

        /// <summary>
        /// Get achievement list of user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("UserLogin")]
        public MessageData UserLogin(UserLoginRequest userLoginRequest)
        {
            try
            {
                var res = _userAuthenticateService.UserLogin(userLoginRequest);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message };
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("UserRegister")]
        public MessageData UserRegister(UserRegisterRequest userRegisterRequest)
        {
            try
            {
                var res = _userAuthenticateService.UserRegister(userRegisterRequest);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message };
            }
        }
    }
}
