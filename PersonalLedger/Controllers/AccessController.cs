using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonalLedger.Models;

namespace PersonalLedger.Controllers
{
    public class AccessController : Controller
    {
        private readonly ILogger _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccessController(ILogger<AccessController> logger, RoleManager<IdentityRole> roleManager, SignInManager < IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }  //ctor

        [HttpGet]
        public IActionResult AccessDenied()  //Identity middleware automatically redirects to this view
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            SeedDatabaseAsync();
            if (_signInManager.IsSignedIn(User))
            {
                _signInManager.SignOutAsync();
            }
            return View();
        }  //Login[Get]

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Main", "Home");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }  //Login[Post]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }  //Logout

        [HttpGet]
        public IActionResult Register()
        {
            SeedDatabaseAsync();
            return View();
        }  //Register[Get]

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = model.UserName,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (model.Administrator)
                    {
                        await _userManager.AddToRoleAsync(user, "Administrator");
                    }
                    return RedirectToAction("Login", "Access");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }  //Register[Post]

        private async Task SeedDatabaseAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                _logger.LogInformation("Seeding roles in database.");
                IdentityRole role = new IdentityRole("Administrator");
                await _roleManager.CreateAsync(role);
            }
            if (!_userManager.Users.Any())
            {
                _logger.LogInformation("Seeding first user in database.");
                IdentityUser user = new IdentityUser("admin");
                await _userManager.CreateAsync(user, "Password_1");
                await _userManager.AddToRoleAsync(user, "Administrator");
            }
        }  //SeedUsersAsync
    }  //controller
}  //namespace
