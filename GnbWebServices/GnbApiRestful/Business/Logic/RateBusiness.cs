using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Business.Model;
using AutoMapper;

namespace Business
{
    public class RateBusiness : IRateBusiness
    {
        private readonly IRateData data;
        private readonly IMapper mapper;

        public RateBusiness(IRateData data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<List<CurrencyConvertion>> ListRates()
        {
            List<Data.Model.CurrencyConvertion> originData = await data.ListRates();
            List<CurrencyConvertion> rates = MapRates(originData);
            FillMissingRates(rates);
            rates = SortRates(rates);
            InsertOrUpdateRates(rates);

            return rates;
        }

        private List<CurrencyConvertion> SortRates(List<CurrencyConvertion> rates)
        {
            return rates.OrderBy(o => o.From).ThenBy(o => o.To).ToList();
        }

        private List<CurrencyConvertion> MapRates(List<Data.Model.CurrencyConvertion> originData)
        {
            List<CurrencyConvertion> rates = mapper.Map<List<CurrencyConvertion>>(originData);

            return rates;
        }

        private void InsertOrUpdateRates(List<CurrencyConvertion> rates)
        {
            List<Data.Model.CurrencyConvertion> newRates = new List<Data.Model.CurrencyConvertion>();
            rates.ForEach(o => newRates.Add(mapper.Map<Data.Model.CurrencyConvertion>(o)));
            data.InsertOrUpdateRates(newRates);
        }

        private void FillMissingRates(List<CurrencyConvertion> rates)
        {
            List<string> froms = rates.Select(o => o.From).Distinct().ToList();
            List<string> tos = rates.Select(o => o.To).Distinct().ToList();

            foreach (string from in froms)
            {
                foreach (string to in tos)
                {
                    if (from == to)
                    {
                        continue;
                    }

                    CurrencyConvertion rate = rates.SingleOrDefault(o =>
                        o.From == from
                        && o.To == to);

                    if (rate == null)
                    {
                        FillMissingRate(rates, from, to);
                    }
                }
            }
        }

        private void FillMissingRate(List<CurrencyConvertion> rates, string from, string to)
        {
            List<CurrencyConvertion> partielFroms = rates.Where(o => o.From == from).ToList();
            List<string> tos = partielFroms.Select(o => o.To).Distinct().ToList();
            List<CurrencyConvertion> nexusFroms = rates.Where(o => tos.Contains(o.From)).ToList();
            CurrencyConvertion nexusTo = nexusFroms.Where(o => o.To == to).FirstOrDefault();

            if (nexusTo != null)
            {
                AddMissingRate(rates, from, to, partielFroms, nexusTo);
            }
        }

        private void AddMissingRate(
            List<CurrencyConvertion> rates,
            string from,
            string to,
            List<CurrencyConvertion> partielFroms,
            CurrencyConvertion nexusTo)
        {
            CurrencyConvertion nexusFrom = partielFroms.Where(o => o.To == nexusTo.From).Single();

            CurrencyConvertion rate = new CurrencyConvertion
            {
                From = from,
                To = to,
                Rate = nexusTo.Rate * nexusFrom.Rate
            };

            rates.Add(rate);
        }
    }
}
