using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MVSschuh.Data.Entities;
using MVSschuh.Data.Model;
using MVSschuh.Services;
using MVSschuh.View_Model;
using Newtonsoft.Json;

namespace MVSschuh.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly EFContext _context;


        public AccountController(UserManager<User> userManager,
             SignInManager<User> signInManager,
             EFContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpGet]
        [Route("Account/ChangePassword/{id}")]
        public IActionResult ChangePassword(string id)
        {
            return View();
        }
        [HttpPost]
        [Route("Account/ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword(ChangePassword model, string id)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    ModelState.AddModelError("", "This User not registred");
                    return View(model);
                }

                var hashPass = _userManager.PasswordHasher.HashPassword(user, model.Password);
                user.PasswordHash = hashPass;
                var result = await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Login);
            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    ModelState.AddModelError("", "This Email not registred");
                    return View(model);
                }
                EmailService service = new EmailService();
                var email = user.Email;
                string url = "http://localhost:50740/Account/ChangePassword/" + user.Id;
                var username = user.UserName;
                await  service.SendEmailAsync(email, "Forgot Password",
                    $"Dear {username}," +
                    $"<br/>" +
                    $"To change your password " +
                    $"you should visit this link: <a href='{url}'>Click</a>");
            }
            return RedirectToAction("Index", "Home");
        }

        //Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Login);
            if (user == null)
            {
                ModelState.AddModelError("", "This Email not registred");
                return View(model);
            }
            var result = _signInManager
                .PasswordSignInAsync(user, model.Password, false, false).Result;
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Wrong Password");
                return View(model);
            }
            await _signInManager.SignInAsync(user, isPersistent: false);

            await Authenticate(model.Login);
            var userInfo = new UserInfo()
            {
                UserId = user.Id,
                Email = user.Email
            };
            HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(userInfo));


            return RedirectToAction("Index", "Home");
        }
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }


        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        //Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tmpUser = _context.Users.FirstOrDefault(x => x.Email == model.Email);
                if (tmpUser != null)
                {
                    ModelState.AddModelError(string.Empty, "This e-mail is already used");
                    return View(model);
                }
                else
                {
                    UserProfile userProfile = new UserProfile
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName
                    };

                    User user = new User
                    {
                        Email = model.Email,
                        UserName = model.Login,
                        UserProfile = userProfile
                    };

                    var rolename = "User";
                    var result = await _userManager.CreateAsync(user, model.Password);
                    result = _userManager.AddToRoleAsync(user, rolename).Result;

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
            
        }

    }
}