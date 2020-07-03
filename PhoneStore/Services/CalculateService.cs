using System.Threading.Tasks;
using PhoneStore.Models;
using PhoneStore.ViewModels;

namespace PhoneStore.Services
{
    public class CalculateService : ICalculateService
    {
        private readonly IRepository _repository;

        public CalculateService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<double> CalculatePrice(NamePhones phones)
        {
            Phone phoneOnePrice = await _repository.GetPhone(phones.NameOnePhone);
            Phone phoneSecondPrice = await _repository.GetPhone(phones.NameSecondPhone);
            if (phoneOnePrice == null || phoneSecondPrice == null) return -1;
            return phoneOnePrice.Price - phoneSecondPrice.Price;
        }
    }
}