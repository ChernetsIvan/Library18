using System.Collections.Generic;

namespace Library.Core.Models
{
    public class BookModel
    {
        public string BookId { get; set; }
        public List<AuthorModel> Authors { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public int PagesAmount { get; set; }
        public string PublishingHouse { get; set; }
        public int BookAmount { get; set; }
    }
}