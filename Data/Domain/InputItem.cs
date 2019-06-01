namespace Exchange.Data.Domain
{
    public class InputItem
    {
        public CurrencyItem CurrencyFrom { get; set; }

        public CurrencyItem CurrencyTo { get; set; }

        public decimal MoneyAmount { get; set; }
    }
}
