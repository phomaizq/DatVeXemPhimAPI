
using System;

namespace BanVeXemPhimApi.Dto
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public DateTime PlaySchedule { get; set; }
        public string PlayScheduleString { get; set; }
        public string TimePlayString { get; set; }
        public bool IsCanBook{ get; set; }
    }
}
