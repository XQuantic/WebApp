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
        public async Task <IActionResult> Index()
        {
            var companies = await _repository.GetCompanies();
            IndexModel indexModel = new IndexModel
            {
                Companies = companies
            };
            return View(indexModel);
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
            var phoneOne = await _repository.GetPhone(phones.NameOnePhone);
            var phoneSecond = await _repository.GetPhone(phones.NameSecondPhone);
            if (phoneOne == null || phoneSecond == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json("Data not found");
            }
            double phoneOnePrice = phoneOne.Price;
            double phoneSecondPrice = phoneSecond.Price;
            double price = _calculate.CalculatePrice(phoneOnePrice, phoneSecondPrice);
            return Json(price);
        }

        [HttpPut]
        public async Task<IActionResult> InsertPhone([FromBody] InsertPhone phone)
        {
            if(!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.Conflict;
                return Json("Please fill fields");
            }
            Phone data = new Phone
            {
                CompanyId = phone.CompanyPhone,
                Country = phone.CountryPhone,
                Name = phone.NamePhone,
                Price = phone.PricePhone
            };
            await _repository.InsertPhone(data);
            return Json("Insert completed");
        }
    }
}