namespace Library.Model.Models
{
    public class BookQrCode
    {
        public string BookQrCodeId { get; set; }
        public string BookId { get; set; }
        public virtual Book Book { get; set; } //Foreign key
        public byte[] QrImageData { get; set; }
        public string QrImageMimeType { get; set; }
    }
}
