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
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class SkuBusinessUnitTest
    {
        #region Fixture

        private CommonFakes commonFakes = new CommonFakes();
        private static readonly IBankCalculus bankCalculus = new BankCalculus();
        private static readonly List<string> emptySkus = new List<string>();
        private static readonly List<string> oneSku = new List<string> { CommonFakes.firstSku };
        private static readonly List<string> twoSkus = new List<string> { CommonFakes.firstSku, CommonFakes.secondSku };
        private static readonly Data.Model.Transaction firstTransaction = new Data.Model.Transaction
        {
            Sku = CommonFakes.firstSku,
            Currency = CommonFakes.firstCurrency,
            Amount = CommonFakes.firstAmount
        };
        private static readonly List<Data.Model.Transaction> emptyTransactions = new List<Data.Model.Transaction>();
        private static readonly List<Data.Model.Transaction> oneTransaction = new List<Data.Model.Transaction>
        {
            firstTransaction
        };
        private static readonly List<Data.Model.Transaction> oneTransactionForBank = new List<Data.Model.Transaction>
        {
            new Data.Model.Transaction
            {
                Sku = CommonFakes.firstSku,
                Currency = CommonFakes.secondCurrency,
                Amount = CommonFakes.secondAmount
            }
        };
        private static readonly List<Data.Model.Transaction> twoTransactionForBanks = new List<Data.Model.Transaction>
        {
            new Data.Model.Transaction
            {
                Sku = CommonFakes.firstSku,
                Currency = CommonFakes.firstCurrency,
                Amount = CommonFakes.firstAmount
            },
            new Data.Model.Transaction
            {
                Sku = CommonFakes.secondSku,
                Currency = CommonFakes.firstCurrency,
                Amount = CommonFakes.secondAmount
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
                From = CommonFakes.firstFrom,
                To = CommonFakes.firstTo,
                Rate = CommonFakes.firstRate
            },
            new CurrencyConvertion
            {
                From = CommonFakes.secondFrom,
                To = CommonFakes.secondTo,
                Rate = CommonFakes.secondRate
            }
        };
        private static readonly CurrencyConvertion firstCurrencyConvertion = new CurrencyConvertion
        {
            From = CommonFakes.firstFrom,
            To = CommonFakes.firstTo,
            Rate = CommonFakes.firstRate
        };
        private static readonly Exception exception = new Exception(CommonFakes.excpetionMessage);

        #endregion

        #region ListSkus

        [TestMethod]
        public void ListSkus_WithoutParameters_ReturnsAStringList()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(o => o.ListSkus()).Returns(oneSku);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object, bankCalculus, commonFakes.Configaration, commonFakes.Mapper);

            List<string> result = sut.ListSkus();

            Assert.IsTrue(result is List<string>, "result is not a 'string list'");
        }

        [TestMethod]
        public void ListSkus_WithoutParameters_InvokesListSkusMethodFromBusinessLayer()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(o => o.ListSkus()).Returns(oneSku);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object, bankCalculus, commonFakes.Configaration, commonFakes.Mapper);

            List<string> result = sut.ListSkus();

            data.Verify(m => m.ListSkus(), Times.Once());
        }

        [TestMethod]
        public void ListSkus_CallsListSkusMethodFromServicesLayer_WichReturnsAnEmptyList()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(o => o.ListSkus()).Returns(emptySkus);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object, bankCalculus, commonFakes.Configaration, commonFakes.Mapper);

            List<string> result = sut.ListSkus();

            Assert.AreEqual(emptySkus.Count, result.Count);
        }

        [TestMethod]
        public void ListSkus_CallsListSkusMethodFromServicesLayer_WichReturnsAListWithOnItem()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(o => o.ListSkus()).Returns(oneSku);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object, bankCalculus, commonFakes.Configaration, commonFakes.Mapper);

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
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object, bankCalculus, commonFakes.Configaration, commonFakes.Mapper);

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
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object, bankCalculus, commonFakes.Configaration, commonFakes.Mapper);

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
        public async Task GetTransactionsBySku_WithAnSkuId_InvokesGetTransactionsBySkuMethodFromBusniessLayer()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            rateBusiness.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(m => m.GetTransactionsBySku(CommonFakes.firstSku)).Returns(oneTransaction);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object, bankCalculus, commonFakes.Configaration, commonFakes.Mapper);

            Sku result = await sut.GetTransactionsBySku(CommonFakes.firstSku);

            data.Verify(m => m.GetTransactionsBySku(CommonFakes.firstSku), Times.Once);
        }

        [TestMethod]
        public async Task GetTransactionsBySku_WithAnSkuId_InvokesRoundBankMethodBankCalculusObject()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            rateBusiness.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            Mock<IBankCalculus> bankCalculus = new Mock<IBankCalculus>();
            bankCalculus.Setup(m => m.RoundBank(It.IsAny<decimal>())).Returns(CommonFakes.firstAmount);
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(m => m.GetTransactionsBySku(CommonFakes.firstSku)).Returns(oneTransactionForBank);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object, bankCalculus.Object, commonFakes.Configaration, commonFakes.Mapper);

            Sku result = await sut.GetTransactionsBySku(CommonFakes.firstSku);

            bankCalculus.Verify(m => m.RoundBank(It.IsAny<decimal>()), Times.Exactly(CommonFakes.twice));
        }

        [TestMethod]
        public async Task GetTransactionsBySku_WithAnSkuId_ReturnsAFullSkuData()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            rateBusiness.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(m => m.GetTransactionsBySku(CommonFakes.firstSku)).Returns(oneTransaction);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object, bankCalculus, commonFakes.Configaration, commonFakes.Mapper);

            Sku result = await sut.GetTransactionsBySku(CommonFakes.firstSku);

            Assert.AreEqual(CommonFakes.firstSku, result.Name);
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
            data.Setup(m => m.GetTransactionsBySku(CommonFakes.firstSku)).Returns(twoTransactionForBanks);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object, bankCalculus, commonFakes.Configaration, commonFakes.Mapper);

            Sku result = await sut.GetTransactionsBySku(CommonFakes.firstSku);
            decimal total = twoTransactionForBanks.Sum(o => commonFakes.RoundBank(o.Amount));
            total = commonFakes.RoundBank(total);

            Assert.AreEqual(CommonFakes.firstSku, result.Name);
            Assert.AreEqual(total, result.Total);
        }

        [TestMethod]
        public async Task GetTransactionsBySku_CallsGetTransactionsBySkuMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<IRateBusiness> rateBusiness = new Mock<IRateBusiness>();
            rateBusiness.Setup(m => m.ListRates()).Returns(Task.FromResult(twoCurrencyConvertions));
            Mock<ISkuData> data = new Mock<ISkuData>();
            data.Setup(m => m.GetTransactionsBySku(CommonFakes.firstSku)).Throws(exception);
            ISkuBusiness sut = new SkuBusiness(rateBusiness.Object, data.Object, bankCalculus, commonFakes.Configaration, commonFakes.Mapper);

            try
            {
                await sut.GetTransactionsBySku(CommonFakes.firstSku);

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
