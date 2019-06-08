using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Model;

namespace Service
{
    public interface ISkuService
    {
        Task<Sku> GetTransactionsBySku(string sku);
        List<string> ListSkus();
    }
}
