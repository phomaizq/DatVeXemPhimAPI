using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BanVeXemPhimApi.Request
{
    public class MovieStoreRequest
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Cast { get; set; }
        public int MovieType { get; set; }
        public int Time { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
