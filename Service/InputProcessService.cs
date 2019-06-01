using Exchange.Data;
using Exchange.Data.Domain;
using System;

namespace Exchange.Service
{
    public class InputProcessService : IInputProcessService
    {
        private readonly ICurrencyItemRepository _currencyItemRepository;

        public InputProcessService(ICurrencyItemRepository currencyItemRepository)
        {
            _currencyItemRepository = currencyItemRepository;
        }

        public InputItem ProcessInputData(string[] inputData)
        {
            ValidateInputDataLength(inputData);
            var currencyPairParts = inputData[0].Split('/');
            ValidateCurrencyPairPartsLength(currencyPairParts);

            Enum.TryParse(currencyPairParts[0], out CurrencyIso currencyFromIso);
            Enum.TryParse(currencyPairParts[1], out CurrencyIso currencyToIso);
            decimal.TryParse(inputData[1], out decimal amount);

            ValidateAmount(amount);
            ValidateCurrencies(currencyFromIso, currencyToIso);

            return new InputItem
            {
                CurrencyFrom = _currencyItemRepository.GetByIsoName(currencyFromIso),
                CurrencyTo = _currencyItemRepository.GetByIsoName(currencyToIso),
                MoneyAmount = amount
            };
        }

        private static void ValidateInputDataLength(string[] inputData)
        {
            if (inputData.Length != 2)
            {
                throw new Exception("Usage: Exchange <currency pair> <amount to exchange>");
            }
        }

        private static void ValidateCurrencyPairPartsLength(string[] currencyPairParts)
        {
            if (currencyPairParts.Length != 2)
            {
                throw new Exception("Currency pair not well formatted. Example DKK/EUR");
            }
        }

        private static void ValidateAmount(decimal amount)
        {
            if (amount == 0)
            {
                throw new Exception("Currency amount should be a number");
            }
        }

        private static void ValidateCurrencies(CurrencyIso currencyFromIso, CurrencyIso currencyToIso)
        {
            if (currencyFromIso == CurrencyIso.NONE || currencyToIso == CurrencyIso.NONE)
            {
                throw new Exception($"Currency pair should contains one of these: {string.Join(",", Enum.GetNames(typeof(CurrencyIso)))};  ");
            }
        }
    }
}
