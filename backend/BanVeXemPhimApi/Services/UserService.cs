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

namespace BanVeXemPhimApi.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IWebHostEnvironment _webHost;
        public UserService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _webHost = webHost;
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <returns></returns>
        public object GetUsers(int limit, int page, string? name)
        {
            try
            {
                var query = this._userRepository.FindAll();
                if (name != null)
                {
                    query = query.Where(row => row.Name.Contains(name));
                }
                query.Skip((page - 1) * limit).Take(limit);
                var total = query.Count();
                int tmpByInt = total / limit;
                double tmpByDouble = (double)total / (double)limit;
                int totalPage = 1;
                if(tmpByDouble > (double)tmpByInt)
                {
                    totalPage = tmpByInt + 1;
                }
                else
                {
                    totalPage = tmpByInt;
                }
                query = query.Skip((page - 1) * limit).Take(limit);
                var amount = query.Count();
                return new
                {
                    data = query.ToList(),
                    Amount = amount,
                    PageSize = limit,
                    Total = total,
                    TotalPage = totalPage,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
