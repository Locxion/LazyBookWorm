using System.Collections.Generic;
using LazyBookworm.Common.Models.Common;

namespace LazyBookworm.Common.Models
{
    public class Author : Person
    {
        public Guid Id { get; set; }
        public List<Book> Books { get; set; }
    }
}