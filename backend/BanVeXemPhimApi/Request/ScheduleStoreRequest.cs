using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BanVeXemPhimApi.Request
{
    public class ScheduleStoreRequest
    {
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public DateTime PlaySchedule { get; set; }
    }
}
