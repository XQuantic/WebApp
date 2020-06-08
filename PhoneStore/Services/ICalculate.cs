using System.Threading.Tasks;
using PhoneStore.ViewModels;

namespace PhoneStore.Services
{
    public interface ICalculate
    {
        Task<double> CalculatePrice(NamePhones phones);
    }
}