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
using BanVeXemPhimApi.Dto.Admin;

namespace BanVeXemPhimApi.Services
{
    public class ScheduleManagementService
    {
        private readonly ScheduleRepository _scheduleRepository;
        private readonly MovieRepository _movieRepository;
        private readonly CinemaRepository _cinemaRepository;
        private readonly IMapper _mapper;
        public ScheduleManagementService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _scheduleRepository = new ScheduleRepository(apiOption, databaseContext, mapper);
            _movieRepository = new MovieRepository(apiOption, databaseContext, mapper);
            _cinemaRepository = new CinemaRepository(apiOption, databaseContext, mapper);
            _mapper = mapper;
        }

        /// <summary>
        /// Get schedule
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public object GetSchedules(int limit, int page)
        {
            try
            {
                var query = this._scheduleRepository.FindAll();
                var paginationSchedule = new Pagination<Schedule>(query, limit, page);
                var scheduleList = paginationSchedule.Data;
                
                var getScheduleDtoList = scheduleList.Select(row => _mapper.Map<GetScheduleDto>(row)).ToList();
                var movieIdList = getScheduleDtoList.Select(row => row.MovieId).ToList();
                var cinemaIdList = getScheduleDtoList.Select(row => row.CinemaId).ToList();

                var movieList = _movieRepository.FindByCondition(row => movieIdList.Contains(row.Id)).ToList();
                var cinemaList = _cinemaRepository.FindByCondition(row => cinemaIdList.Contains(row.Id)).ToList();

                foreach( var item in getScheduleDtoList )
                {
                    var movie = movieList.Where(row => row.Id == item.MovieId).FirstOrDefault();
                    if ( movie != null )
                    {
                        item.MovieName = movie.Name;
                    }

                    var cinema = cinemaList.Where(row => row.Id == item.CinemaId).FirstOrDefault();
                    if (cinema != null)
                    {
                        item.CinemaName = cinema.Name;
                    }
                }

                return new Pagination<GetScheduleDto>(getScheduleDtoList, paginationSchedule.TotalPages, paginationSchedule.TotalRecords, limit, page);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get schedule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object GetDetail(int id)
        {
            try
            {
                return _scheduleRepository.FindOrFail(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Store movie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Store(ScheduleStoreRequest request)
        {
            try
            {
                if(request.PlaySchedule < DateTime.Now)
                {
                    throw new Exception("Play Schedule invalid!");
                }
                var movie = _movieRepository.FindOrFail(request.MovieId);
                if (movie == null)
                {
                    throw new Exception("MovieId does not exist!");
                }

                var cinema = _cinemaRepository.FindOrFail(request.CinemaId);
                if (cinema == null)
                {
                    throw new Exception("CinemaId does not exist!");
                }

                var newSchedule = _mapper.Map<Schedule>(request);
                newSchedule.SeatHaveBeenBookedList = "";
                _scheduleRepository.Create(newSchedule);
                _scheduleRepository.SaveChange();
                return newSchedule;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Update(int scheduleId, ScheduleUpdateRequest request)
        {
            try
            {
                var scheduleUpdate = _scheduleRepository.FindOrFail(scheduleId);
                if(scheduleUpdate == null)
                {
                    throw new ValidateError("Schedule id invalid!");
                }
                scheduleUpdate.MovieId = request.MovieId;
                scheduleUpdate.CinemaId = request.CinemaId;
                scheduleUpdate.PlaySchedule= request.PlaySchedule;
                scheduleUpdate.UpdatedDate = DateTime.Now;

                _scheduleRepository.UpdateByEntity(scheduleUpdate);
                _scheduleRepository.SaveChange();
                return scheduleUpdate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public bool Delete(int scheduleId)
        {
            try
            {
                var scheduleDelete = _scheduleRepository.FindOrFail(scheduleId);
                if (scheduleDelete == null)
                {
                    throw new ValidateError("Id invalid");
                }
                _scheduleRepository.DeleteByEntity(scheduleDelete);
                _scheduleRepository.SaveChange();

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
