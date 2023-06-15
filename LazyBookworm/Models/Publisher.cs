using LazyBookworm.Common.Models.Common;

namespace LazyBookworm.Common.Models
{
    public class Publisher : Firm
    {
        public string PublisherId { get; set; }
    }

    public class Firm
    {
        public Guid Id { get; set; }
        public string FirmName { get; set; }
        public Person Contact { get; set; }
    }
}