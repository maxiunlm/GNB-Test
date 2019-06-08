using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Model;

namespace Data
{
    public interface ITransactionData
    {
        Task<List<Transaction>> ListTransactions();
        void RefreshTransactions(List<Transaction> transactions);
    }
}