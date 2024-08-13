using AutoMapper;
using BanVeXemPhimApi.Dto;
using BanVeXemPhimApi.Dto.Admin;
using BanVeXemPhimApi.Models;
using BanVeXemPhimApi.Request;

namespace BanVeXemPhimApi.Mapper
{
    public class MappingContext : Profile
    {
        public MappingContext()
        {
            // user request
            CreateMap<UserRegisterRequest, User>();

            // schedule
            CreateMap<Schedule, ScheduleDto>();
            CreateMap<Schedule, GetScheduleDetailDto>();
            CreateMap<ScheduleStoreRequest, Schedule>();
            CreateMap<Schedule, GetScheduleDto>();

            // Movie
            CreateMap<MovieStoreRequest, Movie>();
            CreateMap<Movie, MovieWithScheduleDto>();

            // Cinema
            CreateMap<CinemaStoreRequest, Cinema>();

            // OrderTicket
            CreateMap<BookTicketRequest, OrderTicket>();
            CreateMap<OrderTicket, OrderHistoryDto>();

            // Admin
            CreateMap<EmployeeStoreRequest, Admin>();
        }
    }
}
