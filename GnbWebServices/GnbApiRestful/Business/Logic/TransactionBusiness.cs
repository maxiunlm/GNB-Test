using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Business.Model;
using AutoMapper;

namespace Business
{
    public class TransactionBusiness : ITransactionBusiness
    {
        private readonly ITransactionData data;
        private readonly IMapper mapper;

        public TransactionBusiness(ITransactionData data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<List<Transaction>> ListTransactions()
        {
            List<Data.Model.Transaction> originData = await data.ListTransactions();
            data.RefreshTransactions(originData);

            List<Transaction> transactions = mapper.Map<List<Transaction>>(originData);
            transactions = transactions.OrderBy(o => o.Sku).ThenBy(o => o.Currency).ToList();

            return transactions;
        }
    }
}
