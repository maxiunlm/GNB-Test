using AutoMapper;

namespace Webapi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Service.Model.Transaction, Webapi.Model.Transaction>();
            CreateMap<Webapi.Model.Transaction, Service.Model.Transaction>();
            CreateMap<Service.Model.Sku, Webapi.Model.Sku>();
            CreateMap<Webapi.Model.Sku, Service.Model.Sku>();
            CreateMap<Service.Model.CurrencyConvertion, Webapi.Model.CurrencyConvertion>();
            CreateMap<Webapi.Model.CurrencyConvertion, Service.Model.CurrencyConvertion>();

            CreateMap<Service.Model.Transaction, Business.Model.Transaction>();
            CreateMap<Business.Model.Transaction, Service.Model.Transaction>();
            CreateMap<Service.Model.Sku, Business.Model.Sku>();
            CreateMap<Business.Model.Sku, Service.Model.Sku>();
            CreateMap<Service.Model.CurrencyConvertion, Business.Model.CurrencyConvertion>();
            CreateMap<Business.Model.CurrencyConvertion, Service.Model.CurrencyConvertion>();

            CreateMap<Data.Model.Transaction, Business.Model.Transaction>();
            CreateMap<Business.Model.Transaction, Data.Model.Transaction>();
            CreateMap<Data.Model.CurrencyConvertion, Business.Model.CurrencyConvertion>();
            CreateMap<Business.Model.CurrencyConvertion, Data.Model.CurrencyConvertion>();
        }
    }
}