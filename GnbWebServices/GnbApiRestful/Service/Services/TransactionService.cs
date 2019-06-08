using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Service.Model;
using AutoMapper;

namespace Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionBusiness business;
        private readonly IMapper mapper;

        public TransactionService(ITransactionBusiness business)
        {
            this.business = business;

            MapperConfiguration automappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Business.Model.Transaction, Transaction>();
                config.CreateMap<Transaction, Business.Model.Transaction>();
            });
            this.mapper = automappingConfiguration.CreateMapper();
        }

        public async Task<List<Transaction>> ListTransactions()
        {
            List<Business.Model.Transaction> originData = await business.ListTransactions();
            List<Transaction> transactions = mapper.Map<List<Transaction>>(originData);

            return transactions;
        }
    }
}
