﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PhoneStore.ViewModels; 
using PhoneStore.Models; 
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace PhoneStore.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IRepository _repository;

        public AccountController(IRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            if (User.IsInRole("admin")) return RedirectToAction("Index", "Home");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _repository.GetUser(model.Email, model.Password);
                if (user != null)
                {
                    await Authenticate(user, model.Remember);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _repository.GetUser(model.Email);
                if (user == null)
                {
                    user = new User
                    {
                        Email = model.Email, 
                        Password = model.Password,
                        Company = model.Company
                    };
                    Role userRole = await _repository.GetRole("user");
                    if (userRole != null) user.Role = userRole;
                    await _repository.InsertUser(user);
                    return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("BasicScheme");
            return RedirectToAction("Login", "Account");
        }
        
        private async Task Authenticate(User user, bool persistent)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Name", user.Email),
                new Claim("Role", user.Role?.Name),
                new Claim("Company", user.Company)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", "Name", "Role");
            ClaimsPrincipal principal = new ClaimsPrincipal(id);
            AuthenticationProperties properties = new AuthenticationProperties
            {
                IsPersistent = persistent,
            };
            await HttpContext.SignInAsync("BasicScheme", principal, properties);
        }
    }
}