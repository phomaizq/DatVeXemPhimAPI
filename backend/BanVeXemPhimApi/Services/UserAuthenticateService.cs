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
    public class UserAuthenticateService
    {
        private readonly UserRepository _userRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;

        public UserAuthenticateService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="userLoginRequest"></param>
        /// <returns></returns>
        public object UserLogin(UserLoginRequest userLoginRequest)
        {
            try
            {
                var user = _userRepository.UserLogin(userLoginRequest);
                if(user == null)
                {
                    throw new Exception("Username or Password incorrect");
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiOption.Secret));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var claimList = new[]
                {
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.UserData, user.Username),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
            };
                var token = new JwtSecurityToken(
                    issuer: _apiOption.ValidIssuer,
                    audience: _apiOption.ValidAudience,
                    expires: DateTime.Now.AddDays(1),
                    claims: claimList,
                    signingCredentials: credentials
                    );
                var tokenByString = new JwtSecurityTokenHandler().WriteToken(token);
                return new  { 
                    token = tokenByString,
                    role = "User",
                    user = user,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// user register account
        /// </summary>
        /// <param name="userRegisterRequest"></param>
        /// <returns></returns>
        public object UserRegister(UserRegisterRequest userRegisterRequest)
        {
            try
            {
                var user = _mapper.Map<User>(userRegisterRequest);
                user.Username = user.Email;
                user.Password = Untill.CreateMD5(userRegisterRequest.Password);

                var resultCheck = _userRepository.CheckUserRegister(user);
                if (!resultCheck)
                {
                    throw new Exception("Username or Numberphone does exist!");
                }

                // create user
                _userRepository.Create(user);
                _userRepository.SaveChange();

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiOption.Secret));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var claimList = new[]
                {
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.UserData, user.Username),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
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
                    role = "User",
                    user = user,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
