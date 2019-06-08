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

namespace Tests
{
    [TestClass]
    public class RateBusinessUnitTest
    {
        #region Fixture

        private const decimal firstRate = 1.359M;
        private const decimal secondRate = 0.736M;
        private const decimal thirdRate = 0.732M;
        private const decimal fourthRate = 1.366M;
        private const string firstFrom = "EUR";
        private const string firstTo = "USD";
        private const string secondFrom = "USD";
        private const string secondTo = "EUR";
        private const string thirdFrom = "CAD";
        private const string thirdTo = "EUR";
        private const string fourthFrom = "EUR";
        private const string fourthTo = "CAD";
        private const string excpetionMessage = "excpetionMessage";

        private static readonly List<Data.Model.CurrencyConvertion> emptyCurrencyConvertions = new List<Data.Model.CurrencyConvertion>();
        private static readonly List<Data.Model.CurrencyConvertion> twoCurrencyConvertions = new List<Data.Model.CurrencyConvertion>
        {
            new Data.Model.CurrencyConvertion
            {
                From = firstFrom,
                To = firstTo,
                Rate = firstRate
            },
            new Data.Model.CurrencyConvertion
            {
                From = secondFrom,
                To = secondTo,
                Rate = secondRate
            }
        };
        private static readonly List<Data.Model.CurrencyConvertion> fourCurrencyConvertions = new List<Data.Model.CurrencyConvertion>
        {
            new Data.Model.CurrencyConvertion
            {
                From = firstFrom,
                To = firstTo,
                Rate = firstRate
            },
            new Data.Model.CurrencyConvertion
            {
                From = secondFrom,
                To = secondTo,
                Rate = secondRate
            },
            new Data.Model.CurrencyConvertion
            {
                From = thirdFrom,
                To = thirdTo,
                Rate = thirdRate
            },
            new Data.Model.CurrencyConvertion
            {
                From = fourthFrom,
                To = fourthTo,
                Rate = fourthRate
            }
        };
        private static readonly Exception exception = new Exception(excpetionMessage);

        #endregion

        #region ListRates

        [TestMethod]
        public async Task ListRates_WithoutParameters_ReturnsARateList()
        {
            Mock<IRateData> data = new Mock<IRateData>();
            data.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            data.Setup(m => m.InsertOrUpdateRates(It.IsAny<List<Data.Model.CurrencyConvertion>>()));
            IRateBusiness sut = new RateBusiness(data.Object);

            List<CurrencyConvertion> result = await sut.ListRates();

            Assert.IsTrue(result is List<CurrencyConvertion>, "result is not a 'CurrencyConvertion list'");
        }

        [TestMethod]
        public async Task ListRates_WithoutParameters_InvokesListRatesMethodFromServicesLayer()
        {
            Mock<IRateData> data = new Mock<IRateData>();
            data.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            data.Setup(m => m.InsertOrUpdateRates(It.IsAny<List<Data.Model.CurrencyConvertion>>()));
            IRateBusiness sut = new RateBusiness(data.Object);

            List<CurrencyConvertion> result = await sut.ListRates();

            data.Verify(m => m.ListRates(), Times.Once());
        }

        [TestMethod]
        public async Task ListRates_WithoutParameters_InvokesInsertOrUpdateRatesMethodFromServicesLayer()
        {
            Mock<IRateData> data = new Mock<IRateData>();
            data.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            data.Setup(m => m.InsertOrUpdateRates(It.IsAny<List<Data.Model.CurrencyConvertion>>()));
            IRateBusiness sut = new RateBusiness(data.Object);

            List<CurrencyConvertion> result = await sut.ListRates();

            data.Verify(m => m.InsertOrUpdateRates(It.IsAny<List<Data.Model.CurrencyConvertion>>()), Times.Once());
        }

        [TestMethod]
        public async Task ListRates_CallsListRatesMethodFromServicesLayer_WichReturnsAnEmptyList()
        {
            Mock<IRateData> data = new Mock<IRateData>();
            data.Setup(m => m.ListRates()).Returns(Task.FromResult(emptyCurrencyConvertions));
            data.Setup(m => m.InsertOrUpdateRates(It.IsAny<List<Data.Model.CurrencyConvertion>>()));
            IRateBusiness sut = new RateBusiness(data.Object);

            List<CurrencyConvertion> result = await sut.ListRates();

            Assert.AreEqual(emptyCurrencyConvertions.Count, result.Count);
        }

        [TestMethod]
        public async Task ListRates_CallsListRatesMethodFromServicesLayer_WichReturnsAListWithTwoItems()
        {
            Mock<IRateData> data = new Mock<IRateData>();
            data.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            IRateBusiness sut = new RateBusiness(data.Object);

            List<CurrencyConvertion> result = await sut.ListRates();

            Assert.AreEqual(twoCurrencyConvertions.Count, result.Count);
        }

        [TestMethod]
        public async Task ListRates_WithMissingRates_ReturnsAllRates()
        {
            Mock<IRateData> data = new Mock<IRateData>();
            data.Setup(m => m.ListRates()).Returns(Task.FromResult(fourCurrencyConvertions));
            data.Setup(m => m.InsertOrUpdateRates(It.IsAny<List<Data.Model.CurrencyConvertion>>()));
            IRateBusiness sut = new RateBusiness(data.Object);

            List<CurrencyConvertion> result = await sut.ListRates();

            Assert.IsTrue(fourCurrencyConvertions.Count < result.Count);
        }

        [TestMethod]
        public async Task ListRates_CallsListRatesMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<IRateData> data = new Mock<IRateData>();
            data.Setup(m => m.ListRates()).Throws(exception);
            data.Setup(m => m.InsertOrUpdateRates(It.IsAny<List<Data.Model.CurrencyConvertion>>()));
            IRateBusiness sut = new RateBusiness(data.Object);

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
