using Booking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Booking.Controllers.Booking
{
    public class BookingController : Controller
    {
        // 
        // GET: /Booking/
        public IActionResult Index(string city)
        {
            if(String.IsNullOrEmpty(city))
            {
                return NotFound("ERROR: Please enter hotel name in search textarea");
            }
            using(var context = _contextFactory.CreateDbContext())
            {
                return View(context.Hotels.Where(h => h.City == city).ToList());
            }
        }
        // 
        // GET: /Booking/Details
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
                return NotFound();
            using( var context = _contextFactory.CreateDbContext())
            {
                var hotel = await context.Hotels.FirstOrDefaultAsync(m => m.Id == id);
                if(hotel == null)
                    return NotFound();
                return View(hotel);
            }
        }

        public IActionResult Reservation(int? id)
        {
            if(User.Identity.Name != null)
            {
                if(id == null)
                return NotFound();

                Console.WriteLine(User.Identity.Name);

                using( var context = _contextFactory.CreateDbContext())
                {
                    return View(context.Hotels.FirstOrDefault(h => h.Id == id));
                }
            }
            else
            {
                return RedirectToRoute(new {controller = "UsersAuth", action = "SignInIndex"});
            }
            
        }
         public IActionResult RoomsStatus(int hotelId)
        {
            using( var context = _contextFactory.CreateDbContext())
            {
                return View(context.Rooms.Where(r => r.H_id == hotelId).ToList());
            }
        }
        private readonly IDbContextFactory<BookingContext> _contextFactory;

        public BookingController(IDbContextFactory<BookingContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}