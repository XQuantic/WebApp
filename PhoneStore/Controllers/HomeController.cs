using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;
using PhoneStore.Services;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers
{
    [Authorize(Roles = "admin, user", Policy = "AppleCompany")]
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly ICalculate _calculate;
        

        public HomeController(IRepository repository, ICalculate calculate)
        {
            _repository = repository;
            _calculate = calculate;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Errors(string errorCode)
        {
            return Content($"Status code {errorCode}");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Error()
        {
            return Content("Error");
        }
        
        [HttpPost]
        public async Task<IActionResult> CalcPrice([FromBody] NamePhones phones)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.Conflict;
                return Json("Please fill fields");
            }
            double price = await _calculate.CalculatePrice(phones);
            if (price == -1)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json("Data not found");
            }
            return Json(price + "$");
        }
    }
}