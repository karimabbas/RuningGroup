using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
        private readonly ISendGridEmail _sendGridEmail;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ISendGridEmail sendGridEmail)
        {
            _userManger = userManager;
            _signInManager = signInManager;
            _sendGridEmail = sendGridEmail;

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
            return RedirectToAction("Index", "Club");

        }


        [HttpGet]
        public IActionResult Login([FromQuery] string? ReturnUrl)
        {
            this.ViewData["Url"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            //   loginVM.ReturnUrl = returnUrl;
            // returnUrl = returnUrl ?? Url.Content("~/");

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
                        if (Url.IsLocalUrl(loginVM.ReturnUrl))
                            return Redirect(loginVM.ReturnUrl);
                        else
                            return RedirectToAction("Index", "Home");

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

        [HttpPost]
        public IActionResult ExternalLogin(string provider)
        {
            var redirecturl = Url.Action("ExternalLoginCallback", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirecturl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError("", $"Error Form External provider{remoteError}");
                return View("Login", "Account");
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) return RedirectToAction("Login");

            //if user already has login and need to login with this provider
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                /////Update user Auth Tokens
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                return RedirectToAction("Index", "Club");
            }
            else
            {
                // if user Dose not have account and need to use External login to login and register
                ViewData["ProviderDisplayName"] = info.ProviderDisplayName;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLoginConfirmation", new ExternalLoginViewModel { Email = email });

            }

        }

        [HttpPost]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel externalModel)
        {
            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null) return View("Error");

                var User = new AppUser
                {
                    UserName = externalModel.Name,
                    Email = externalModel.Email
                };
                var result = await _userManger.CreateAsync(User);
                if (result.Succeeded)
                {
                    await _userManger.AddToRoleAsync(User, UserRole.User);
                    result = await _userManger.AddLoginAsync(User, info);
                    await _userManger.AddClaimAsync(User, new Claim(ClaimTypes.MobilePhone,"01151557900"));
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(User, isPersistent: false);
                        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                        return RedirectToAction("Index", "Club");
                    }
                }
                ModelState.AddModelError("Email", "Error occuresd");

            }
            return View(externalModel);

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Race");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManger.FindByEmailAsync(forgotPasswordVM.Email);
                if (user == null)
                {
                    return RedirectToAction("CheckEmail");
                }
                var code = await _userManger.GeneratePasswordResetTokenAsync(user);
                var callbackurl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                await _sendGridEmail.SendEmailAsync(forgotPasswordVM.Email, "Reset Email Confirmation",
                "Please Reset Email Password by Going to <a href=\"" + callbackurl + "\" >Link</a>");
                return RedirectToAction("CheckEmail");

            }
            return View(forgotPasswordVM);
        }

        [HttpGet]
        public IActionResult CheckEmail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string? code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManger.FindByEmailAsync(resetVM.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "User not Found");
                    return View();
                }
                var result = await _userManger.ResetPasswordAsync(user, resetVM.Code, resetVM.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
            }
            return View(resetVM);
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
