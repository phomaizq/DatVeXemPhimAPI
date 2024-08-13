using System;

namespace BanVeXemPhimApi.Models
{
    public class ReviewMovie : BaseModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Content{ get; set; }
    }
}
