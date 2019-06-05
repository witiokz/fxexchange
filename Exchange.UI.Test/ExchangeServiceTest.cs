using Exchange.Data;
using Exchange.Data.Domain;
using Exchange.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Exchange.UI.Test
{
    [TestClass]
    public class ExchangeServiceTest
    {
        [TestMethod]
        public void Test_If_The_Same_Currencies_Return_Same_Amount()
        {
            var expectedAmount = 10;
            var inputItem = new InputItem { MoneyAmount = expectedAmount };
            var mock = new Mock<IExchangeItemRepository>();

            var exchangeService = new ExchangeService(mock.Object);
            var amount = exchangeService.Calculate(inputItem);

            Assert.AreEqual(expectedAmount, amount);
        }

        [TestMethod]
        public void Test_If_Convertion_From_Currency_To_Default_Returns_Correct_Amount()
        {
            var inputItem = new InputItem
            {
                CurrencyFrom = new CurrencyItem { IsoName = CurrencyIso.EUR },
                CurrencyTo = new CurrencyItem { IsoName = CurrencyIso.DKK },
                MoneyAmount = 1
            };
            var mock = new Mock<IExchangeItemRepository>();
            mock.Setup(m => m.GetByCurrencies(It.IsAny<CurrencyItem>(), It.IsAny<CurrencyItem>()))
              .Returns(new ExchangeItem { Amount = 743.94m });
            mock.Setup(m => m.DefaultCurrency)
                .Returns(new CurrencyItem { IsoName = CurrencyIso.DKK });

            var exchangeService = new ExchangeService(mock.Object);
            var amount = exchangeService.Calculate(inputItem);

            Assert.AreEqual(7.4394m, amount);
        }

        [TestMethod]
        public void Test_If_Convertion_From_Default_To_Currency_Returns_Correct_Amount()
        {
            var inputItem = new InputItem
            {
                CurrencyFrom = new CurrencyItem { IsoName = CurrencyIso.DKK },
                CurrencyTo = new CurrencyItem { IsoName = CurrencyIso.EUR },
                MoneyAmount = 1
            };
            var mock = new Mock<IExchangeItemRepository>();
            mock.Setup(m => m.GetByCurrencies(It.IsAny<CurrencyItem>(), It.IsAny<CurrencyItem>()))
              .Returns(new ExchangeItem { Amount = 743.94m });
            mock.Setup(m => m.DefaultCurrency)
              .Returns(new CurrencyItem { IsoName = CurrencyIso.DKK });

            var exchangeService = new ExchangeService(mock.Object);
            var amount = exchangeService.Calculate(inputItem);

            Assert.AreEqual(0.1344m, amount);
        }

        [TestMethod]
        public void Test_If_Convertion_From_Currency_To_Currency_Returns_Correct_Amount()
        {
            var itemsAmount = 2;
            var inputItem = new InputItem
            {
                CurrencyFrom = new CurrencyItem { IsoName = CurrencyIso.EUR },
                CurrencyTo = new CurrencyItem { IsoName = CurrencyIso.USD },
                MoneyAmount = itemsAmount
            };
            var mock = new Mock<IExchangeItemRepository>();
            mock.Setup(m => m.GetByCurrencies(It.Is<CurrencyItem>(i => i.IsoName == CurrencyIso.EUR),
                                              It.Is<CurrencyItem>(i => i.IsoName == CurrencyIso.DKK)))
                .Returns(new ExchangeItem { Amount = 743.94m * itemsAmount });
            mock.Setup(m => m.GetByCurrencies(It.Is<CurrencyItem>(i => i.IsoName == CurrencyIso.USD),
                                              It.Is<CurrencyItem>(i => i.IsoName == CurrencyIso.DKK)))
                .Returns(new ExchangeItem { Amount = 663.11m * itemsAmount });
            mock.Setup(m => m.DefaultCurrency)
                .Returns(new CurrencyItem { IsoName = CurrencyIso.DKK });

            var exchangeService = new ExchangeService(mock.Object);
            var amount = exchangeService.Calculate(inputItem);

            Assert.AreEqual(2.2438m, amount);
        }
    }
}
