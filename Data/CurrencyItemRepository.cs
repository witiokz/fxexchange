using Exchange.Data.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Exchange.Data
{
    public class CurrencyItemRepository : ICurrencyItemRepository
    {
        private static IReadOnlyList<CurrencyItem> _currencies
        {
            get
            {
                return new CurrencyItem[]
                {
                    new CurrencyItem
                    {
                        Name = "Euro",
                        IsoName = CurrencyIso.EUR
                    },
                    new CurrencyItem
                    {
                        Name = "Amerikanske dollar",
                        IsoName = CurrencyIso.USD
                    },
                    new CurrencyItem
                    {
                        Name = "Britiske pund",
                        IsoName = CurrencyIso.GBP
                    },
                    new CurrencyItem
                    {
                        Name = "Svenske kroner",
                        IsoName = CurrencyIso.SEK
                    },
                    new CurrencyItem
                    {
                        Name = "Norske kroner",
                        IsoName = CurrencyIso.NOK
                    },
                    new CurrencyItem
                    {
                        Name = "Schweiziske franc",
                        IsoName = CurrencyIso.CHF
                    },
                    new CurrencyItem
                    {
                        Name = "Japanske yen",
                        IsoName = CurrencyIso.JPY
                    },
                    new CurrencyItem
                    {
                        Name = "Danish kroner",
                        IsoName = CurrencyIso.DKK
                    }
                };
            }
        }

        public IReadOnlyList<CurrencyItem> GetAll()
        {
            return _currencies;
        }

        public CurrencyItem GetByIsoName(CurrencyIso isoName)
        {
            return _currencies.FirstOrDefault(i => i.IsoName == isoName);
        }
    }
}
