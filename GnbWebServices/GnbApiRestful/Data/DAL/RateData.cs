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
    public class RateData : IRateData
    {
        private readonly string ratesUrl;
        private readonly string ratesCollectionName;
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient client;

        public RateData(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            ratesUrl = configuration["RatesUrl"];
            ratesCollectionName = configuration["RatesCollectionName"];
            this.httpClientFactory = httpClientFactory;
            client = this.httpClientFactory.CreateClient();
            this.configuration = configuration;
        }

        public async Task<List<CurrencyConvertion>> ListRates()
        {
            List<CurrencyConvertion> rates = new List<CurrencyConvertion>();

            try
            {
                string jsonRates = await client.GetStringAsync(ratesUrl);
                rates = JsonConvert.DeserializeObject<List<CurrencyConvertion>>(jsonRates);
            }
            catch (Exception)
            {
                MongoDal mongoDal = new MongoDal(ratesCollectionName, configuration);
                rates = mongoDal.GetList<CurrencyConvertion>();
            }

            return rates;
        }

        public void InsertOrUpdateRates(List<CurrencyConvertion> rates)
        {
            MongoDal mongoDal = new MongoDal(ratesCollectionName, configuration);

            foreach (CurrencyConvertion rate in rates)
            {
                CurrencyConvertion existentRate = mongoDal.GetWhere<CurrencyConvertion>(o =>
                    o.From == rate.From
                    && o.To == rate.To).FirstOrDefault();

                if (existentRate == null)
                {
                    mongoDal.AddObject<CurrencyConvertion>(rate);
                }
                else
                {
                    existentRate.Rate = rate.Rate;
                    mongoDal.UpadeteObject<CurrencyConvertion>(existentRate);
                }
            }
        }
    }
}
