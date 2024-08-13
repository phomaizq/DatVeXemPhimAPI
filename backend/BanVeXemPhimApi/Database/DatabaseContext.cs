using BanVeXemPhimApi.Models;
using BanVeXemPhimApi.Seeder;
using Microsoft.EntityFrameworkCore;

namespace BanVeXemPhimApi.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }


        #region User
        public DbSet<User> users { get; set; }
        #endregion

        #region Admin
        public DbSet<Admin> admins { get; set; }
        #endregion

        #region Movie
        public DbSet<Movie> movies { get; set; }
        #endregion

        #region Cinema
        public DbSet<Cinema> cenimas { get; set; }
        #endregion

        #region Cinema
        public DbSet<Schedule> schedules { get; set; }
        #endregion

        #region ReviewMovie
        public DbSet<ReviewMovie> reviewMovies{ get; set; }
        #endregion

        #region OrderTicket
        public DbSet<OrderTicket> order_tickets{ get; set; }
        #endregion

        public static void UpdateDatabase(DatabaseContext context)
        {
            context.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var sqlConnection = "Server=localhost;Port=3306;Database=website_ban_ve_xem_phim;Uid=root;Pwd=1234$;MaximumPoolSize=500;";
                optionsBuilder.UseMySql(sqlConnection,
                    MySqlServerVersion.LatestSupportedServerVersion);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Movie
            new MovieSeeder(modelBuilder).SeedData();
            #endregion

            #region Admin
            new AdminSeeder(modelBuilder).SeedData();
            #endregion

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}