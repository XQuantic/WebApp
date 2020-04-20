using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Models;
using PhoneStore.Services;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private readonly MyDbContext _db;
        private readonly ICalculate _calculate;
        

        public HomeController(MyDbContext context, ICalculate calculate)
        {
            _db = context;
            _calculate = calculate;
        }
        
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var result = await _db.Phones.Include(x => x.Company).ToListAsync();
            ViewBag.Company = _db.Companies.Select(x => x);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> CalcPrice([FromBody] NamePhones phones)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.Conflict;
                return Json("Please fill fields");
            }
            var phoneOne = await _db.Phones.FirstOrDefaultAsync(x => x.Name == phones.NameOnePhone);
            var phoneSecond = await _db.Phones.FirstOrDefaultAsync(x => x.Name == phones.NameSecondPhone);
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
        
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] string nameToDelete)
        {
            Phone phone = await _db.Phones.FirstOrDefaultAsync(x => x.Name == nameToDelete);
            if (phone == null)
            {
                return RedirectToAction("Index");
            }
            _db.Phones.Remove(phone);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        [HttpPut]
        public async Task<IActionResult> InsertPhone([FromBody] InsertPhone phone)
        {
            if(!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.Conflict;
                return Json("Please fill fields");
            }
            Phone data = new Phone() {CompanyId = phone.CompanyPhone, Country = phone.CountryPhone, Name = phone.NamePhone, Price = phone.PricePhone};
            await _db.Phones.AddAsync(data);
            await _db.SaveChangesAsync();
            return Json("Insert completed");
        }
    }
}