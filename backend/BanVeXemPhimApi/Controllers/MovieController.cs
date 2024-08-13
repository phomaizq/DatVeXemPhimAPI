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

namespace BanVeXemPhimApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : BaseApiController<MovieController>
    {
        private readonly MovieService _movieService;
        public MovieController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _movieService = new MovieService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// Get movie list of user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetMovieSuggest")]
        public MessageData GetMovieSuggest(string sugguestType)
        {
            try
            {
                var res = _movieService.GetMovieSuggest(sugguestType);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message };
            }
        }

        /// <summary>
        /// Get achievement list of user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetMovieDetail")]
        public MessageData GetMovieDetail(int movieId)
        {
            try
            {
                var res = _movieService.GetMovieDetail(movieId);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message };
            }
        }

        /// <summary>
        /// Get movie list by category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetMovieByCategory")]
        public MessageData GetMovieByCategory(string category)
        {
            try
            {
                var res = _movieService.GetMovieByCategory(category);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message };
            }
        }

        /// <summary>
        /// User search movie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("UserSearchMovie")]
        public MessageData UserSearchMovie(string name)
        {
            try
            {
                var res = _movieService.UserSearchMovie(name);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message };
            }
        }

        /// <summary>
        /// Review movie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReviewMovie")]
        public MessageData UserReviewMovie(ReviewMovieRequest request)
        {
            try
            {
                var res = _movieService.UserReviewMovie(UserId, request);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message };
            }
        }
    }
}
