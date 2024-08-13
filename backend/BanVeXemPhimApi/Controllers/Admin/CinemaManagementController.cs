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
    [Authorize(Roles = "Manager")]
    public class CinemaManagementController : BaseApiController<CinemaManagementController>
    {
        private readonly CinemaManagementService _cinemaManagementService;
        public CinemaManagementController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _cinemaManagementService = new CinemaManagementService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// Get list cinema
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("Get")]
        public IActionResult Get(int limit, int page, string? name)
        {
            try
            {
                var res = _cinemaManagementService.GetCinemas(limit, page, name);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// get detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetDetail")]
        public IActionResult GetDetail(int id)
        {
            try
            {
                var res = _cinemaManagementService.GetDetail(id);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Store cinema
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Store")]
        public IActionResult Store(CinemaStoreRequest request)
        {
            try
            {
                var res = _cinemaManagementService.Store(request);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Update cinema
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public IActionResult Update(int id, CinemaUpdateRequest request)
        {
            try
            {
                var res = _cinemaManagementService.Update(id, request);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Delete cinema
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var res = _cinemaManagementService.Delete(id);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
