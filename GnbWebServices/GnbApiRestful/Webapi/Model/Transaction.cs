namespace Webapi.Model
{
    public class Transaction
    {
        public string Sku { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}