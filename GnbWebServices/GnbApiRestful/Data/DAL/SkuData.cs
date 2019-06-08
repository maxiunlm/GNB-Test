using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Data.Model;
using Data.Base;

namespace Data
{
    public class SkuData : ISkuData
    {
        public async Task<List<Transaction>> GetTransactionsBySku(string sku)
        {
            MongoDal mongoDal = new MongoDal(TransactionData.transactionsCollectionName);
            List<Transaction> transactions = mongoDal.GetWhere<Transaction>(o => o.Sku == sku).ToList();

            return transactions;
        }

        public List<string> ListSkus()
        {
            MongoDal mongoDal = new MongoDal(TransactionData.transactionsCollectionName);
            List<string> skus = mongoDal.GetQueryable<Transaction>().Select(o => o.Sku).Distinct().OrderBy(o => o).ToList();

            return skus;
        }
    }
}
