using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BanVeXemPhimApi.Request
{
    public class UserRegisterRequest
    {
        [Required]
        public string Name{ get; set; }
        [Required]
        public string NumberPhone { get; set; }
        [Required]
        public string Address{ get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}
