using Exchange.Data.Domain;

namespace Exchange.Data
{
    public interface IExchangeItemRepository
    {
        CurrencyItem DefaultCurrency { get; }

        ExchangeItem GetByCurrencies(CurrencyItem currencyFrom, CurrencyItem currencyTo);
    }
}