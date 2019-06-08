using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Model;

namespace Business
{
    public interface ITransactionBusiness
    {
        Task<List<Transaction>> ListTransactions();
    }
}