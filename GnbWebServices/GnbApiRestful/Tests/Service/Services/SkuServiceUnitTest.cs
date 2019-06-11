using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Service;
using Service.Model;
using Business;
using Data;
using System;
using Moq;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class SkuServiceUnitTest
    {
        #region Fixture
        private CommonFakes commonFakes = new CommonFakes();

        private static readonly List<string> emptySkus = new List<string>();
        private static readonly List<string> oneSku = new List<string> { CommonFakes.firstSku };
        private static readonly List<string> twoSkus = new List<string> { CommonFakes.firstSku, CommonFakes.secondSku };
        private static readonly Business.Model.Transaction firstTransaction = new Business.Model.Transaction
        {
            Sku = CommonFakes.firstSku,
            Currency = CommonFakes.defaultCurrency,
            Amount = CommonFakes.total
        };
        private static readonly Business.Model.Sku fullSku = new Business.Model.Sku
        {
            Name = CommonFakes.firstSku,
            Total = CommonFakes.total,
            Transactions = new List<Business.Model.Transaction> { firstTransaction }
        };
        private static readonly Exception exception = new Exception(CommonFakes.excpetionMessage);

        #endregion

        #region ListSkus

        [TestMethod]
        public void ListSkus_WithoutParameters_ReturnsAStringList()
        {
            Mock<ISkuBusiness> business = new Mock<ISkuBusiness>();
            business.Setup(o => o.ListSkus()).Returns(oneSku);
            ISkuService sut = new SkuService(business.Object, commonFakes.Mapper);

            List<string> result = sut.ListSkus();

            Assert.IsTrue(result is List<string>, "result is not a 'string list'");
        }

        [TestMethod]
        public void ListSkus_WithoutParameters_InvokesListSkusMethodFromServicesLayer()
        {
            Mock<ISkuBusiness> business = new Mock<ISkuBusiness>();
            business.Setup(m => m.ListSkus()).Returns(oneSku);
            ISkuService sut = new SkuService(business.Object, commonFakes.Mapper);

            List<string> result = sut.ListSkus();

            business.Verify(m => m.ListSkus(), Times.Once());
        }

        [TestMethod]
        public void ListSkus_CallsListSkusMethodFromServicesLayer_WichReturnsAnEmptyList()
        {
            Mock<ISkuBusiness> business = new Mock<ISkuBusiness>();
            business.Setup(m => m.ListSkus()).Returns(emptySkus);
            ISkuService sut = new SkuService(business.Object, commonFakes.Mapper);

            List<string> result = sut.ListSkus();

            Assert.AreEqual(emptySkus.Count, result.Count);
        }

        [TestMethod]
        public void ListSkus_CallsListSkusMethodFromServicesLayer_WichReturnsAListWithOnItem()
        {
            Mock<ISkuBusiness> business = new Mock<ISkuBusiness>();
            business.Setup(m => m.ListSkus()).Returns(oneSku);
            ISkuService sut = new SkuService(business.Object, commonFakes.Mapper);

            List<string> result = sut.ListSkus();

            Assert.AreEqual(oneSku.Count, result.Count);
            Assert.AreEqual(oneSku.First(), result.First());
        }

        [TestMethod]
        public void ListSkus_CallsListSkusMethodFromServicesLayer_WichReturnsAListWithTwoItems()
        {
            Mock<ISkuBusiness> business = new Mock<ISkuBusiness>();
            business.Setup(m => m.ListSkus()).Returns(twoSkus);
            ISkuService sut = new SkuService(business.Object, commonFakes.Mapper);

            List<string> result = sut.ListSkus();

            Assert.AreEqual(twoSkus.Count, result.Count);
            Assert.AreEqual(twoSkus.First(), result.First());
            Assert.AreEqual(twoSkus.Last(), result.Last());
        }

        [TestMethod]
        public void ListSkus_CallsListSkusMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<ISkuBusiness> business = new Mock<ISkuBusiness>();
            business.Setup(m => m.ListSkus()).Throws(exception);
            ISkuService sut = new SkuService(business.Object, commonFakes.Mapper);

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
            Mock<ISkuBusiness> business = new Mock<ISkuBusiness>();
            business.Setup(m => m.GetTransactionsBySku(CommonFakes.firstSku)).Returns(Task.FromResult(fullSku));
            ISkuService sut = new SkuService(business.Object, commonFakes.Mapper);

            Sku result = await sut.GetTransactionsBySku(CommonFakes.firstSku);

            business.Verify(m => m.GetTransactionsBySku(CommonFakes.firstSku), Times.Once);
        }

        [TestMethod]
        public async Task GetTransactionsBySku_WithAnSkuId_ReturnsAFullSkuData()
        {
            Mock<ISkuBusiness> business = new Mock<ISkuBusiness>();
            business.Setup(m => m.GetTransactionsBySku(CommonFakes.firstSku)).Returns(Task.FromResult(fullSku));
            ISkuService sut = new SkuService(business.Object, commonFakes.Mapper);

            Sku result = await sut.GetTransactionsBySku(CommonFakes.firstSku);

            Assert.AreEqual(fullSku.Name, result.Name);
            Assert.AreEqual(fullSku.Total, result.Total);
            Assert.AreEqual(fullSku.Transactions.Count, result.Transactions.Count);
            Assert.AreEqual(fullSku.Transactions.First().Sku, result.Transactions.First().Sku);
            Assert.AreEqual(fullSku.Transactions.First().Amount, result.Transactions.First().Amount);
            Assert.AreEqual(fullSku.Transactions.First().Currency, result.Transactions.First().Currency);
        }

        [TestMethod]
        public async Task GetTransactionsBySku_CallsGetTransactionsBySkuMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<ISkuBusiness> business = new Mock<ISkuBusiness>();
            business.Setup(m => m.GetTransactionsBySku(CommonFakes.firstSku)).Throws(exception);
            ISkuService sut = new SkuService(business.Object, commonFakes.Mapper);

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
