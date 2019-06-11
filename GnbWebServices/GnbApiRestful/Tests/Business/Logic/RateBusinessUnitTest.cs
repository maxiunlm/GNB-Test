using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Business.Model;
using Business;
using Data;
using Moq;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class RateBusinessUnitTest
    {

        #region Fixture
        private CommonFakes commonFakes = new CommonFakes();
        private static readonly List<Data.Model.CurrencyConvertion> emptyCurrencyConvertions = new List<Data.Model.CurrencyConvertion>();
        private static readonly List<Data.Model.CurrencyConvertion> twoCurrencyConvertions = new List<Data.Model.CurrencyConvertion>
        {
            new Data.Model.CurrencyConvertion
            {
                From = CommonFakes.firstFrom,
                To = CommonFakes.firstTo,
                Rate = CommonFakes.firstRate
            },
            new Data.Model.CurrencyConvertion
            {
                From = CommonFakes.secondFrom,
                To = CommonFakes.secondTo,
                Rate = CommonFakes.secondRate
            }
        };
        private static readonly List<Data.Model.CurrencyConvertion> fourCurrencyConvertions = new List<Data.Model.CurrencyConvertion>
        {
            new Data.Model.CurrencyConvertion
            {
                From = CommonFakes.firstFrom,
                To = CommonFakes.firstTo,
                Rate = CommonFakes.firstRate
            },
            new Data.Model.CurrencyConvertion
            {
                From = CommonFakes.secondFrom,
                To = CommonFakes.secondTo,
                Rate = CommonFakes.secondRate
            },
            new Data.Model.CurrencyConvertion
            {
                From = CommonFakes.thirdFrom,
                To = CommonFakes.thirdTo,
                Rate = CommonFakes.thirdRate
            },
            new Data.Model.CurrencyConvertion
            {
                From = CommonFakes.fourthFrom,
                To = CommonFakes.fourthTo,
                Rate = CommonFakes.fourthRate
            }
        };
        private static readonly Exception exception = new Exception(CommonFakes.excpetionMessage);

        #endregion

        #region ListRates

        [TestMethod]
        public async Task ListRates_WithoutParameters_ReturnsARateList()
        {
            Mock<IRateData> data = new Mock<IRateData>();
            data.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            data.Setup(m => m.InsertOrUpdateRates(It.IsAny<List<Data.Model.CurrencyConvertion>>()));
            IRateBusiness sut = new RateBusiness(data.Object, commonFakes.Mapper);

            List<CurrencyConvertion> result = await sut.ListRates();

            Assert.IsTrue(result is List<CurrencyConvertion>, "result is not a 'CurrencyConvertion list'");
        }

        [TestMethod]
        public async Task ListRates_WithoutParameters_InvokesListRatesMethodFromDataLayer()
        {
            Mock<IRateData> data = new Mock<IRateData>();
            data.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            data.Setup(m => m.InsertOrUpdateRates(It.IsAny<List<Data.Model.CurrencyConvertion>>()));
            IRateBusiness sut = new RateBusiness(data.Object, commonFakes.Mapper);

            List<CurrencyConvertion> result = await sut.ListRates();

            data.Verify(m => m.ListRates(), Times.Once());
        }

        [TestMethod]
        public async Task ListRates_WithoutParameters_InvokesInsertOrUpdateRatesMethodFromDataLayer()
        {
            Mock<IRateData> data = new Mock<IRateData>();
            data.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            data.Setup(m => m.InsertOrUpdateRates(It.IsAny<List<Data.Model.CurrencyConvertion>>()));
            IRateBusiness sut = new RateBusiness(data.Object, commonFakes.Mapper);

            List<CurrencyConvertion> result = await sut.ListRates();

            data.Verify(m => m.InsertOrUpdateRates(It.IsAny<List<Data.Model.CurrencyConvertion>>()), Times.Once());
        }

        [TestMethod]
        public async Task ListRates_CallsListRatesMethodFromServicesLayer_WichReturnsAnEmptyList()
        {
            Mock<IRateData> data = new Mock<IRateData>();
            data.Setup(m => m.ListRates()).Returns(Task.FromResult(emptyCurrencyConvertions));
            data.Setup(m => m.InsertOrUpdateRates(It.IsAny<List<Data.Model.CurrencyConvertion>>()));
            IRateBusiness sut = new RateBusiness(data.Object, commonFakes.Mapper);

            List<CurrencyConvertion> result = await sut.ListRates();

            Assert.AreEqual(emptyCurrencyConvertions.Count, result.Count);
        }

        [TestMethod]
        public async Task ListRates_CallsListRatesMethodFromServicesLayer_WichReturnsAListWithTwoItems()
        {
            Mock<IRateData> data = new Mock<IRateData>();
            data.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            IRateBusiness sut = new RateBusiness(data.Object, commonFakes.Mapper);

            List<CurrencyConvertion> result = await sut.ListRates();

            Assert.AreEqual(twoCurrencyConvertions.Count, result.Count);
        }

        [TestMethod]
        public async Task ListRates_WithMissingRates_ReturnsAllRates()
        {
            Mock<IRateData> data = new Mock<IRateData>();
            data.Setup(m => m.ListRates()).Returns(Task.FromResult(fourCurrencyConvertions));
            data.Setup(m => m.InsertOrUpdateRates(It.IsAny<List<Data.Model.CurrencyConvertion>>()));
            IRateBusiness sut = new RateBusiness(data.Object, commonFakes.Mapper);

            List<CurrencyConvertion> result = await sut.ListRates();

            Assert.IsTrue(fourCurrencyConvertions.Count < result.Count);
        }

        [TestMethod]
        public async Task ListRates_CallsListRatesMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<IRateData> data = new Mock<IRateData>();
            data.Setup(m => m.ListRates()).Throws(exception);
            data.Setup(m => m.InsertOrUpdateRates(It.IsAny<List<Data.Model.CurrencyConvertion>>()));
            IRateBusiness sut = new RateBusiness(data.Object, commonFakes.Mapper);

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
