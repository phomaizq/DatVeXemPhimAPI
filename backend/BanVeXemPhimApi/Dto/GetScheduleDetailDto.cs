
using System;

namespace BanVeXemPhimApi.Dto
{
    public class GetScheduleDetailDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public DateTime PlaySchedule { get; set; }
        public string PlayScheduleString { get; set; }
        public string SeatHaveBeenBookedList { get; set; }

        //movie
        public string MovieName { get; set; }
        public string Author { get; set; }
        public string Cast { get; set; }
        public int MovieType { get; set; }
        public int Time { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Image { get; set; }
        public string MovieDescription { get; set; }
        public int NumberBooking { get; set; }
        public int NumberView { get; set; }

        //cinema
        public string CinemaName { get; set; }
        public string Address { get; set; }
        public string CinemaDescription { get; set; }
    }
}
