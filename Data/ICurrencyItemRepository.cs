using System.Collections.Generic;
using Exchange.Data.Domain;

namespace Exchange.Data
{
    public interface ICurrencyItemRepository
    {
        IReadOnlyList<CurrencyItem> GetAll();

        CurrencyItem GetByIsoName(CurrencyIso isoName);
    }
}