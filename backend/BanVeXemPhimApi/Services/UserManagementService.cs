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
    public class UserManagementService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public UserManagementService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _mapper = mapper;
            _webHost = webHost;
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public object GetUsers(int limit, int page, string? name, string? email)
        {
            try
            {
                var query = this._userRepository.FindAll();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(row => row.Name.ToLower().Contains(name.ToLower()));
                }

                if (!string.IsNullOrEmpty(email))
                {
                    query = query.Where(row => row.Email.ToLower().Contains(email.ToLower()));
                }

                return new Pagination<User>(query, limit, page);
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
        public object GetUserDetail(int userId)
        {
            try
            {
                return _userRepository.FindOrFail(userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
