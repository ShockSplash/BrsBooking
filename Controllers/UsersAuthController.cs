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
using Booking.Services.SignUp;

namespace Booking.Controllers.UsersAuth
{
    public class UsersAuth : Controller
    {


        private readonly bookingContext _bookingContext;

        private readonly IProfile profile;

        private readonly ISign _sign;

        public UsersAuth(bookingContext context, IProfile _profile, ISign sign)
        {
            _bookingContext = context;
            profile = _profile;
            _sign = sign;
        }
        // 

        public IActionResult SignUpIndex()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(string name, string login, string password)
        {
            if(_bookingContext.Users.Any(u => u.Login == login))
                    return NotFound("User already exists");

            _sign.SignU(name, login, password, _bookingContext);
            return View();
        }
        public IActionResult SignInIndex()
        {
            return View();
        }
        public async Task<IActionResult> SignIn(string login, string password)
        {
                var user = _bookingContext.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
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
            var us = profile.getBooking(_bookingContext, User.Identity.Name);

            if (us != null)
                return View(us);
            return RedirectToRoute(new { controller = "UsersAuth", action = "SignInIndex" });
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