using System.Collections;
using System.Collections.Generic;

namespace Library.Model.Models
{
    public class Book
    {
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public int PagesAmount { get; set; }
        public string PublishingHouse { get; set; }
        public string BookQrCodeId { get; set; }
        public virtual BookQrCode BookQrCode { get; set; } //Foreign key
        public virtual ICollection<Author> Authors { get; set; }
        public virtual BookAmount BookAmount { get; set; }
    }
}
