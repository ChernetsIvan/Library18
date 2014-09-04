using System.Collections;
using System.Collections.Generic;

namespace Library.Model.Models
{
    public class Author
    {
        public string AuthorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
