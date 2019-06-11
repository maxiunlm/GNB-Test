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
    public class TransactionServiceUnitTest
    {
        #region Fixture
        private CommonFakes commonFakes = new CommonFakes();
        private static readonly List<Business.Model.Transaction> emptyTransactions = new List<Business.Model.Transaction>();
        private static readonly List<Business.Model.Transaction> oneTransaction = new List<Business.Model.Transaction>
        {
            new Business.Model.Transaction
            {
                Sku = CommonFakes.firstSku,
                Currency = CommonFakes.firstCurrency,
                Amount = CommonFakes.firstAmount
            }
        };
        private static readonly List<Business.Model.Transaction> twoTransactions = new List<Business.Model.Transaction>
        {
            new Business.Model.Transaction
            {
                Sku = CommonFakes.firstSku,
                Currency = CommonFakes.firstCurrency,
                Amount = CommonFakes.firstAmount
            },
            new Business.Model.Transaction
            {
                Sku = CommonFakes.secondSku,
                Currency = CommonFakes.secondCurrency,
                Amount = CommonFakes.secondAmount
            }
        };
        private static readonly Exception exception = new Exception(CommonFakes.excpetionMessage);

        #endregion

        #region ListTransactions

        [TestMethod]
        public async Task ListTransactions_WithoutParameters_ReturnsATransactionList()
        {
            Mock<ITransactionBusiness> business = new Mock<ITransactionBusiness>();
            business.Setup(m => m.ListTransactions()).Returns(Task.FromResult(oneTransaction));
            ITransactionService sut = new TransactionService(business.Object, commonFakes.Mapper);

            List<Transaction> result = await sut.ListTransactions();

            Assert.IsTrue(result is List<Transaction>, "result is not a 'Transaction list'");
        }

        [TestMethod]
        public async Task ListTransactions_WithoutParameters_InvokesListTransactionsMethodFromBusinessLayer()
        {
            Mock<ITransactionBusiness> business = new Mock<ITransactionBusiness>();
            business.Setup(m => m.ListTransactions()).Returns(Task.FromResult(oneTransaction));
            ITransactionService sut = new TransactionService(business.Object, commonFakes.Mapper);

            List<Transaction> result = await sut.ListTransactions();

            business.Verify(m => m.ListTransactions(), Times.Once());
        }

        [TestMethod]
        public async Task ListTransactions_CallsListTransactionsMethodFromServicesLayer_WichReturnsAnEmptyList()
        {
            Mock<ITransactionBusiness> business = new Mock<ITransactionBusiness>();
            business.Setup(m => m.ListTransactions()).Returns(Task.FromResult(emptyTransactions));
            ITransactionService sut = new TransactionService(business.Object, commonFakes.Mapper);

            List<Transaction> result = await sut.ListTransactions();

            Assert.AreEqual(emptyTransactions.Count, result.Count);
        }

        [TestMethod]
        public async Task ListTransactions_CallsListTransactionsMethodFromServicesLayer_WichReturnsAListWithOnItem()
        {
            Mock<ITransactionBusiness> business = new Mock<ITransactionBusiness>();
            business.Setup(m => m.ListTransactions()).Returns(Task.FromResult(oneTransaction));
            ITransactionService sut = new TransactionService(business.Object, commonFakes.Mapper);

            List<Transaction> result = await sut.ListTransactions();

            Assert.AreEqual(oneTransaction.Count, result.Count);
        }

        [TestMethod]
        public async Task ListTransactions_CallsListTransactionsMethodFromServicesLayer_WichReturnsAListWithTwoItems()
        {
            Mock<ITransactionBusiness> business = new Mock<ITransactionBusiness>();
            business.Setup(m => m.ListTransactions()).Returns(Task.FromResult(twoTransactions));
            ITransactionService sut = new TransactionService(business.Object, commonFakes.Mapper);

            List<Transaction> result = await sut.ListTransactions();

            Assert.AreEqual(twoTransactions.Count, result.Count);
        }

        [TestMethod]
        public async Task ListTransactions_CallsListTransactionsMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<ITransactionBusiness> business = new Mock<ITransactionBusiness>();
            business.Setup(m => m.ListTransactions()).Throws(exception);
            ITransactionService sut = new TransactionService(business.Object, commonFakes.Mapper);

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
