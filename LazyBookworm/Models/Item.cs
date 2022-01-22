using System;
using System.Collections.Generic;
using System.Text;

namespace LazyBookworm.Models
{
    public class Item
    {
        public DateTime ReceivedDate { get; set; } = DateTime.UtcNow;
        public Condition Condition { get; set; }
        public string Notes { get; set; } = "";
        public int LoanCount { get; set; } = 0;
    }

    public class Book
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Publisher { get; set; }
    }
}