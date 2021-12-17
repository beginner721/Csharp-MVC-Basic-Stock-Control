using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcBasicStockControl.Constants;
using MvcBasicStockControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcBasicStockControl.Controllers
{
    public class AdminController : Controller
    {
        MvcWorkContext context = new MvcWorkContext();

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            var currentUser = context.Users.FirstOrDefault(u => u.Username == user.Username);
            if (currentUser == null)
            {
                ViewBag.Error = Messages.LoginError;
            }
            else
            {
                if (user.Password == currentUser.Password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.Username)
                    };
                    var userId = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userId);
                    await HttpContext.SignInAsync(principal);

                    HttpContext.Session.SetString("ssUserId", (currentUser.Id).ToString());
                    HttpContext.Session.SetString("ssUsername", (user.Username).ToString());
                    return RedirectToAction("Index", "Category");
                }
                ViewBag.Error = Messages.LoginError;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(User user)
        {

            if (context.Users.Any(u => u.Username == user.Username))
            {
                ViewBag.Notification = "This account already existed.";
                return View();
            }
            else
            {
                context.Users.Add(user);
                context.SaveChanges();
                HttpContext.Session.SetString("ssUserId", (user.Id).ToString());
                HttpContext.Session.SetString("ssUsername", (user.Username).ToString());

                //ViewBag.Session = HttpContext.Session.GetString("ssUserId");
                return RedirectToAction("Index", "Category");
            }
        }
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));

        }


        public IActionResult Details(int id)
        {
            var user = context.Users.Find(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Details(User user)
        {
            var oldPass = context.Users.Find(user.Id).Password;

            if (user.OldPass == oldPass)
            {
                MvcWorkContext context2 = new MvcWorkContext();//we need new context because old context is on tracking
                context2.Users.Update(user);
                context2.SaveChanges();
                TempData["PasswordError"] = "Password update successful.";

            }
            else
            {

                ViewBag.PasswordError = "Old password not correct.";
                return View(user);

            }

            return RedirectToAction(nameof(Details));
        }
    }
}
