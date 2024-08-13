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

namespace BanVeXemPhimApi.Services
{
    public class MovieService
    {
        private readonly MovieRepository _movieRepository;
        private readonly ReviewMovieRepository _reviewMovieRepository;
        public MovieService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper)
        {
            _movieRepository = new MovieRepository(apiOption, databaseContext, mapper);
            _reviewMovieRepository = new ReviewMovieRepository(apiOption, databaseContext, mapper);
        }

        /// <summary>
        /// Get movie suggest
        /// </summary>
        /// <returns></returns>
        public object GetMovieSuggest(string suggestType)
        {
            try
            {
                var query = _movieRepository.FindAll();
                if(suggestType == "Hot")
                {
                    return query.OrderByDescending(row => row.NumberBooking).Take(8).ToList();
                }
                else if(suggestType == "Arrival")
                {
                    return query.OrderByDescending(row => row.NumberView).Take(8).ToList();
                }
                else
                {
                    return query.OrderByDescending(row => row.Id).Take(4).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get movie detail
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public Movie GetMovieDetail(int movieId)
        {
            try
            {
                var movie = _movieRepository.FindOrFail(movieId);
                if(movie == null)
                {
                    throw new Exception("Phim không tồn tại!");
                }
                return movie;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get movie list by category
        /// </summary>
        /// <returns></returns>
        public object GetMovieByCategory(string category)
        {
            try
            {
                var query = _movieRepository.FindAll();
                if (category == "Playing")
                {
                    return query.OrderByDescending(row => row.NumberBooking).Take(8).ToList();
                }
                else if (category == "Coming soon")
                {
                    return query.OrderByDescending(row => row.NumberView).Take(8).ToList();
                }
                else
                {
                    return query.OrderByDescending(row => row.Id).Take(4).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// User search movie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object UserSearchMovie(string? name)
        {
            try
            {
                var query = _movieRepository.FindAll();
                if (!string.IsNullOrEmpty(name))
                {
                    return query.Where(row => row.Name.Contains(name) || name.Contains(row.Name)).ToList();
                }
                return new List<Movie>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Review movie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object UserReviewMovie(int userId, ReviewMovieRequest request)
        {
            try
            {
                var movie = _movieRepository.FindOrFail(request.MovieId);
                if(movie == null)
                {
                    throw new Exception("Movie does not exits!");
                }

                var newReviewMovie = new ReviewMovie()
                {
                    MovieId = request.MovieId,
                    Content = request.Content,
                };
                _reviewMovieRepository.Create(newReviewMovie);
                _reviewMovieRepository.SaveChange();
                return newReviewMovie;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
