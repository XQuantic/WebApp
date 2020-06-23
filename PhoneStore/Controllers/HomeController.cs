using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;
using PhoneStore.Services;
using PhoneStore.ViewModels;

namespace PhoneStore.Controllers
{
    [Authorize(Roles = "admin, user", Policy = "AppleCompany")]
    public class HomeController : Controller
    {

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
        
    }
}