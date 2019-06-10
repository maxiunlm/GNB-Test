using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Service.Model;
using AutoMapper;

namespace Service
{
    public class RateService : IRateService
    {
        private readonly IRateBusiness business;
        private readonly IMapper mapper;


        public RateService(IRateBusiness business, IMapper mapper)
        {
            this.business = business;
            this.mapper = mapper;
        }

        public async Task<List<CurrencyConvertion>> ListRates()
        {
            List<Business.Model.CurrencyConvertion> originData = await business.ListRates();
            List<CurrencyConvertion> rates = mapper.Map<List<CurrencyConvertion>>(originData);

            return rates;
        }
    }
}
