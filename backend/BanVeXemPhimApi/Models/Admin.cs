﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanVeXemPhimApi.Models
{
    public class Admin : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NumberPhone { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
