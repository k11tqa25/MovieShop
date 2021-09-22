using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        //localhost/account/register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }       
        
        // Model Binding: paramter matches the incoming key (not case-sensitive)
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            // Save only if the data pass the model validation
            if (!ModelState.IsValid)
            {
                return View();
            }

            var createdUser = await _userService.RegisterUserAsync(model);

            // Redirect to Login page

            return RedirectToAction(nameof(Login));

        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLoginRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userService.LoginAsync(requestModel.Email, requestModel.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid password");
                return View();
            }

            // correct password
            // display account info,  Logout  button
            // Cookie Based Authentication...
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            // Identity object
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Create the cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            
            // Redirect to home page
            return LocalRedirect("~/");
        }

        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
