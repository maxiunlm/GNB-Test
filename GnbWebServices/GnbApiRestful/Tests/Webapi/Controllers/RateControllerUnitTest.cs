using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Webapi.Controllers;
using Webapi.Model;
using Service;
using Business;
using Data;
using Moq;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class RateControllerUnitTest
    {
        #region Fixture
        private CommonFakes commonFakes = new CommonFakes();
        private static readonly List<Service.Model.CurrencyConvertion> emptyCurrencyConvertions = new List<Service.Model.CurrencyConvertion>();
        private static readonly List<Service.Model.CurrencyConvertion> oneCurrencyConvertion = new List<Service.Model.CurrencyConvertion>
        {
            firstCurrencyConvertion
        };
        private static readonly List<Service.Model.CurrencyConvertion> twoCurrencyConvertions = new List<Service.Model.CurrencyConvertion>
        {
            firstCurrencyConvertion,
            secondCurrencyConvertion
        };
        private static readonly Service.Model.CurrencyConvertion firstCurrencyConvertion = new Service.Model.CurrencyConvertion
        {
            From = CommonFakes.firstFrom,
            To = CommonFakes.firstTo,
            Rate = CommonFakes.firstRate
        };
        private static readonly Service.Model.CurrencyConvertion secondCurrencyConvertion = new Service.Model.CurrencyConvertion
        {
            From = CommonFakes.secondFrom,
            To = CommonFakes.secondTo,
            Rate = CommonFakes.secondRate
        };
        private static readonly Exception exception = new Exception(CommonFakes.excpetionMessage);

        #endregion

        #region GET

        [TestMethod]
        public async Task Get_WithoutParameters_ReturnsARateList()
        {
            Mock<ILogger<RateController>> logger = new Mock<ILogger<RateController>>();
            Mock<IRateService> service = new Mock<IRateService>();
            service.Setup(o => o.ListRates()).Returns(Task.FromResult(oneCurrencyConvertion));
            RateController sut = new RateController(service.Object, commonFakes.Mapper);

            ActionResult<List<CurrencyConvertion>> actionResult = await sut.Get();
            List<CurrencyConvertion> result = (List<CurrencyConvertion>)actionResult.Value;

            Assert.IsTrue(result is List<CurrencyConvertion>, "result is not a 'CurrencyConvertion list'");
        }

        [TestMethod]
        public async Task Get_WithoutParameters_InvokesListRatesMethodFromServicesLayer()
        {
            Mock<ILogger<RateController>> logger = new Mock<ILogger<RateController>>();
            Mock<IRateService> service = new Mock<IRateService>();
            service.Setup(m => m.ListRates()).Returns(Task.FromResult(oneCurrencyConvertion));
            RateController sut = new RateController(service.Object, commonFakes.Mapper);

            ActionResult<List<CurrencyConvertion>> actionResult = await sut.Get();
            List<CurrencyConvertion> result = (List<CurrencyConvertion>)actionResult.Value;

            service.Verify(m => m.ListRates(), Times.Once());
        }

        [TestMethod]
        public async Task Get_CallsListRatesMethodFromServicesLayer_WichReturnsAnEmptyList()
        {
            Mock<ILogger<RateController>> logger = new Mock<ILogger<RateController>>();
            Mock<IRateService> service = new Mock<IRateService>();
            service.Setup(m => m.ListRates()).Returns(Task.FromResult(emptyCurrencyConvertions));
            RateController sut = new RateController(service.Object, commonFakes.Mapper);

            ActionResult<List<CurrencyConvertion>> actionResult = await sut.Get();
            List<CurrencyConvertion> result = (List<CurrencyConvertion>)actionResult.Value;

            Assert.AreEqual(emptyCurrencyConvertions.Count, result.Count);
        }

        [TestMethod]
        public async Task Get_CallsListRatesMethodFromServicesLayer_WichReturnsAListWithOnItem()
        {
            Mock<ILogger<RateController>> logger = new Mock<ILogger<RateController>>();
            Mock<IRateService> service = new Mock<IRateService>();
            service.Setup(m => m.ListRates()).Returns(Task.FromResult(oneCurrencyConvertion));
            RateController sut = new RateController(service.Object, commonFakes.Mapper);

            ActionResult<List<CurrencyConvertion>> actionResult = await sut.Get();
            List<CurrencyConvertion> result = (List<CurrencyConvertion>)actionResult.Value;

            Assert.AreEqual(oneCurrencyConvertion.Count, result.Count);
        }

        [TestMethod]
        public async Task Get_CallsListRatesMethodFromServicesLayer_WichReturnsAListWithTwoItems()
        {
            Mock<ILogger<RateController>> logger = new Mock<ILogger<RateController>>();
            Mock<IRateService> service = new Mock<IRateService>();
            service.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            RateController sut = new RateController(service.Object, commonFakes.Mapper);

            ActionResult<List<CurrencyConvertion>> actionResult = await sut.Get();
            List<CurrencyConvertion> result = (List<CurrencyConvertion>)actionResult.Value;

            Assert.AreEqual(twoCurrencyConvertions.Count, result.Count);
        }

        [TestMethod]
        public async Task Get_CallsListRatesMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<ILogger<RateController>> logger = new Mock<ILogger<RateController>>();
            Mock<IRateService> service = new Mock<IRateService>();
            service.Setup(m => m.ListRates()).Throws(exception);
            RateController sut = new RateController(service.Object, commonFakes.Mapper);

            try
            {
                await sut.Get();

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
