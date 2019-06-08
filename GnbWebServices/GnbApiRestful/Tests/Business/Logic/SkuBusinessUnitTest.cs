using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Business.Model;
using Business;
using Data;
using System;
using Moq;

namespace Tests
{
    [TestClass]
    public class SkuBusinessUnitTest
    {
        #region Fixture

        private const int digits = 2;
        private const decimal firstAmount = 14.99M;
        private const decimal secondAmount = 34.57M;
        private const decimal firstRate = 1.359M;
        private const decimal secondRate = 0.736M;
        private const string firstFrom = "EUR";
        private const string firstTo = "USD";
        private const string secondFrom = "USD";
        private const string secondTo = "EUR";
        private const string firstCurrency = "EUR";
        private const string secondCurrency = "USD";
        private const string nonSku = "";
        private const string firstSku = "E7719";
        private const string secondSku = "E7719";
        private const string excpetionMessage = "excpetionMessage";

        private static readonly List<string> emptySkus = new List<string>();
        private static readonly List<string> oneSku = new List<string> { firstSku };
        private static readonly List<string> twoSkus = new List<string> { firstSku, secondSku };
        private static readonly Data.Model.Transaction firstTransaction = new Data.Model.Transaction
        {
            Sku = firstSku,
            Currency = firstCurrency,
            Amount = firstAmount
        };
        private static readonly List<Data.Model.Transaction> emptyTransactions = new List<Data.Model.Transaction>();
        private static readonly List<Data.Model.Transaction> oneTransaction = new List<Data.Model.Transaction>
        {
            firstTransaction
        };
        private static readonly List<Data.Model.Transaction> twoTransactions = new List<Data.Model.Transaction>
        {
            new Data.Model.Transaction
            {
                Sku = firstSku,
                Currency = firstCurrency,
                Amount = firstAmount
            },
            new Data.Model.Transaction
            {
                Sku = secondSku,
                Currency = secondCurrency,
                Amount = secondAmount
            }
        };
        private static readonly List<Data.Model.Transaction> twoTransactionForBanks = new List<Data.Model.Transaction>
        {
            new Data.Model.Transaction
            {
                Sku = firstSku,
                Currency = firstCurrency,
                Amount = firstAmount
            },
            new Data.Model.Transaction
            {
                Sku = secondSku,
                Currency = firstCurrency,
                Amount = secondAmount
            }
        };
        private static readonly List<CurrencyConvertion> emptyCurrencyConvertions = new List<CurrencyConvertion>();
        private static readonly List<CurrencyConvertion> oneCurrencyConvertion = new List<CurrencyConvertion>
        {
            firstCurrencyConvertion
        };
        private static readonly List<CurrencyConvertion> twoCurrencyConvertions = new List<CurrencyConvertion>
        {
            new CurrencyConvertion
            {
                From = firstFrom,
                To = firstTo,
                Rate = firstRate
            },
            new CurrencyConvertion
            {
                From = secondFrom,
                To = secondTo,
                Rate = secondRate
            }
        };
        private static readonly CurrencyConvertion firstCurrencyConvertion = new CurrencyConvertion
        {
            From = firstFrom,
            To = firstTo,
            Rate = firstRate
        };
        private static readonly Exception exception = new Exception(excpetionMessage);

        #endregion

        #region ListSkus

        [TestMethod]
        public void ListSkus_WithoutParameters_ReturnsAStringList()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(o => o.ListSkus()).Returns(oneSku);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object);

            List<string> result = sut.ListSkus();

            Assert.IsTrue(result is List<string>, "result is not a 'string list'");
        }

        [TestMethod]
        public void ListSkus_WithoutParameters_InvokesListSkusMethodFromServicesLayer()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(o => o.ListSkus()).Returns(oneSku);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object);

            List<string> result = sut.ListSkus();

            data.Verify(m => m.ListSkus(), Times.Once());
        }

        [TestMethod]
        public void ListSkus_CallsListSkusMethodFromServicesLayer_WichReturnsAnEmptyList()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(o => o.ListSkus()).Returns(emptySkus);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object);

            List<string> result = sut.ListSkus();

            Assert.AreEqual(emptySkus.Count, result.Count);
        }

        [TestMethod]
        public void ListSkus_CallsListSkusMethodFromServicesLayer_WichReturnsAListWithOnItem()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(o => o.ListSkus()).Returns(oneSku);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object);

            List<string> result = sut.ListSkus();

            Assert.AreEqual(oneSku.Count, result.Count);
            Assert.AreEqual(oneSku.First(), result.First());
        }

        [TestMethod]
        public void ListSkus_CallsListSkusMethodFromServicesLayer_WichReturnsAListWithTwoItems()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(o => o.ListSkus()).Returns(twoSkus);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object);

            List<string> result = sut.ListSkus();

            Assert.AreEqual(twoSkus.Count, result.Count);
            Assert.AreEqual(twoSkus.First(), result.First());
            Assert.AreEqual(twoSkus.Last(), result.Last());
        }

        [TestMethod]
        public void ListSkus_CallsListSkusMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(o => o.ListSkus()).Throws(exception);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object);

            try
            {
                sut.ListSkus();

                Assert.IsTrue(false, "No exception thrown. Exception exception was expected.");
            }
            catch (Exception result)
            {
                Assert.AreSame(exception, result);
            }
        }

        #endregion

        #region GetTransactionsBySku

        [TestMethod]
        public async Task GetTransactionsBySku_WithAnSkuId_InvokesGetTransactionsBySkuMethodFromServicesLayer()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            rateBusiness.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(m => m.GetTransactionsBySku(firstSku)).Returns(Task.FromResult(oneTransaction));
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object);

            Sku result = await sut.GetTransactionsBySku(firstSku);

            data.Verify(m => m.GetTransactionsBySku(firstSku), Times.Once);
        }

        [TestMethod]
        public async Task GetTransactionsBySku_WithAnSkuId_ReturnsAFullSkuData()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            rateBusiness.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(m => m.GetTransactionsBySku(firstSku)).Returns(Task.FromResult(oneTransaction));
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object);

            Sku result = await sut.GetTransactionsBySku(firstSku);

            Assert.AreEqual(firstSku, result.Name);
            Assert.AreEqual(oneTransaction.Count, result.Transactions.Count);
            Assert.AreEqual(oneTransaction.First().Sku, result.Transactions.First().Sku);
            Assert.AreEqual(oneTransaction.First().Amount, result.Transactions.First().Amount);
            Assert.AreEqual(oneTransaction.First().Currency, result.Transactions.First().Currency);
        }

        [TestMethod]
        public async Task GetTransactionsBySku_WithAnSkuId_ReturnsAFullSkuDataWithTotalRoundedToBank()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            rateBusiness.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(m => m.GetTransactionsBySku(firstSku)).Returns(Task.FromResult(twoTransactionForBanks));
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object);

            Sku result = await sut.GetTransactionsBySku(firstSku);
            decimal total = twoTransactionForBanks.Sum(o => RoundBank(o.Amount));
            total = RoundBank(total);

            Assert.AreEqual(firstSku, result.Name);
            Assert.AreEqual(total, result.Total);
        }

        [TestMethod]
        public async Task GetTransactionsBySku_CallsGetTransactionsBySkuMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            rateBusiness.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(m => m.GetTransactionsBySku(firstSku)).Throws(exception);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object);

            try
            {
                await sut.GetTransactionsBySku(firstSku);

                Assert.IsTrue(false, "No exception thrown. Exception exception was expected.");
            }
            catch (Exception result)
            {
                Assert.AreSame(exception, result);
            }
        }

        #endregion

        private decimal RoundBank(decimal amount)
        {
            return Math.Round(amount, digits, MidpointRounding.AwayFromZero);
        }
    }
}
