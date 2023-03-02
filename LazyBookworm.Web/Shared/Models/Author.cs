using System.Collections.Generic;
using LazyBookworm.Common.Models.Common;

namespace LazyBookworm.Common.Models
{
    public class Author : Person
    {
        public ICollection<Book> Books { get; set; }
    }
}