using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Fakes
{
    public class CommonFakes
    {
        public const decimal firstRate = 1.359M;
        public const decimal secondRate = 0.736M;
        public const decimal thirdRate = 0.732M;
        public const decimal fourthRate = 1.366M;
        public const decimal firstAmount = 1.359M;
        public const decimal secondAmount = 0.736M;
        public const int digits = 2;
        public const decimal total = 14.99M;
        public const string defaultCurrency = "EUR";
        public const string nonSku = "";
        public const string firstCurrency = "EUR";
        public const string firstSku = "T2006";
        public const string secondCurrency = "USD";
        public const string secondSku = "M2007";
        public const string firstFrom = "EUR";
        public const string firstTo = "USD";
        public const string secondFrom = "USD";
        public const string secondTo = "EUR";
        public const string thirdFrom = "CAD";
        public const string thirdTo = "EUR";
        public const string fourthFrom = "EUR";
        public const string fourthTo = "CAD";
        public const string excpetionMessage = "excpetionMessage";

        public CommonFakes()
        {
            MapperConfiguration automappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Data.Model.CurrencyConvertion, Business.Model.CurrencyConvertion>();
                config.CreateMap<Service.Model.Transaction, Webapi.Model.Transaction>();
                config.CreateMap<Webapi.Model.Transaction, Service.Model.Transaction>();
                config.CreateMap<Service.Model.Sku, Webapi.Model.Sku>();
                config.CreateMap<Webapi.Model.Sku, Service.Model.Sku>();
                config.CreateMap<Service.Model.CurrencyConvertion, Webapi.Model.CurrencyConvertion>();
                config.CreateMap<Webapi.Model.CurrencyConvertion, Service.Model.CurrencyConvertion>();

                config.CreateMap<Service.Model.Transaction, Business.Model.Transaction>();
                config.CreateMap<Business.Model.Transaction, Service.Model.Transaction>();
                config.CreateMap<Service.Model.Sku, Business.Model.Sku>();
                config.CreateMap<Business.Model.Sku, Service.Model.Sku>();
                config.CreateMap<Service.Model.CurrencyConvertion, Business.Model.CurrencyConvertion>();
                config.CreateMap<Business.Model.CurrencyConvertion, Service.Model.CurrencyConvertion>();

                config.CreateMap<Data.Model.Transaction, Business.Model.Transaction>();
                config.CreateMap<Business.Model.Transaction, Data.Model.Transaction>();
                config.CreateMap<Data.Model.CurrencyConvertion, Business.Model.CurrencyConvertion>();
                config.CreateMap<Business.Model.CurrencyConvertion, Data.Model.CurrencyConvertion>();

            });
            this.Mapper = automappingConfiguration.CreateMapper();

            Mock<IConfiguration> configurationMock = new Mock<IConfiguration>();
            Configaration = configurationMock.Object;
        }

        public IMapper Mapper { get; private set; }
        public IConfiguration Configaration { get; private set; }

    }
}