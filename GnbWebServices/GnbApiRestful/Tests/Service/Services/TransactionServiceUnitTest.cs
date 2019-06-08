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

namespace Tests
{
    [TestClass]
    public class TransactionServiceUnitTest
    {
        #region Fixture

        private const decimal firstAmount = 1.359M;
        private const decimal secondAmount = 0.736M;
        private const string firstCurrency = "EUR";
        private const string firstSku = "T2006";
        private const string secondCurrency = "USD";
        private const string secondSku = "M2007";
        private const string excpetionMessage = "excpetionMessage";

        private static readonly List<Business.Model.Transaction> emptyTransactions = new List<Business.Model.Transaction>();
        private static readonly List<Business.Model.Transaction> oneTransaction = new List<Business.Model.Transaction>
        {
            firstTransaction
        };
        private static readonly List<Business.Model.Transaction> twoTransactions = new List<Business.Model.Transaction>
        {
            firstTransaction,
            secondTransaction
        };
        private static readonly Business.Model.Transaction firstTransaction = new Business.Model.Transaction
        {
            Sku = firstSku,
            Currency = firstCurrency,
            Amount = firstAmount
        };
        private static readonly Business.Model.Transaction secondTransaction = new Business.Model.Transaction
        {
            Sku = secondSku,
            Currency = secondCurrency,
            Amount = secondAmount
        };
        private static readonly Exception exception = new Exception(excpetionMessage);

        #endregion

        #region ListTransactions

        [TestMethod]
        public async Task ListTransactions_WithoutParameters_ReturnsATransactionList()
        {
            Mock<ITransactionBusiness> business = new Mock<ITransactionBusiness>();
            business.Setup(m => m.ListTransactions()).Returns(Task.FromResult(oneTransaction));
            ITransactionService sut = new TransactionService(business.Object);

            List<Transaction> result = await sut.ListTransactions();

            Assert.IsTrue(result is List<Transaction>, "result is not a 'Transaction list'");
        }

        [TestMethod]
        public async Task ListTransactions_WithoutParameters_InvokesListTransactionsMethodFromServicesLayer()
        {
            Mock<ITransactionBusiness> business = new Mock<ITransactionBusiness>();
            business.Setup(m => m.ListTransactions()).Returns(Task.FromResult(oneTransaction));
            ITransactionService sut = new TransactionService(business.Object);

            List<Transaction> result = await sut.ListTransactions();

            business.Verify(m => m.ListTransactions(), Times.Once());
        }

        [TestMethod]
        public async Task ListTransactions_CallsListTransactionsMethodFromServicesLayer_WichReturnsAnEmptyList()
        {
            Mock<ITransactionBusiness> business = new Mock<ITransactionBusiness>();
            business.Setup(m => m.ListTransactions()).Returns(Task.FromResult(emptyTransactions));
            ITransactionService sut = new TransactionService(business.Object);

            List<Transaction> result = await sut.ListTransactions();

            Assert.AreEqual(emptyTransactions.Count, result.Count);
        }

        [TestMethod]
        public async Task ListTransactions_CallsListTransactionsMethodFromServicesLayer_WichReturnsAListWithOnItem()
        {
            Mock<ITransactionBusiness> business = new Mock<ITransactionBusiness>();
            business.Setup(m => m.ListTransactions()).Returns(Task.FromResult(oneTransaction));
            ITransactionService sut = new TransactionService(business.Object);

            List<Transaction> result = await sut.ListTransactions();

            Assert.AreEqual(oneTransaction.Count, result.Count);
        }

        [TestMethod]
        public async Task ListTransactions_CallsListTransactionsMethodFromServicesLayer_WichReturnsAListWithTwoItems()
        {
            Mock<ITransactionBusiness> business = new Mock<ITransactionBusiness>();
            business.Setup(m => m.ListTransactions()).Returns(Task.FromResult(twoTransactions));
            ITransactionService sut = new TransactionService(business.Object);

            List<Transaction> result = await sut.ListTransactions();

            Assert.AreEqual(twoTransactions.Count, result.Count);
        }

        [TestMethod]
        public async Task ListTransactions_CallsListTransactionsMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<ITransactionBusiness> business = new Mock<ITransactionBusiness>();
            business.Setup(m => m.ListTransactions()).Throws(exception);
            ITransactionService sut = new TransactionService(business.Object);

            try
            {
                await sut.ListTransactions();

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
