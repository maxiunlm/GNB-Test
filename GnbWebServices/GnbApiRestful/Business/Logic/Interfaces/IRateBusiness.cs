using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Model;

namespace Business
{
    public interface IRateBusiness
    {
        Task<List<CurrencyConvertion>> ListRates();
    }
}