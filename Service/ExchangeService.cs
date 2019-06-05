using Exchange.Data;
using Exchange.Data.Domain;
using System;

namespace Exchange.Service
{
    public class ExchangeService : IExchangeService
    {
        private const int Ratio = 100;

        private readonly IExchangeItemRepository _exchangeItemRepository;

        public ExchangeService(IExchangeItemRepository exchangeItemRepository)
        {
            _exchangeItemRepository = exchangeItemRepository;
        }

        public decimal Calculate(InputItem inputItem)
        {
            if (CurrenciesAreTheSame(inputItem))
            {
                return inputItem.MoneyAmount;
            }

            decimal? priceAmount = 0;

            if (inputItem.CurrencyTo.IsoName == _exchangeItemRepository.DefaultCurrency.IsoName)
            {
                priceAmount = GetAmountFromCurrencyToDefault(inputItem.CurrencyFrom, inputItem.CurrencyTo);
            }
            else if (inputItem.CurrencyFrom.IsoName == _exchangeItemRepository.DefaultCurrency.IsoName)
            {
                priceAmount = GetAmountFromDefaultToCurrency(inputItem.CurrencyTo, inputItem.CurrencyFrom);
            }
            else
            {
                var currencyFromToDefaultCurrencyAmount = GetAmountFromCurrencyToDefault(inputItem.CurrencyFrom, _exchangeItemRepository.DefaultCurrency);

                inputItem = new InputItem
                {
                    CurrencyFrom = _exchangeItemRepository.DefaultCurrency,
                    CurrencyTo = inputItem.CurrencyTo,
                    MoneyAmount = currencyFromToDefaultCurrencyAmount * inputItem.MoneyAmount ?? 0
                };
                return Calculate(inputItem);
            }

            return Math.Round(inputItem.MoneyAmount * priceAmount ?? 0, 4);
        }

        private static bool CurrenciesAreTheSame(InputItem inputItem)
        {
            return inputItem.CurrencyFrom?.IsoName == inputItem.CurrencyTo?.IsoName;
        }

        private decimal? GetAmountFromCurrencyToDefault(CurrencyItem currencyFrom, CurrencyItem currencyTo)
        {
            return _exchangeItemRepository.GetByCurrencies(currencyFrom, currencyTo)?.Amount / Ratio;
        }

        private decimal? GetAmountFromDefaultToCurrency(CurrencyItem currencyFrom, CurrencyItem currencyTo)
        {
            return Ratio / _exchangeItemRepository.GetByCurrencies(currencyFrom, currencyTo)?.Amount;
        }
    }
}
