using System;
using LazyBookworm.Models;

namespace LazyBookworm.Common.Models
{
    public class Item
    {
        
        
        public DateTime ReceivedDate { get; set; } = DateTime.UtcNow;
        public DateTime? LoanDate { get; set; }= null;
        public DateTime? ReturnDate { get; set; }= null;
        public DateTime? ReserveDate { get; set; } = null;
        public Condition Condition { get; set; }
        public string Notes { get; set; } = "";
        public int LoanCount { get; set; } = 0;
    }
}