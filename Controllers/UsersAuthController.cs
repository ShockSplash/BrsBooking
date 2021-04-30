using Booking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Booking.Models;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Booking.Controllers.UsersAuth
{
    public class UsersAuth : Controller
    {
        public IActionResult SignUpIndex()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(string name, string login, string password)
        {
            using( var context = _contextFactory.CreateDbContext())
            {
                if(context.Users.Any(u => u.Login == login)) return NotFound("User already exists");
                var id = context.Users.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
                var user = new Users { Id = id, Name = name, Login = login, Password = password};
                context.Users.Add(user);
                context.SaveChanges();
            }
            return View();
        }
        public IActionResult SignInIndex()
        {
            return View();
        }
        public async Task<IActionResult> SignIn(string login, string password)
        {
            using( var context = _contextFactory.CreateDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Login == login);
                if(user != null)
                {
                    await Authenticate(login, password);
                    Console.WriteLine(User.Identity.Name);
                    return RedirectToRoute(new { controller = "Home", action= "Index"});   
                }
                else
                    return NotFound("Wrong password or username");
            }
        }
        public IActionResult Profile()
        {
            var userName = User.Identity.Name;
            Console.WriteLine(userName);
            if(userName != null)
            {
                using(var context = _contextFactory.CreateDbContext())
                {
                    Console.WriteLine(context.Users.FirstOrDefault(u => u.Login == userName).Name);
                    return View(context.Users.FirstOrDefault(u => u.Login == userName));
                }
            }
            return RedirectToRoute(new {controller = "UsersAuth", action = "SignInIndex"});
        }
        private async Task Authenticate(string userName, string userPassword)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultNameClaimType, userPassword)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
 
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }
        private readonly IDbContextFactory<BookingContext> _contextFactory;
        public UsersAuth(IDbContextFactory<BookingContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}