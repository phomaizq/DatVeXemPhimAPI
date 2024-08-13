using Microsoft.EntityFrameworkCore;
using BanVeXemPhimApi.Models;
using BanVeXemPhimApi.Common;
using BanVeXemPhimApi.Common.Enum;
using System;

namespace BanVeXemPhimApi.Seeder
{
    class MovieSeeder
    {
        private readonly ModelBuilder _modelBuilder;
        public MovieSeeder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        /// <summary>
        /// Excute data
        /// </summary>
        public void SeedData()
        {
            _modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Name = "ONE PIECE FILM RED",
                    Author = "Gorō Taniguchi",
                    Cast = "Mayumi Tanaka (Luffy), Shuichi Ikeda (Shanks), Hiroaki Hirata (Sanji), Ikue Otani (Tony Tony Chopper)",
                    MovieType = 1,
                    Time = 115,
                    ReleaseDate = DateTime.Now,
                    Image = "image/movie/movie_1.jpg",
                    Description = "Bối cảnh One Piece Film Red diễn ra ở hòn đảo âm nhạc Elegia, nơi diva nổi tiếng bậc nhất thế giới tên Uta thực hiện buổi biểu diễn trực tiếp đầu tiên trước công chúng. Uta đứng trên sân khấu với một ước mơ giản dị rằng ” Âm nhạc của tôi sẽ khiến cho thế giới hạnh phúc”. Đằng sau hình ảnh cô ca sĩ sở hữu giọng hát được đánh giá là ở “Đẳng cấp hoàn toàn khác” là một thân thế vô cùng bí ẩn được che giấu. Băng hải tặc Mũ Rơm và các fan khác của Uta từ nhiều thế lực khác nhau như hải tặc lẫn hải quân đều đã cùng tề tựu về buổi biểu diễn này. Biến cố bắt đầu ngay khi sự thật kinh hoàng được tiết lộ rằng Uta chính là “con gái của Shanks”. Luffy và Uta lần đầu tiên hội ngộ sau lần gặp gỡ vào 12 năm trước tại Làng Foosha."
                },
                new Movie
                {
                    Id = 2,
                    Name = "CHIẾN BINH BÁO ĐEN 2: WAKANDA BẤT DIỆT",
                    Author = "Ryan Coogler",
                    Cast = "Tenoch Huerta, Martin Freeman, Lupita Nyong'o",
                    MovieType = 1,
                    Time = 115,
                    ReleaseDate = DateTime.Now,
                    Image = "image/movie/movie_2.jpg",
                    Description = "Bối cảnh One Piece Film Red diễn ra ở hòn đảo âm nhạc Elegia, nơi diva nổi tiếng bậc nhất thế giới tên Uta thực hiện buổi biểu diễn trực tiếp đầu tiên trước công chúng. Uta đứng trên sân khấu với một ước mơ giản dị rằng ” Âm nhạc của tôi sẽ khiến cho thế giới hạnh phúc”. Đằng sau hình ảnh cô ca sĩ sở hữu giọng hát được đánh giá là ở “Đẳng cấp hoàn toàn khác” là một thân thế vô cùng bí ẩn được che giấu. Băng hải tặc Mũ Rơm và các fan khác của Uta từ nhiều thế lực khác nhau như hải tặc lẫn hải quân đều đã cùng tề tựu về buổi biểu diễn này. Biến cố bắt đầu ngay khi sự thật kinh hoàng được tiết lộ rằng Uta chính là “con gái của Shanks”. Luffy và Uta lần đầu tiên hội ngộ sau lần gặp gỡ vào 12 năm trước tại Làng Foosha."
                },
                new Movie
                {
                    Id = 3,
                    Name = "CHÚ NGUYỀN TÁI SINH",
                    Author = "James Lee",
                    Cast = "Alif Satar, Ikmal Amry, Abbas Mahmood",
                    MovieType = 4,
                    Time = 115,
                    ReleaseDate = DateTime.Now,
                    Image = "image/movie/movie_3.jpg",
                    Description = "Bối cảnh One Piece Film Red diễn ra ở hòn đảo âm nhạc Elegia, nơi diva nổi tiếng bậc nhất thế giới tên Uta thực hiện buổi biểu diễn trực tiếp đầu tiên trước công chúng. Uta đứng trên sân khấu với một ước mơ giản dị rằng ” Âm nhạc của tôi sẽ khiến cho thế giới hạnh phúc”. Đằng sau hình ảnh cô ca sĩ sở hữu giọng hát được đánh giá là ở “Đẳng cấp hoàn toàn khác” là một thân thế vô cùng bí ẩn được che giấu. Băng hải tặc Mũ Rơm và các fan khác của Uta từ nhiều thế lực khác nhau như hải tặc lẫn hải quân đều đã cùng tề tựu về buổi biểu diễn này. Biến cố bắt đầu ngay khi sự thật kinh hoàng được tiết lộ rằng Uta chính là “con gái của Shanks”. Luffy và Uta lần đầu tiên hội ngộ sau lần gặp gỡ vào 12 năm trước tại Làng Foosha."
                },
                new Movie
                {
                    Id = 4,
                    Name = "HẠNH PHÚC MÁU",
                    Author = "Nguyễn Chung",
                    Cast = "NSND Kim Xuân, NSƯT Lê Thiện, NSƯT Công Ninh, Dược Sĩ Tiến, Phạm Huỳnh Hữu Tài",
                    MovieType = 4,
                    Time = 115,
                    ReleaseDate = DateTime.Now,
                    Image = "image/movie/movie_4.jpg",
                    Description = "Bối cảnh One Piece Film Red diễn ra ở hòn đảo âm nhạc Elegia, nơi diva nổi tiếng bậc nhất thế giới tên Uta thực hiện buổi biểu diễn trực tiếp đầu tiên trước công chúng. Uta đứng trên sân khấu với một ước mơ giản dị rằng ” Âm nhạc của tôi sẽ khiến cho thế giới hạnh phúc”. Đằng sau hình ảnh cô ca sĩ sở hữu giọng hát được đánh giá là ở “Đẳng cấp hoàn toàn khác” là một thân thế vô cùng bí ẩn được che giấu. Băng hải tặc Mũ Rơm và các fan khác của Uta từ nhiều thế lực khác nhau như hải tặc lẫn hải quân đều đã cùng tề tựu về buổi biểu diễn này. Biến cố bắt đầu ngay khi sự thật kinh hoàng được tiết lộ rằng Uta chính là “con gái của Shanks”. Luffy và Uta lần đầu tiên hội ngộ sau lần gặp gỡ vào 12 năm trước tại Làng Foosha."
                },
                new Movie
                {
                    Id = 5,
                    Name = "KHỈ CON LON TON THẾ GIỚI",
                    Author = "Samuel Tourneux",
                    Cast = "",
                    MovieType = 3,
                    Time = 115,
                    ReleaseDate = DateTime.Now,
                    Image = "image/movie/movie_5.jpg",
                    Description = "Bối cảnh One Piece Film Red diễn ra ở hòn đảo âm nhạc Elegia, nơi diva nổi tiếng bậc nhất thế giới tên Uta thực hiện buổi biểu diễn trực tiếp đầu tiên trước công chúng. Uta đứng trên sân khấu với một ước mơ giản dị rằng ” Âm nhạc của tôi sẽ khiến cho thế giới hạnh phúc”. Đằng sau hình ảnh cô ca sĩ sở hữu giọng hát được đánh giá là ở “Đẳng cấp hoàn toàn khác” là một thân thế vô cùng bí ẩn được che giấu. Băng hải tặc Mũ Rơm và các fan khác của Uta từ nhiều thế lực khác nhau như hải tặc lẫn hải quân đều đã cùng tề tựu về buổi biểu diễn này. Biến cố bắt đầu ngay khi sự thật kinh hoàng được tiết lộ rằng Uta chính là “con gái của Shanks”. Luffy và Uta lần đầu tiên hội ngộ sau lần gặp gỡ vào 12 năm trước tại Làng Foosha."
                });
        }
    }
}
