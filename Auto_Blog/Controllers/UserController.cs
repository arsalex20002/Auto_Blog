using Auto_Blog.Domain.Enum;
using Auto_Blog.Domain.ViewModels.User;
using Auto_Blog.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auto_Blog.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IPostService postService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.Status == ErrorStatus.Success)
            {
                return RedirectToAction("GetPosts", "Post");
            }
            return View("Error", $"{response.Description}");
        }

        public async Task<IActionResult> UserPanel()
        {
            var response = await _userService.GetUsers();
            if (response.Status == ErrorStatus.Success)
            {
                return View(response.Data.ToList());
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> Account()
        {
            if (User.Identity.IsAuthenticated)
            {
                var responce = await _userService.GetUser(User.Identity.Name);
                if (responce.Status == ErrorStatus.Success)
                {
                    return View(responce.Data);
                }
                else
                {
                    return View();
                }

            }
            return View("Error", "Ошибка доступа к форме");
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return View("Error", "Ошибка доступа к форме");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Login(model);
                if (response.Status == Domain.Enum.ErrorStatus.Success)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("GetPosts", "Post");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return View("Error", "Ошибка доступа к форме");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Register(model);
                if (response.Status == ErrorStatus.Success)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("GetPosts", "Post");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("GetPosts", "Post");
        }
    }
}
