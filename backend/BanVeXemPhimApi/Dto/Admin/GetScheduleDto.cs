using BanVeXemPhimApi.Models;

namespace BanVeXemPhimApi.Dto.Admin
{
    public class GetScheduleDto : Schedule
    {
        public string MovieName { get; set; }
        public string CinemaName { get; set; }
    }
}
