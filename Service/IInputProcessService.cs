using Exchange.Data.Domain;

namespace Exchange.Service
{
    public interface IInputProcessService
    {
        InputItem ProcessInputData(string[] inputData);
    }
}