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
    public class TransactionData : ITransactionData
    {
        private readonly string transactionsUrl;
        private readonly string transactionsCollectionName;
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient client;

        public TransactionData(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            transactionsUrl = configuration["TransactionsUrl"];
            transactionsCollectionName = configuration["TransactionsCollectionName"];
            this.httpClientFactory = httpClientFactory;
            client = this.httpClientFactory.CreateClient();
            this.configuration = configuration;
        }

        public async Task<List<Transaction>> ListTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {
//                 string jsonTransactions = @"[
//  { 'sku': 'T2006', 'amount': '10.00', 'currency': 'USD' },
//  { 'sku': 'M2007', 'amount': '34.57', 'currency': 'CAD' },
//  { 'sku': 'R2008', 'amount': '17.95', 'currency': 'USD' },
//  { 'sku': 'T2006', 'amount': '7.63', 'currency': 'EUR' },
//  { 'sku': 'B2009', 'amount': '21.23', 'currency': 'USD' }
// ]";
                string jsonTransactions = await client.GetStringAsync(transactionsUrl);
                transactions = JsonConvert.DeserializeObject<List<Transaction>>(jsonTransactions);
            }
            catch (Exception)
            {
                MongoDal mongoDal = new MongoDal(transactionsCollectionName, configuration);
                transactions = mongoDal.GetList<Transaction>();
            }

            return transactions;
        }

        public void RefreshTransactions(List<Transaction> transactions)
        {
            MongoDal mongoDal = new MongoDal(transactionsCollectionName, configuration);
            mongoDal.DeleteAll<Transaction>();
            mongoDal.AddList<Transaction>(transactions);
        }
    }
}
