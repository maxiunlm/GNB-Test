using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Model;

namespace Service
{
    public interface ITransactionService
    {
        Task<List<Transaction>> ListTransactions();
    }
}