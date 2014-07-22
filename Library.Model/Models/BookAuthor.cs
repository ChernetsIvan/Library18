namespace Library.Model.Models
{
    public class BookAuthor
    {
        public string BookAuthorId { get; set; }
        public string BookId { get; set; }
        public virtual Book Book { get; set; } //Foreign key
        public string AuthorId { get; set; }        //  !несколько авторов хранятся как несколько записей
        public virtual Author Author { get; set; } //Foreign key
    }
}
