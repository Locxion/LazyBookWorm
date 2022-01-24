namespace LazyBookworm.Models.Common
{
    public class Settings
    {
        public string DatabaseHost { get; set; } = "localhost";
        public string DatabaseName { get; set; }

        public string DatabaseUser { get; set; }
        public string DatabasePassword { get; set; }
    }
}