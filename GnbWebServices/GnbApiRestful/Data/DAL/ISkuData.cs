using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Model;

namespace Data
{
    public interface ISkuData
    {
        Task<List<Transaction>> GetTransactionsBySku(string sku);
        List<string> ListSkus();
    }
}
