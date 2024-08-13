using Microsoft.EntityFrameworkCore;
using BanVeXemPhimApi.Models;
using BanVeXemPhimApi.Common;

namespace BanVeXemPhimApi.Seeder
{
    class AdminSeeder
    {
        private readonly ModelBuilder _modelBuilder;
        public AdminSeeder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        /// <summary>
        /// Excute data
        /// </summary>
        public void SeedData()
        {
            _modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    Name = "Admin",
                    Username = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Password = Untill.CreateMD5("admin@gmail.com"),
                    Role = "Manager"
                });
        }
    }
}
