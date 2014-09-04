using System.Collections.Generic;

namespace Library.Domain.Models
{
    public class AuthorModel
    {
        public string AuthorId { get; set; }
        public string Name { get; set; }
        List<BookModel> Books { get; set; }
    }
}