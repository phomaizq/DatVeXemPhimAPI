using System;

namespace BanVeXemPhimApi.Dto
{
    public class OrderHistoryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
        public string Name { get; set; }
        public string NumberPhone { get; set; }
        public string Email { get; set; }
        public string SeatList { get; set; }
        public double TotalPrice { get; set; }
        public bool IsPaid { get; set; }
        public string OrderDateString { get; set; }
        public string PaymentCodeImage { get; set; }
        public DateTime CreatedDate { get; set; }

        //movie
        public string MovieName { get; set; }
        public string Image { get; set; }

        //Schedule
        public string PlayScheduleString { get; set; }
    }
}
