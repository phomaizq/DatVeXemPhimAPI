using System;

namespace BanVeXemPhimApi.Models
{
    public class BaseModel
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
    }
}
