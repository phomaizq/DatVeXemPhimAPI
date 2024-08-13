using System;

namespace BanVeXemPhimApi.Models
{
    public class Schedule : BaseModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public DateTime PlaySchedule { get; set; }
        public string SeatHaveBeenBookedList { get; set; }
    }
}
