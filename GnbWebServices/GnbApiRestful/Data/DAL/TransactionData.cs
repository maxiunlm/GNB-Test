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
    public class TransactionData : ITransactionData
    {
        private const string transactionsUrl = "http://quiet-stone-2094.herokuapp.com/transactions.json";
        public const string transactionsCollectionName = "Transactions";

        private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient client;

        public TransactionData(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            client = this.httpClientFactory.CreateClient();
        }

        public async Task<List<Transaction>> ListTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {
                string jsonTransactions = await client.GetStringAsync(transactionsUrl);
                transactions = JsonConvert.DeserializeObject<List<Transaction>>(jsonTransactions);
            }
            catch (Exception)
            {
                MongoDal mongoDal = new MongoDal(transactionsCollectionName);
                transactions = mongoDal.GetList<Transaction>();
            }

            return transactions;
        }

        public void RefreshTransactions(List<Transaction> transactions)
        {
            MongoDal mongoDal = new MongoDal(transactionsCollectionName);
            mongoDal.DeleteAll<Transaction>();
            mongoDal.AddList<Transaction>(transactions);
        }
    }
}
