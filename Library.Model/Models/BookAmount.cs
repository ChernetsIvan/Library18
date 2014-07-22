namespace Library.Model.Models
{
    public class BookAmount
    {
        public string BookAmountId { get; set; }
        public string BookId { get; set; }
        public virtual Book Book { get; set; } //Foreign key
        public int Amount { get; set; }        
    }
}
