using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Business;
using System;
using Moq;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class BankCalculusUnitTest
    {
        #region Fixture

        private CommonFakes commonFakes = new CommonFakes();

        #endregion

        #region RoundBank

        [TestMethod]
        public void RoundBank_WithAnIntegerAmmount_ReturnsSameNumber()
        {
            IBankCalculus sut = new BankCalculus();
            decimal integerAmmount = 9;

            decimal result = sut.RoundBank(integerAmmount);

            Assert.AreEqual(integerAmmount, result);
        }

        [TestMethod]
        public void RoundBank_WithThreeDecimals_ReturnsARoundedNumber()
        {
            IBankCalculus sut = new BankCalculus();
            decimal threeDecimals = 1.001M;

            decimal result = sut.RoundBank(threeDecimals);

            Assert.AreNotEqual(threeDecimals, result);
            Assert.IsTrue(result.ToString().Length == threeDecimals.ToString().Length - 1);
        }

        [TestMethod]
        public void RoundBank_WithThreeDecimalsLessThan005_ReturnsARoundedNumberToDown()
        {
            IBankCalculus sut = new BankCalculus();
            decimal threeDecimals = 1.004M;
            decimal expected = 1.00M;

            decimal result = sut.RoundBank(threeDecimals);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RoundBank_WithThreeDecimalsGreaterThan005_ReturnsARoundedNumberToUp()
        {
            IBankCalculus sut = new BankCalculus();
            decimal threeDecimals = 1.006M;
            decimal expected = 1.01M;

            decimal result = sut.RoundBank(threeDecimals);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RoundBank_WithThreeDecimalsEqualTo005_ReturnsARoundedNumberToUp()
        {
            IBankCalculus sut = new BankCalculus();
            decimal threeDecimals = 1.005M;
            decimal expected = 1.01M;

            decimal result = sut.RoundBank(threeDecimals);

            Assert.AreEqual(expected, result);
        }

        #endregion
    }
}