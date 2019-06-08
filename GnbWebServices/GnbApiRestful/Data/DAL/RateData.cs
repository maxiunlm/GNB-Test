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
    public class RateData : IRateData
    {
        private const string ratesUrl = "http://quiet-stone-2094.herokuapp.com/rates.json";
        private const string ratesCollectionName = "Rates";

        private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient client;

        public RateData(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            client = this.httpClientFactory.CreateClient();
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
                MongoDal mongoDal = new MongoDal(ratesCollectionName);
                rates = mongoDal.GetList<CurrencyConvertion>();
            }

            return rates;
        }

        public void InsertOrUpdateRates(List<CurrencyConvertion> rates)
        {
            MongoDal mongoDal = new MongoDal(ratesCollectionName);

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
