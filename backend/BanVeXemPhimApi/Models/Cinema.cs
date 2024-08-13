namespace BanVeXemPhimApi.Models
{
    public class Cinema : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
