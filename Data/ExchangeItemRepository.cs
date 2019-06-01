using Exchange.Data.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Exchange.Data
{
    public class ExchangeItemRepository : IExchangeItemRepository
    {
        private readonly ICurrencyItemRepository _currencyItemRepository;

        public ExchangeItemRepository(ICurrencyItemRepository currencyItemRepository)
        {
            _currencyItemRepository = currencyItemRepository;
        }

        private IReadOnlyList<ExchangeItem> _exchanges
        {
            get
            {
                return new ExchangeItem[]
                {
                    new ExchangeItem
                    {
                        CurrencyFrom = _currencyItemRepository.GetByIsoName(CurrencyIso.EUR),
                        CurrencyTo = DefaultCurrency,
                        Amount = 743.94m
                    },
                    new ExchangeItem
                    {
                        CurrencyFrom = _currencyItemRepository.GetByIsoName(CurrencyIso.USD),
                        CurrencyTo = DefaultCurrency,
                        Amount = 663.11m
                    },
                    new ExchangeItem
                    {
                        CurrencyFrom = _currencyItemRepository.GetByIsoName(CurrencyIso.GBP),
                        CurrencyTo = DefaultCurrency,
                        Amount = 852.85m
                    },
                    new ExchangeItem
                    {
                        CurrencyFrom = _currencyItemRepository.GetByIsoName(CurrencyIso.SEK),
                        CurrencyTo = DefaultCurrency,
                        Amount = 76.10m
                    },
                    new ExchangeItem
                    {
                        CurrencyFrom = _currencyItemRepository.GetByIsoName(CurrencyIso.NOK),
                        CurrencyTo = DefaultCurrency,
                        Amount = 78.40m
                    },
                    new ExchangeItem
                    {
                        CurrencyFrom = _currencyItemRepository.GetByIsoName(CurrencyIso.CHF),
                        CurrencyTo = DefaultCurrency,
                        Amount = 683.58m
                    },
                    new ExchangeItem
                    {
                        CurrencyFrom = _currencyItemRepository.GetByIsoName(CurrencyIso.JPY),
                        CurrencyTo = DefaultCurrency,
                        Amount = 5.9740m
                    }
                };
            }
        }

        public CurrencyItem DefaultCurrency
        {
            get
            {
                return _currencyItemRepository.GetByIsoName(CurrencyIso.DKK);
            }
        }

        public ExchangeItem GetByCurrencies(CurrencyItem currencyFrom, CurrencyItem currencyTo)
        {
            return _exchanges.FirstOrDefault(i => i.CurrencyFrom?.IsoName == currencyFrom?.IsoName && i.CurrencyTo?.IsoName == currencyTo?.IsoName);
        }


    }
}
