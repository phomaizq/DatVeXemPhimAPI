using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BanVeXemPhimApi.Request
{
    public class BookTicketRequest
    {
        public int ScheduleId { get; set; }
        public string NumberPhone { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string SeatList { get; set; }
    }
}
