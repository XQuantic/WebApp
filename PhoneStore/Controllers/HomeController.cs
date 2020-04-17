using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneStore.Models;
using PhoneStore.Services;

namespace PhoneStore.Controllers
{
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
        public IActionResult Index()
        {
            var result = _db.Phones.Include(x => x.Company).ToList();
            ViewBag.Company = _db.Companies.Select(x => x);
            return View(result);
        }

        [HttpPost]
        public IActionResult CalcPrice([FromBody] NamePhones phones)
        {
            var phoneOne = _db.Phones.FirstOrDefault(x => x.Name == phones.NameOnePhone);
            var phoneSecond = _db.Phones.FirstOrDefault(x => x.Name == phones.NameSecondPhone);
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
        public IActionResult Delete([FromForm] string nameToDelete)
        {
            Phone phone = _db.Phones.FirstOrDefault(x => x.Name == nameToDelete);
            if (phone == null)
            {
                return RedirectToAction("Index");
            }
            _db.Phones.Remove(phone);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpPut]
        public IActionResult InsertPhone([FromBody] InsertPhone phone)
        {
            if(String.IsNullOrEmpty(phone.NamePhone))
            {
                Response.StatusCode = (int)HttpStatusCode.Conflict;
                return Json("Please fill name field");
            }
            Phone data = new Phone() {CompanyId = phone.CompanyPhone, Country = phone.CountryPhone, Name = phone.NamePhone, Price = phone.PricePhone};
            _db.Phones.Add(data);
            _db.SaveChanges();
            return Json("Insert completed");
        }
    }
}