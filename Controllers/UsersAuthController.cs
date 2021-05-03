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
using Booking.Models.UserWithBookingModel;
using Booking.Services.ProfileService;

namespace Booking.Controllers.UsersAuth
{
    public class UsersAuth : Controller
    {


        private readonly bookingContext bookingContext;

        private readonly IProfile profile;

        public UsersAuth(bookingContext context, IProfile _profile)
        {
            bookingContext = context;
            profile = _profile;
        }
        // 

        public IActionResult SignUpIndex()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(string name, string login, string password)
        {
                if(bookingContext.Users.Any(u => u.Login == login)) return NotFound("User already exists");
                var id = bookingContext.Users.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
                var user = new User { Id = id, Name = name, Login = login, Password = password};
                bookingContext.Users.Add(user);
                bookingContext.SaveChanges();
            return View();
        }
        public IActionResult SignInIndex()
        {
            return View();
        }
        public async Task<IActionResult> SignIn(string login, string password)
        {
                var user = bookingContext.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
                if(user != null)
                {
                    await Authenticate(login);
                    Console.WriteLine(User.Identity.Name);
                    return RedirectToRoute(new { controller = "Home", action= "Index"});   
                }
                else
                    return NotFound("Wrong password or username");
        }
        public IActionResult Profile()
        {
            var us = profile.getBooking(bookingContext, User.Identity.Name);

            if (us != null)
                return View(us);
            return View(new UserBooking(null, null, null, null));
        }
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
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
    }
}