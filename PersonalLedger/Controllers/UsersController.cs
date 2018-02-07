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
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ILogger _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly string _userName;

        public UsersController(UserManager<IdentityUser> userManager, ILogger<AccessController> logger, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _userName = signInManager.Context.User.Identity.Name;
        }  //ctor

        [HttpGet("[action]")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> MakeAdminAsync(string id)
        {
            try
            {
                IdentityUser user = await _userManager.FindByIdAsync(id);
                return Ok(await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator")));
            }
            catch (Exception e)
            {
                HandleException(e, ControllerContext.RouteData.Values["action"].ToString(), "", false);
                return NotFound();
            }
        }  //MakeAdminAsync

        [HttpGet("[action]")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> UnmakeAdminAsync(string id)
        {
            try
            {
                IdentityUser user = await _userManager.FindByIdAsync(id);
                return Ok(await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator")));
            }
            catch (Exception e)
            {
                HandleException(e, ControllerContext.RouteData.Values["action"].ToString(), "", false);
                return NotFound();
            }
        }  //UnmakeAdminAsync

        [HttpGet("[action]")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> UsersAsync()
        {
            //try
            //{
            //    return Ok(await Task.Run(() => _userManager.Users.ToArray()));
            //}
            try
            {
                IdentityUser[] iUsers = await Task.Run(() => _userManager.Users.ToArray());
                int userCount = iUsers.Count();
                AppUser[] aUsers = new AppUser[userCount];
                for (int i = 0; i < userCount; i++)
                {
                    AppUser aUser = new AppUser(iUsers[i]);
                    Debug.WriteLine("-------------------------------------------------");
                    Debug.WriteLine(i + ": User " + iUsers[i].Id);
                    Debug.WriteLine(await Task.Run(() => _userManager.GetRolesAsync(iUsers[i])));
                    Debug.WriteLine(await Task.Run(() => _userManager.IsInRoleAsync(iUsers[i], "Administrator")));
                    Debug.WriteLine("-------------------------------------------------");
                    aUser.Admin = await Task.Run(() => _userManager.IsInRoleAsync(iUsers[i], "Administrator"));
                    aUsers[i] = aUser;
                }
                return Ok(aUsers);
            }
            catch (Exception e)
            {
                HandleException(e, ControllerContext.RouteData.Values["action"].ToString(), "", false);
                return NotFound();
            }
        }  //UsersAsync

        [HttpGet("[action]")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> DeleteUserAsync(string name)
        {
            try
            {
                IdentityUser toDelete = await _userManager.FindByIdAsync(name);
                return Ok(await _userManager.DeleteAsync(toDelete));
            }
            catch (Exception e)
            {
                HandleException(e, ControllerContext.RouteData.Values["action"].ToString(), "", false);
                return NotFound();
            }
        }  //DeleteUserAsync

        #region Infrastructure
        private void HandleException(Exception e, string method, string userMessage, bool redirect)
        {
            _logger.LogError("{0}: An error occurred in UsersController/{1} for user: {2}.\n{3}\n{4}", DateTime.Now, method, _userName, e.Message, userMessage);
            if (redirect)
            {
                RedirectToAction("Error");
            }
        }  //HandleException
        #endregion
    }  //controller
}  //namespace
