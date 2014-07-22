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
        public byte[] QrImageData { get; set; }
        public string QrImageMimeType { get; set; }
    }
}
