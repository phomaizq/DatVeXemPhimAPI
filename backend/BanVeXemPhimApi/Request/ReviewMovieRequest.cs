using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BanVeXemPhimApi.Request
{
    public class ReviewMovieRequest
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
