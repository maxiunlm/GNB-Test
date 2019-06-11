using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Model;

namespace Data
{
    public interface IRateData
    {
        Task<List<CurrencyConvertion>> ListRates();
        void InsertOrUpdateRates(List<CurrencyConvertion> rates);
    }
}