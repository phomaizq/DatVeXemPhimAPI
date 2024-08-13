using BanVeXemPhimApi.Common.Enum;
using System;

namespace BanVeXemPhimApi.Models
{
    public class Movie : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Cast { get; set; }
        public int MovieType { get; set; }
        public int Time { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int NumberBooking { get; set; }
        public int NumberView { get; set; }
    }
}
