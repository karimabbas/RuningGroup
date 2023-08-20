using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocilaMediaProject.Data;
using SocilaMediaProject.Interfaces;
using SocilaMediaProject.Models;
using SocilaMediaProject.ViewModels;

namespace SocilaMediaProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManger = userManager;
            _signInManager = signInManager;

        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            var user = await _userManger.FindByEmailAsync(registerVM.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "This Email Address is Already Registerd Before";
                return View("Login");
            }
            var newUser = new AppUser()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.UserName,
            };
            var result = await _userManger.CreateAsync(newUser, registerVM.Password);
            await _signInManager.SignInAsync(newUser, isPersistent: false);

            if (result.Succeeded)
            {
                await _userManger.AddToRoleAsync(newUser, UserRole.User);

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return RedirectToAction("Index", "Race");

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {

     
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManger.FindByEmailAsync(loginVM.EmailAddress);

            if (user != null)
            {
                //user is Found so Chechk password
                var password = await _userManger.CheckPasswordAsync(user, loginVM.Password);
                if (password)
                {
                    // password is corecct ===>>> sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Club");
                    }
                    TempData["Error"] = "Checking non Correcct , Try again";
                    return View(loginVM);

                }
                TempData["Error"] = "Wrong Password, Please Try Again";
                HttpContext.Session.SetString("mySesstion", user.UserName);
                return View(loginVM);

            }
            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginVM);
        }

        [HttpGet]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Race");
        }



        [HttpGet]
        [Route("Account/Welcome")]
        public async Task<IActionResult> Welcome(int page = 0)
        {
            if (page == 0)
            {
                return View();
            }
            return View();

        }















        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}