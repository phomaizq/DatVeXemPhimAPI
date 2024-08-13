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
    public class MovieManagementService
    {
        private readonly MovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public MovieManagementService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _movieRepository = new MovieRepository(apiOption, databaseContext, mapper);
            _mapper = mapper;
            _webHost = webHost;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public object GetMovies(int limit, int page, string? name)
        {
            try
            {
                var query = this._movieRepository.FindAll();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(row => row.Name.ToLower().Contains(name.ToLower()));
                }

                return new Pagination<Movie>(query, limit, page);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public object GetMovieDetail(int movieId)
        {
            try
            {
                return _movieRepository.FindOrFail(movieId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Store movie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Store(MovieStoreRequest request)
        {
            try
            {
                if (request.ImageFile == null)
                {
                    throw new Exception("File can't not null");
                }

                using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\image\\movie\\" + request.ImageFile.FileName))
                {
                    request.ImageFile.CopyTo(fileStream);
                    fileStream.Flush();
                    var newMovie = _mapper.Map<Movie>(request);
                    newMovie.Image = "image/movie/"+ request.ImageFile.FileName;
                    _movieRepository.Create(newMovie);
                    _movieRepository.SaveChange();
                    return newMovie;
                }
                return null;
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
        public object Update(int movieId, MovieUpdateRequest request)
        {
            try
            {
                var movieUpdate = _movieRepository.FindOrFail(movieId);
                if(movieUpdate == null)
                {
                    throw new ValidateError("Id invalid");
                }
                movieUpdate.Name = request.Name;
                movieUpdate.Author = request.Author;
                movieUpdate.Cast = request.Cast;
                movieUpdate.MovieType = request.MovieType;
                movieUpdate.Time = request.Time;
                movieUpdate.ReleaseDate = request.ReleaseDate;
                movieUpdate.Description = request.Description;
                movieUpdate.UpdatedDate = DateTime.Now;

                _movieRepository.UpdateByEntity(movieUpdate);
                _movieRepository.SaveChange();
                return movieUpdate;
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
        public bool Delete(int movieId)
        {
            try
            {
                var movieDelete = _movieRepository.FindOrFail(movieId);
                if (movieDelete == null)
                {
                    throw new ValidateError("Id invalid");
                }
                _movieRepository.DeleteByEntity(movieDelete);
                _movieRepository.SaveChange();

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
