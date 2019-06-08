using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Model;

namespace Business
{
    public interface ISkuBusiness
    {
        Task<Sku> GetTransactionsBySku(string sku);
        List<string> ListSkus();
    }
}