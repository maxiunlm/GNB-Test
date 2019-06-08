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
    public class TransactionControllerUnitTest
    {
        #region Fixture

        private const decimal firstAmount = 1.359M;
        private const decimal secondAmount = 0.736M;
        private const string firstCurrency = "EUR";
        private const string firstSku = "T2006";
        private const string secondCurrency = "USD";
        private const string secondSku = "M2007";
        private const string excpetionMessage = "excpetionMessage";

        private static readonly List<Service.Model.Transaction> emptyTransactions = new List<Service.Model.Transaction>();
        private static readonly List<Service.Model.Transaction> oneTransaction = new List<Service.Model.Transaction>
        {
            firstTransaction
        };
        private static readonly List<Service.Model.Transaction> twoTransactions = new List<Service.Model.Transaction>
        {
            firstTransaction,
            secondTransaction
        };
        private static readonly Service.Model.Transaction firstTransaction = new Service.Model.Transaction
        {
            Sku = firstSku,
            Currency = firstCurrency,
            Amount = firstAmount
        };
        private static readonly Service.Model.Transaction secondTransaction = new Service.Model.Transaction
        {
            Sku = secondSku,
            Currency = secondCurrency,
            Amount = secondAmount
        };
        private static readonly Exception exception = new Exception(excpetionMessage);

        #endregion

        #region GET

        [TestMethod]
        public async Task Get_WithoutParameters_ReturnsATransactionList()
        {
            Mock<ILogger<TransactionController>> logger = new Mock<ILogger<TransactionController>>();
            Mock<ITransactionService> service = new Mock<ITransactionService>();
            service.Setup(o => o.ListTransactions()).Returns(Task.FromResult(oneTransaction));
            TransactionController sut = new TransactionController(service.Object, logger.Object);

            ActionResult<List<Transaction>> actionResult = await sut.Get();
            List<Transaction> result = (List<Transaction>)actionResult.Value;

            Assert.IsTrue(result is List<Transaction>, "result is not a 'Transaction list'");
        }

        [TestMethod]
        public async Task Get_WithoutParameters_InvokesListTransactionsMethodFromServicesLayer()
        {
            Mock<ILogger<TransactionController>> logger = new Mock<ILogger<TransactionController>>();
            Mock<ITransactionService> service = new Mock<ITransactionService>();
            service.Setup(m => m.ListTransactions()).Returns(Task.FromResult(oneTransaction));
            TransactionController sut = new TransactionController(service.Object, logger.Object);

            ActionResult<List<Transaction>> actionResult = await sut.Get();
            List<Transaction> result = (List<Transaction>)actionResult.Value;

            service.Verify(m => m.ListTransactions(), Times.Once());
        }

        [TestMethod]
        public async Task Get_CallsListTransactionsMethodFromServicesLayer_WichReturnsAnEmptyList()
        {
            Mock<ILogger<TransactionController>> logger = new Mock<ILogger<TransactionController>>();
            Mock<ITransactionService> service = new Mock<ITransactionService>();
            service.Setup(m => m.ListTransactions()).Returns(Task.FromResult(emptyTransactions));
            TransactionController sut = new TransactionController(service.Object, logger.Object);

            ActionResult<List<Transaction>> actionResult = await sut.Get();
            List<Transaction> result = (List<Transaction>)actionResult.Value;

            Assert.AreEqual(emptyTransactions.Count, result.Count);
        }

        [TestMethod]
        public async Task Get_CallsListTransactionsMethodFromServicesLayer_WichReturnsAListWithOnItem()
        {
            Mock<ILogger<TransactionController>> logger = new Mock<ILogger<TransactionController>>();
            Mock<ITransactionService> service = new Mock<ITransactionService>();
            service.Setup(m => m.ListTransactions()).Returns(Task.FromResult(oneTransaction));
            TransactionController sut = new TransactionController(service.Object, logger.Object);

            ActionResult<List<Transaction>> actionResult = await sut.Get();
            List<Transaction> result = (List<Transaction>)actionResult.Value;

            Assert.AreEqual(oneTransaction.Count, result.Count);
        }

        [TestMethod]
        public async Task Get_CallsListTransactionsMethodFromServicesLayer_WichReturnsAListWithTwoItems()
        {
            Mock<ILogger<TransactionController>> logger = new Mock<ILogger<TransactionController>>();
            Mock<ITransactionService> service = new Mock<ITransactionService>();
            service.Setup(m => m.ListTransactions()).Returns(Task.FromResult(twoTransactions));
            TransactionController sut = new TransactionController(service.Object, logger.Object);

            ActionResult<List<Transaction>> actionResult = await sut.Get();
            List<Transaction> result = (List<Transaction>)actionResult.Value;

            Assert.AreEqual(twoTransactions.Count, result.Count);
        }

        [TestMethod]
        public async Task Get_CallsListTransactionsMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<ILogger<TransactionController>> logger = new Mock<ILogger<TransactionController>>();
            Mock<ITransactionService> service = new Mock<ITransactionService>();
            service.Setup(m => m.ListTransactions()).Throws(exception);
            TransactionController sut = new TransactionController(service.Object, logger.Object);

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
