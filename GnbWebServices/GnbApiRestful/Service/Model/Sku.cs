using System.Collections.Generic;

namespace Service.Model
{
    public class Sku
    {
        public string Name { get; set; }
        public decimal Total { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}