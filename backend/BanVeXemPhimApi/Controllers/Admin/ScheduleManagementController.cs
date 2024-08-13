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
    public class ScheduleManagementController : BaseApiController<ScheduleManagementController>
    {
        private readonly ScheduleManagementService _scheduleManagementService;
        public ScheduleManagementController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _scheduleManagementService = new ScheduleManagementService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// Get list schedule
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("Get")]
        public IActionResult Get(int limit, int page)
        {
            try
            {
                var res = _scheduleManagementService.GetSchedules(limit, page);
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
                var res = _scheduleManagementService.GetDetail(id);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Store schedule
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Store")]
        public IActionResult Store(ScheduleStoreRequest request)
        {
            try
            {
                var res = _scheduleManagementService.Store(request);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Update schedule
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public IActionResult Update(int id, ScheduleUpdateRequest request)
        {
            try
            {
                var res = _scheduleManagementService.Update(id, request);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Delete schedule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var res = _scheduleManagementService.Delete(id);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
