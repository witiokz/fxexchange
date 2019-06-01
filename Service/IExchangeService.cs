using Exchange.Data.Domain;

namespace Exchange.Service
{
    public interface IExchangeService
    {
        decimal Calculate(InputItem inputItem);
    }
}