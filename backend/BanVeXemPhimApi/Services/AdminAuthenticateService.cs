using AutoMapper;
using AutoMapper.Configuration;
using BanVeXemPhimApi.Common;
using BanVeXemPhimApi.Database;
using BanVeXemPhimApi.Models;
using BanVeXemPhimApi.Repositories;
using BanVeXemPhimApi.Request;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BanVeXemPhimApi.Services
{
    public class AdminAuthenticateService
    {
        private readonly AdminRepository _adminRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;

        public AdminAuthenticateService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _adminRepository = new AdminRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
        }

        /// <summary>
        /// Admin login
        /// </summary>
        /// <param name="adminLoginRequest"></param>
        /// <returns></returns>
        public object AdminLogin(AdminLoginRequest adminLoginRequest)
        {
            try
            {
                var admin = _adminRepository.AdminLogin(adminLoginRequest);
                if (admin == null)
                {
                    throw new ValidateError("Username or Password incorrect");
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiOption.Secret));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var claimList = new[]
                {
                new Claim(ClaimTypes.Role, admin.Role),
                new Claim(ClaimTypes.Name, admin.Name),
                new Claim(ClaimTypes.UserData, admin.Username),
                new Claim(ClaimTypes.Sid, admin.Id.ToString()),
            };
                var token = new JwtSecurityToken(
                    issuer: _apiOption.ValidIssuer,
                    audience: _apiOption.ValidAudience,
                    expires: DateTime.Now.AddDays(1),
                    claims: claimList,
                    signingCredentials: credentials
                    );
                var tokenByString = new JwtSecurityTokenHandler().WriteToken(token);
                return new
                {
                    token = tokenByString,
                    role = admin.Role,
                    user = admin,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
