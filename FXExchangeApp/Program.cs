using Exchange.Data;
using Exchange.Data.Domain;
using Exchange.Service;
using SimpleInjector;
using System;

namespace FXExchangeApp
{
    class Program
    {
        private static readonly Container container;

        static Program()
        {
            container = new Container();
            container.Register<ICurrencyItemRepository, CurrencyItemRepository>();
            container.Register<IExchangeItemRepository, ExchangeItemRepository>();
            container.Register<IInputProcessService, InputProcessService>();
            container.Register<IExchangeService, ExchangeService>();
        }

        static void Main(string[] args)
        {
            var inputProcessService = container.GetInstance<IInputProcessService>();
            var exchangeService = container.GetInstance<IExchangeService>();

            InputItem inputItem;

            try
            {
                inputItem = inputProcessService.ProcessInputData(args);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return;
            }

            var amount = exchangeService.Calculate(inputItem);

            Console.WriteLine(amount);
            Console.ReadKey();
        }
    }
}
