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

        public SkuService(ISkuBusiness business)
        {
            this.business = business;

            MapperConfiguration automappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Business.Model.Transaction, Transaction>();
                config.CreateMap<Transaction, Business.Model.Transaction>();
                config.CreateMap<Business.Model.Sku, Sku>();
                config.CreateMap<Sku, Business.Model.Sku>();
            });
            this.mapper = automappingConfiguration.CreateMapper();
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
