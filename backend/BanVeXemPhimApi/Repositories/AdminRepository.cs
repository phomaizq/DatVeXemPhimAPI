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
    public class AdminRepository : BaseRespository<Admin>
    {
        private IMapper _mapper;
        public AdminRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }

        /// <summary>
        /// UserLogin function. That return User by user login request
        /// </summary>
        /// <param name="userLoginRequest"></param>
        /// <returns></returns>
        public Admin AdminLogin(AdminLoginRequest userLoginRequest)
        {
            try
            {
                var passwordByMD5 = Untill.CreateMD5(userLoginRequest.Password);
                return Model.Where(row => row.Username == userLoginRequest.Username && row.Password == passwordByMD5).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
