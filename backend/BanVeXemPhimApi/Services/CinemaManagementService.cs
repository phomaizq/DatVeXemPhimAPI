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

namespace BanVeXemPhimApi.Services
{
    public class CinemaManagementService
    {
        private readonly CinemaRepository _cinemaRepository;
        private readonly IMapper _mapper;
        public CinemaManagementService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _cinemaRepository = new CinemaRepository(apiOption, databaseContext, mapper);
            _mapper = mapper;
        }

        /// <summary>
        /// Get schedule
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public object GetCinemas(int limit, int page, string? name)
        {
            try
            {
                var query = this._cinemaRepository.FindAll();
                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(row => row.Name.ToLower().Contains(name.ToLower()));
                }
                return new Pagination<Cinema>(query, limit, page);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Store cinema
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Store(CinemaStoreRequest request)
        {
            try
            {
                var newCinema = _mapper.Map<Cinema>(request);
                _cinemaRepository.Create(newCinema);
                _cinemaRepository.SaveChange();
                return newCinema;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object GetDetail(int id)
        {
            try
            {
                return _cinemaRepository.FindOrFail(id);
            }
            catch (Exception ex)
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
        public object Update(int cinemaId, CinemaUpdateRequest request)
        {
            try
            {
                var cinemaUpdate = _cinemaRepository.FindOrFail(cinemaId);
                if(cinemaUpdate == null)
                {
                    throw new ValidateError("Cinema id invalid!");
                }
                cinemaUpdate.Name = request.Name;
                cinemaUpdate.Address = request.Address;
                cinemaUpdate.Description = request.Description;
                cinemaUpdate.UpdatedDate = DateTime.Now;

                _cinemaRepository.UpdateByEntity(cinemaUpdate);
                _cinemaRepository.SaveChange();
                return cinemaUpdate;
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
        public bool Delete(int cinemaId)
        {
            try
            {
                var cinemaDelete = _cinemaRepository.FindOrFail(cinemaId);
                if (cinemaDelete == null)
                {
                    throw new ValidateError("Cinema Id invalid");
                }
                _cinemaRepository.DeleteByEntity(cinemaDelete);
                _cinemaRepository.SaveChange();

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
