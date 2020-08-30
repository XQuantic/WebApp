using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;
using PhoneStore.Services;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly ICalculateService _calculate;
        private readonly IRepository _repository;
        public PhoneController(ICalculateService calculate, IRepository repository)
        {
            _calculate = calculate;
            _repository = repository;
        }

        [HttpPost("CalcPrice")]
        [Produces("application/json")]
        public async Task<IActionResult> CalcPrice([FromBody] NamePhones phones)
        {
            double price = await _calculate.CalculatePrice(phones);
            if (price == -1)
            {
                return NotFound("Data not found");
            }
            return Ok(price);
        }

        [HttpDelete("DeletePhone/{id}")]
        public async Task<IActionResult> DeletePhone([FromRoute] int id)
        {
            Phone data = await _repository.GetPhoneId(id);
            if (data == null) return NotFound();
            await _repository.RemovePhone(data);
            return Ok();
        }

        [HttpPost("InsertPhone")]
        public async Task<IActionResult> InsertPhone([FromBody] Phone phone)
        {
            await _repository.SavePhone(phone);
            return Ok(new { Message = "Insert complete" });
        }
    }
}
