using System;
using LazyBookworm.Common.Enums;

namespace LazyBookworm.Common.Models
{
    public class Book : Item
    {
        public Guid Id { get; set; }
        public Author Author { get; set; }
        public Guid AuthorId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public BookGenre Genre { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Publisher Publisher { get; set; }
    }
}