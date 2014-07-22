using System;

namespace Library.Model.Models
{
    public class UserBook
    {
        public string UserBookId { get; set; }
        public string UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; } // Foreign key
        public string BookId { get; set; }
        public virtual Book Book { get; set; } // Foreign key
        public DateTime DateTaken { get; set; }
        public DateTime DateReturned { get; set; }

    }
}
