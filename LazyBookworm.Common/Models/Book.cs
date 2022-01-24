using System;
using LazyBookworm.Common.Enums;

namespace LazyBookworm.Common.Models
{
    public class Book : Item
    {
        // ReSharper disable once InconsistentNaming
        public string ISBN { get; set; }

        public string Title { get; set; }
        public BookGenre Genre { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Publisher Publisher { get; set; }
    }
}