using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneStore.Services;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICalculateService _calculate;
        public ValuesController(ICalculateService calculate)
        {
            _calculate = calculate;
        }

        [HttpPost("calcPrice")]
        [Produces("application/json")]
        public async Task<IActionResult> CalcPrice([FromBody] NamePhones phones)
        {
            double price = await _calculate.CalculatePrice(phones);
            if (price == -1)
            {
                return NotFound();
            }
            return Ok(price);
        }
    }
}
