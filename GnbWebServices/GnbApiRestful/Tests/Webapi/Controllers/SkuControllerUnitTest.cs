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

namespace Tests
{
    [TestClass]
    public class SkuControllerUnitTest
    {
        #region Fixture

        private const decimal total = 14.99M;
        private const string defaultCurrency = "EUR";
        private const string nonSku = "";
        private const string firstSku = "E7719";
        private const string secondSku = "E7719";
        private const string excpetionMessage = "excpetionMessage";

        private static readonly List<string> emptySkus = new List<string>();
        private static readonly List<string> oneSku = new List<string> { firstSku };
        private static readonly List<string> twoSkus = new List<string> { firstSku, secondSku };
        private static readonly Service.Model.Transaction firstTransaction = new Service.Model.Transaction
        {
            Sku = firstSku,
            Currency = defaultCurrency,
            Amount = total
        };
        private static readonly Service.Model.Sku fullSku = new Service.Model.Sku
        {
            Name = firstSku,
            Total = total,
            Transactions = new List<Service.Model.Transaction> { firstTransaction }
        };
        private static readonly Exception exception = new Exception(excpetionMessage);

        #endregion

        #region GET

        [TestMethod]
        public void Get_WithoutParameters_ReturnsAStringList()
        {
            Mock<ILogger<SkuController>> logger = new Mock<ILogger<SkuController>>();
            Mock<ISkuService> service = new Mock<ISkuService>();
            service.Setup(o => o.ListSkus()).Returns(oneSku);
            SkuController sut = new SkuController(service.Object, logger.Object);

            ActionResult<List<string>> actionResult = sut.Get();
            List<string> result = (List<string>)actionResult.Value;

            Assert.IsTrue(result is List<string>, "result is not a 'string list'");
        }

        [TestMethod]
        public void Get_WithoutParameters_InvokesListSkusMethodFromServicesLayer()
        {
            Mock<ILogger<SkuController>> logger = new Mock<ILogger<SkuController>>();
            Mock<ISkuService> service = new Mock<ISkuService>();
            service.Setup(m => m.ListSkus()).Returns(oneSku);
            SkuController sut = new SkuController(service.Object, logger.Object);


            ActionResult<List<string>> actionResult = sut.Get();
            List<string> result = (List<string>)actionResult.Value;

            service.Verify(m => m.ListSkus(), Times.Once());
        }

        [TestMethod]
        public void Get_CallsListSkusMethodFromServicesLayer_WichReturnsAnEmptyList()
        {
            Mock<ILogger<SkuController>> logger = new Mock<ILogger<SkuController>>();
            Mock<ISkuService> service = new Mock<ISkuService>();
            service.Setup(m => m.ListSkus()).Returns(emptySkus);
            SkuController sut = new SkuController(service.Object, logger.Object);

            ActionResult<List<string>> actionResult = sut.Get();
            List<string> result = (List<string>)actionResult.Value;

            Assert.AreEqual(emptySkus.Count, result.Count);
        }

        [TestMethod]
        public void Get_CallsListSkusMethodFromServicesLayer_WichReturnsAListWithOnItem()
        {
            Mock<ILogger<SkuController>> logger = new Mock<ILogger<SkuController>>();
            Mock<ISkuService> service = new Mock<ISkuService>();
            service.Setup(m => m.ListSkus()).Returns(oneSku);
            SkuController sut = new SkuController(service.Object, logger.Object);

            ActionResult<List<string>> actionResult = sut.Get();
            List<string> result = (List<string>)actionResult.Value;

            Assert.AreEqual(oneSku.Count, result.Count);
            Assert.AreEqual(oneSku.First(), result.First());
        }

        [TestMethod]
        public void Get_CallsListSkusMethodFromServicesLayer_WichReturnsAListWithTwoItems()
        {
            Mock<ILogger<SkuController>> logger = new Mock<ILogger<SkuController>>();
            Mock<ISkuService> service = new Mock<ISkuService>();
            service.Setup(m => m.ListSkus()).Returns(twoSkus);
            SkuController sut = new SkuController(service.Object, logger.Object);

            ActionResult<List<string>> actionResult = sut.Get();
            List<string> result = (List<string>)actionResult.Value;

            Assert.AreEqual(twoSkus.Count, result.Count);
            Assert.AreEqual(twoSkus.First(), result.First());
            Assert.AreEqual(twoSkus.Last(), result.Last());
        }

        [TestMethod]
        public void Get_CallsListSkusMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<ILogger<SkuController>> logger = new Mock<ILogger<SkuController>>();
            Mock<ISkuService> service = new Mock<ISkuService>();
            service.Setup(m => m.ListSkus()).Throws(exception);
            SkuController sut = new SkuController(service.Object, logger.Object);

            try
            {
                sut.Get();

                Assert.IsTrue(false, "No exception thrown. Exception exception was expected.");
            }
            catch (Exception result)
            {
                Assert.AreSame(exception, result);
            }
        }

        #endregion

        #region GET(skuId)

        [TestMethod]
        public async Task Get_WithAnSkuId_InvokesGetTransactionsBySkuMethodFromServicesLayer()
        {
            Mock<ILogger<SkuController>> logger = new Mock<ILogger<SkuController>>();
            Mock<ISkuService> service = new Mock<ISkuService>();
            service.Setup(m => m.GetTransactionsBySku(firstSku)).Returns(Task.FromResult(fullSku));
            SkuController sut = new SkuController(service.Object, logger.Object);

            ActionResult<Sku> actionResult = await sut.Get(firstSku);
            Sku result = (Sku)actionResult.Value;

            service.Verify(m => m.GetTransactionsBySku(firstSku), Times.Once);
        }

        [TestMethod]
        public async Task Get_WithAnSkuId_ReturnsAFullSkuData()
        {
            Mock<ILogger<SkuController>> logger = new Mock<ILogger<SkuController>>();
            Mock<ISkuService> service = new Mock<ISkuService>();
            service.Setup(m => m.GetTransactionsBySku(firstSku)).Returns(Task.FromResult(fullSku));
            SkuController sut = new SkuController(service.Object, logger.Object);

            ActionResult<Sku> actionResult = await sut.Get(firstSku);
            Sku result = (Sku)actionResult.Value;

            Assert.AreEqual(fullSku.Name, result.Name);
            Assert.AreEqual(fullSku.Total, result.Total);
            Assert.AreEqual(fullSku.Transactions.Count, result.Transactions.Count);
            Assert.AreEqual(fullSku.Transactions.First().Sku, result.Transactions.First().Sku);
            Assert.AreEqual(fullSku.Transactions.First().Amount, result.Transactions.First().Amount);
            Assert.AreEqual(fullSku.Transactions.First().Currency, result.Transactions.First().Currency);
        }

        [TestMethod]
        public async Task Get_CallsGetTransactionsBySkuMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<ILogger<SkuController>> logger = new Mock<ILogger<SkuController>>();
            Mock<ISkuService> service = new Mock<ISkuService>();
            service.Setup(m => m.GetTransactionsBySku(firstSku)).Throws(exception);
            SkuController sut = new SkuController(service.Object, logger.Object);

            try
            {
                await sut.Get(firstSku);

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
