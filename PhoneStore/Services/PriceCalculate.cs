
namespace PhoneStore.Services
{
    public class PriceCalculate : ICalculate 
    {
        public double CalculatePrice(double phoneOnePrice, double phoneSecondPrice)
        {
            return phoneOnePrice - phoneSecondPrice;
        }
    }
}