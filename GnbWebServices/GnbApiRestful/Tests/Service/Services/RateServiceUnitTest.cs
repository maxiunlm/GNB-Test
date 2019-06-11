using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Service.Model;
using Service;
using Business;
using Data;
using Moq;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class RateServiceUnitTest
    {
        #region Fixture
        private CommonFakes commonFakes = new CommonFakes();

        private static readonly List<Business.Model.CurrencyConvertion> emptyCurrencyConvertions = new List<Business.Model.CurrencyConvertion>();
        private static readonly List<Business.Model.CurrencyConvertion> oneCurrencyConvertion = new List<Business.Model.CurrencyConvertion>
        {
            firstCurrencyConvertion
        };
        private static readonly List<Business.Model.CurrencyConvertion> twoCurrencyConvertions = new List<Business.Model.CurrencyConvertion>
        {
            firstCurrencyConvertion,
            secondCurrencyConvertion
        };
        private static readonly Business.Model.CurrencyConvertion firstCurrencyConvertion = new Business.Model.CurrencyConvertion
        {
            From = CommonFakes.firstFrom,
            To = CommonFakes.firstTo,
            Rate = CommonFakes.firstRate
        };
        private static readonly Business.Model.CurrencyConvertion secondCurrencyConvertion = new Business.Model.CurrencyConvertion
        {
            From = CommonFakes.secondFrom,
            To = CommonFakes.secondTo,
            Rate = CommonFakes.secondRate
        };
        private static readonly Exception exception = new Exception(CommonFakes.excpetionMessage);

        #endregion

        #region ListRates

        [TestMethod]
        public async Task ListRates_WithoutParameters_ReturnsARateList()
        {
            Mock<IRateBusiness> business = new Mock<IRateBusiness>();
            business.Setup(m => m.ListRates()).Returns(Task.FromResult(oneCurrencyConvertion));
            IRateService sut = new RateService(business.Object, commonFakes.Mapper);

            List<CurrencyConvertion> result = await sut.ListRates();

            Assert.IsTrue(result is List<CurrencyConvertion>, "result is not a 'CurrencyConvertion list'");
        }

        [TestMethod]
        public async Task ListRates_WithoutParameters_InvokesListRatesMethodFromServicesLayer()
        {
            Mock<IRateBusiness> business = new Mock<IRateBusiness>();
            business.Setup(m => m.ListRates()).Returns(Task.FromResult(oneCurrencyConvertion));
            IRateService sut = new RateService(business.Object, commonFakes.Mapper);

            List<CurrencyConvertion> result = await sut.ListRates();

            business.Verify(m => m.ListRates(), Times.Once());
        }

        [TestMethod]
        public async Task ListRates_CallsListRatesMethodFromServicesLayer_WichReturnsAnEmptyList()
        {
            Mock<IRateBusiness> business = new Mock<IRateBusiness>();
            business.Setup(m => m.ListRates()).Returns(Task.FromResult(emptyCurrencyConvertions));
            IRateService sut = new RateService(business.Object, commonFakes.Mapper);

            List<CurrencyConvertion> result = await sut.ListRates();

            Assert.AreEqual(emptyCurrencyConvertions.Count, result.Count);
        }

        [TestMethod]
        public async Task ListRates_CallsListRatesMethodFromServicesLayer_WichReturnsAListWithOnItem()
        {
            Mock<IRateBusiness> business = new Mock<IRateBusiness>();
            business.Setup(m => m.ListRates()).Returns(Task.FromResult(oneCurrencyConvertion));
            IRateService sut = new RateService(business.Object, commonFakes.Mapper);

            List<CurrencyConvertion> result = await sut.ListRates();

            Assert.AreEqual(oneCurrencyConvertion.Count, result.Count);
        }

        [TestMethod]
        public async Task ListRates_CallsListRatesMethodFromServicesLayer_WichReturnsAListWithTwoItems()
        {
            Mock<IRateBusiness> business = new Mock<IRateBusiness>();
            business.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            IRateService sut = new RateService(business.Object, commonFakes.Mapper);

            List<CurrencyConvertion> result = await sut.ListRates();

            Assert.AreEqual(twoCurrencyConvertions.Count, result.Count);
        }

        [TestMethod]
        public async Task ListRates_CallsListRatesMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<IRateBusiness> business = new Mock<IRateBusiness>();
            business.Setup(m => m.ListRates()).Throws(exception);
            IRateService sut = new RateService(business.Object, commonFakes.Mapper);

            try
            {
                await sut.ListRates();

                Assert.IsTrue(false, "No exception thrown. Exception exception was expected.");
            }
            catch (Exception result)
            {
                Assert.AreSame(exception, result);
            }
        }

        #endregion
    }
}
