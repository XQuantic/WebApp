using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneStore.Models;
using PhoneStore.Services;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICalculate _calculate;


        public ValuesController(ICalculate calculate)
        {
            _calculate = calculate;
        }

        [HttpPost("calcPrice")]
        public async Task<IActionResult> CalcPrice([FromBody] NamePhones phones)
        {
            string result;
            double price = await _calculate.CalculatePrice(phones);
            if (price == -1)
            {
                result = JsonConvert.SerializeObject("Data not found");
                return NotFound(result);
            }
            result = JsonConvert.SerializeObject(price + "$");
            return Ok(result);
        }
    }
}
