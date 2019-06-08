using System.Collections.Generic;

namespace Business.Model
{
    public class Sku
    {
        public string Name { get; set; }
        public decimal Total { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}