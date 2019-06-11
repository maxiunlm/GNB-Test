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
    public class TransactionBusinessUnitTest
    {
        #region Fixture
        private CommonFakes commonFakes = new CommonFakes();

        private static readonly List<Data.Model.Transaction> emptyTransactions = new List<Data.Model.Transaction>();
        private static readonly List<Data.Model.Transaction> oneTransaction = new List<Data.Model.Transaction>
        {
            new Data.Model.Transaction
            {
                Sku = CommonFakes.firstSku,
                Currency = CommonFakes.firstCurrency,
                Amount = CommonFakes.firstAmount
            }
        };
        private static readonly List<Data.Model.Transaction> twoTransactions = new List<Data.Model.Transaction>
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
            Mock<ITransactionData> data = new Mock<ITransactionData>();
            data.Setup(m => m.ListTransactions()).Returns(Task.FromResult(oneTransaction));
            data.Setup(m => m.RefreshTransactions(oneTransaction));
            ITransactionBusiness sut = new TransactionBusiness(data.Object, commonFakes.Mapper);

            List<Transaction> result = await sut.ListTransactions();

            Assert.IsTrue(result is List<Transaction>, "result is not a 'Transaction list'");
        }

        [TestMethod]
        public async Task ListTransactions_WithoutParameters_InvokesListTransactionsMethodFromServicesLayer()
        {
            Mock<ITransactionData> data = new Mock<ITransactionData>();
            data.Setup(m => m.ListTransactions()).Returns(Task.FromResult(oneTransaction));
            data.Setup(m => m.RefreshTransactions(oneTransaction));
            ITransactionBusiness sut = new TransactionBusiness(data.Object, commonFakes.Mapper);

            List<Transaction> result = await sut.ListTransactions();

            data.Verify(m => m.ListTransactions(), Times.Once());
        }

        [TestMethod]
        public async Task ListTransactions_WithoutParameters_InvokesRefreshTransactionsMethodFromServicesLayer()
        {
            Mock<ITransactionData> data = new Mock<ITransactionData>();
            data.Setup(m => m.ListTransactions()).Returns(Task.FromResult(oneTransaction));
            data.Setup(m => m.RefreshTransactions(oneTransaction));
            ITransactionBusiness sut = new TransactionBusiness(data.Object, commonFakes.Mapper);

            List<Transaction> result = await sut.ListTransactions();

            data.Verify(m => m.RefreshTransactions(oneTransaction), Times.Once());
        }

        [TestMethod]
        public async Task ListTransactions_CallsListTransactionsMethodFromServicesLayer_WichReturnsAnEmptyList()
        {
            Mock<ITransactionData> data = new Mock<ITransactionData>();
            data.Setup(m => m.ListTransactions()).Returns(Task.FromResult(emptyTransactions));
            data.Setup(m => m.RefreshTransactions(emptyTransactions));
            ITransactionBusiness sut = new TransactionBusiness(data.Object, commonFakes.Mapper);

            List<Transaction> result = await sut.ListTransactions();

            Assert.AreEqual(emptyTransactions.Count, result.Count);
        }

        [TestMethod]
        public async Task ListTransactions_CallsListTransactionsMethodFromServicesLayer_WichReturnsAListWithOnItem()
        {
            Mock<ITransactionData> data = new Mock<ITransactionData>();
            data.Setup(m => m.ListTransactions()).Returns(Task.FromResult(oneTransaction));
            data.Setup(m => m.RefreshTransactions(oneTransaction));
            ITransactionBusiness sut = new TransactionBusiness(data.Object, commonFakes.Mapper);

            List<Transaction> result = await sut.ListTransactions();

            Assert.AreEqual(oneTransaction.Count, result.Count);
        }

        [TestMethod]
        public async Task ListTransactions_CallsListTransactionsMethodFromServicesLayer_WichReturnsAListWithTwoItems()
        {
            Mock<ITransactionData> data = new Mock<ITransactionData>();
            data.Setup(m => m.ListTransactions()).Returns(Task.FromResult(twoTransactions));
            data.Setup(m => m.RefreshTransactions(twoTransactions));
            ITransactionBusiness sut = new TransactionBusiness(data.Object, commonFakes.Mapper);

            List<Transaction> result = await sut.ListTransactions();

            Assert.AreEqual(twoTransactions.Count, result.Count);
        }

        [TestMethod]
        public async Task ListTransactions_CallsListTransactionsMethodFromServicesLayer_WichThrowsAnException()
        {
            Mock<ITransactionData> data = new Mock<ITransactionData>();
            data.Setup(m => m.ListTransactions()).Throws(exception);
            ITransactionBusiness sut = new TransactionBusiness(data.Object, commonFakes.Mapper);

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
