using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Data;
using Business.Model;
using AutoMapper;

namespace Business
{
    public class SkuBusiness : ISkuBusiness
    {
        private const int digits = 2;
        private readonly string defualtCurrency;
        private readonly ISkuData data;
        private readonly IRateBusiness rateBusiness;
        private readonly IMapper mapper;

        public SkuBusiness(IRateBusiness rateBusiness, ISkuData data, IConfiguration configuration)
        {
            this.data = data;
            this.rateBusiness = rateBusiness;
            defualtCurrency = configuration["DefualtCurrency"];

            MapperConfiguration automappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Data.Model.CurrencyConvertion, CurrencyConvertion>();
                config.CreateMap<CurrencyConvertion, Data.Model.CurrencyConvertion>();
                config.CreateMap<Data.Model.Transaction, Transaction>();
                config.CreateMap<Transaction, Data.Model.Transaction>();
            });
            this.mapper = automappingConfiguration.CreateMapper();
        }

        public List<string> ListSkus()
        {
            List<string> skus = data.ListSkus();

            return skus;
        }

        public async Task<Sku> GetTransactionsBySku(string sku)
        {
            List<CurrencyConvertion> Rates = await rateBusiness.ListRates();
            List<Data.Model.Transaction> originData = await data.GetTransactionsBySku(sku);
            List<Transaction> transactions = mapper.Map<List<Transaction>>(originData);
            PassTransactionsToDefualtCurrency(Rates, transactions);
            transactions = transactions.OrderBy(o => o.Currency).ToList();
            decimal total = transactions.Sum(o => o.Amount);
            total = RoundBank(total);

            Sku skuModel = new Sku
            {
                Name = sku,
                Transactions = transactions,
                Total = total
            };

            return skuModel;
        }

        private void PassTransactionsToDefualtCurrency(List<CurrencyConvertion> Rates, List<Transaction> transactions)
        {
            transactions.ForEach(t =>
            {
                if (t.Currency != defualtCurrency)
                {
                    CurrencyConvertion Rate = Rates.SingleOrDefault(c =>
                        c.From == t.Currency
                        && c.To == defualtCurrency);

                    if (Rate != null)
                    {
                        t.Currency = defualtCurrency;
                        t.Amount *= Rate.Rate;
                        t.Amount = RoundBank(t.Amount);
                    }
                }
            });
        }

        private decimal RoundBank(decimal amount)
        {
            return Math.Round(amount, digits, MidpointRounding.AwayFromZero);
        }
    }
}
