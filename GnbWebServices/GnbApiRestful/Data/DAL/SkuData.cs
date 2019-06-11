using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Data.Model;
using Data.Base;

namespace Data
{
    public class SkuData : ISkuData
    {
        private readonly string transactionsCollectionName;
        private readonly IConfiguration configuration;

        public SkuData(IConfiguration configuration)
        {
            this.configuration = configuration;
            transactionsCollectionName = configuration["TransactionsCollectionName"];
        }

        public List<Transaction> GetTransactionsBySku(string sku)
        {
            MongoDal mongoDal = new MongoDal(transactionsCollectionName, configuration);
            List<Transaction> transactions = mongoDal.GetWhere<Transaction>(o => o.Sku == sku).ToList();

            return transactions;
        }

        public List<string> ListSkus()
        {
            MongoDal mongoDal = new MongoDal(transactionsCollectionName, configuration);
            List<string> skus = mongoDal.GetQueryable<Transaction>().Select(o => o.Sku).Distinct().OrderBy(o => o).ToList();

            return skus;
        }
    }
}
