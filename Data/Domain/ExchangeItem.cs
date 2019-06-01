namespace Exchange.Data.Domain
{
    public class ExchangeItem
    {
        public CurrencyItem CurrencyFrom { get; set; }

        public CurrencyItem CurrencyTo { get; set; }

        public decimal Amount { get; set; }
    }
}
