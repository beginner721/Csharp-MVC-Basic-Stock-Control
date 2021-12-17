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
            var checkId = context.Users.Any(u => u.Username == user.Username);
            if (checkId)
            {
                var checkPass = context.Users.Any(u => u.Password == user.Password);
                if (checkPass)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.Username)
                    };
                    var userId = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userId);
                    await HttpContext.SignInAsync(principal);

                   HttpContext.Session.SetString("ssUserId", (user.Id).ToString());
                   HttpContext.Session.SetString("ssUsername", (user.Username).ToString());
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ViewBag.Error = Messages.LoginError;
                }
            }
            else
            {
                ViewBag.Error = Messages.LoginError;
            }
            return View();
            //var check = context.Users.Where(u => u.Username == user.Username && u.Password == user.Password);
            //if (check != null)
            //{
            //    HttpContext.Session.SetString("ssUserId", (user.Id).ToString());
            //    HttpContext.Session.SetString("ssUsername", (user.Username).ToString());
            //    return RedirectToAction("Index", "Category");
            //}
            //else
            //{
            //    ViewBag.Error = Messages.LoginError;
            //}
            //return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(User user)
        {

            if (context.Users.Any(u=> u.Username== user.Username))
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
                return RedirectToAction("Index","Category");
            }
        }
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
            
        }


    }
}
