using System.Threading.Tasks;
using PhoneStore.Models;
using PhoneStore.ViewModels;

namespace PhoneStore.Services
{
    public class PriceCalculate : ICalculate
    {
        private readonly IRepository _repository;

        public PriceCalculate(IRepository repository)
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