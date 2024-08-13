using System;

namespace BanVeXemPhimApi.Models
{
    public class OrderTicket : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
        public string Name { get; set; }
        public string NumberPhone { get; set; }
        public string Email{ get; set; }
        public string SeatList { get; set; }
        public double TotalPrice { get; set; }
    }
}
