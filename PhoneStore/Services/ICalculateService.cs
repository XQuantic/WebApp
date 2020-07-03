using System.Threading.Tasks;
using PhoneStore.ViewModels;

namespace PhoneStore.Services
{
    public interface ICalculateService
    {
        Task<double> CalculatePrice(NamePhones phones);
    }
}