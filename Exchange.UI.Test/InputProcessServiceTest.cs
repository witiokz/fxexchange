using System;
using Exchange.Data;
using Exchange.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Exchange.UI.Test
{
    [TestClass]
    public class InputProcessServiceTest
    {
        [TestMethod]
        public void Test_If_Empty_input_data_Throws_Exception()
        {
           var inputData = new string[0];
           var mock = new Mock<ICurrencyItemRepository>();

            var inputProcessService = new InputProcessService(mock.Object);

            Assert.ThrowsException<Exception>(() => inputProcessService.ProcessInputData(inputData));
        }

        [TestMethod]
        public void Test_If_Incorrect_Currency_Pair_Throws_Exception()
        {
            var inputData = new string[] { "pair", "1" };
            var mock = new Mock<ICurrencyItemRepository>();

            var inputProcessService = new InputProcessService(mock.Object);

            Assert.ThrowsException<Exception>(() => inputProcessService.ProcessInputData(inputData));
        }

        [TestMethod]
        public void Test_If_Incorrect_Currency_Amount_Throws_Exception()
        {
            var inputData = new string[] { "EUR/DKK", "amount" };
            var mock = new Mock<ICurrencyItemRepository>();

            var inputProcessService = new InputProcessService(mock.Object);

            Assert.ThrowsException<Exception>(() => inputProcessService.ProcessInputData(inputData));
        }

        [TestMethod]
        public void Test_If_Incorrect_Exchange_Throws_Exception()
        {
            var inputData = new string[] { "EUR1/DKK", "1" };
            var mock = new Mock<ICurrencyItemRepository>();

            var inputProcessService = new InputProcessService(mock.Object);

            Assert.ThrowsException<Exception>(() => inputProcessService.ProcessInputData(inputData));
        }
    }
}
