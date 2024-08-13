using Microsoft.EntityFrameworkCore;
using BanVeXemPhimApi.Models;
using BanVeXemPhimApi.Common;

namespace BanVeXemPhimApi.Seeder
{
    class UserSeeder
    {
        private readonly ModelBuilder _modelBuilder;
        public UserSeeder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        /// <summary>
        /// Excute data
        /// </summary>
        public void SeedData()
        {
            _modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Nguyễn Văn Test",
                    Username = "nguyenvantest@gmail.com",
                    Password = Untill.CreateMD5("nguyenvantest"),
                });
        }
    }
}
