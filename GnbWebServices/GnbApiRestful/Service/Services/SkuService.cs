using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Service.Model;
using AutoMapper;

namespace Service
{
    public class SkuService : ISkuService
    {
        private readonly ISkuBusiness business;
        private readonly IMapper mapper;

        public SkuService(ISkuBusiness business, IMapper mapper)
        {
            this.business = business;
            this.mapper = mapper;
        }

        public async Task<Sku> GetTransactionsBySku(string sku)
        {
            Business.Model.Sku originData = await business.GetTransactionsBySku(sku);
            Sku skuModel = mapper.Map<Sku>(originData);

            return skuModel;
        }

        public List<string> ListSkus()
        {
            List<string> skus = business.ListSkus();

            return skus;
        }
    }
}
