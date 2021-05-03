using Booking.Models;
using Booking.Services.Reservation;
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

        private readonly bookingContext bookingContext;

        private readonly IReserve reserve;

        public BookingController(bookingContext context, IReserve _reserve)
        {
            bookingContext = context;
            reserve = _reserve;
        }
        // 
        // GET: /Booking/
        public IActionResult Index(string city, DateTime beginDate, DateTime endDate, int seats)
        {
            Date.beginDate = beginDate;
            Date.endDate = endDate;
            return View(bookingContext.Hotels.ToList());
        }
        // 
        // GET: /Booking/Details
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
                return NotFound();

                var hotel = await bookingContext.Hotels.FirstOrDefaultAsync(m => m.Id == id);
                if(hotel == null)
                    return NotFound();
                return View(hotel);
        }


        public async Task<IActionResult> DetailsOfRoom(int? id)
        {
            if (id == null)
                return NotFound();

            var room = await bookingContext.Rooms.FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
                return NotFound();
            return View(room);
        }

        public IActionResult Reservation(int? id)
        {
            if(User.Identity.Name != null)
            {
                if(id == null)
                return NotFound();

                Console.WriteLine(User.Identity.Name);


                    return View(bookingContext.Hotels.FirstOrDefault(h => h.Id == id));
            }
            else
            {
                return RedirectToRoute(new {controller = "UsersAuth", action = "SignInIndex"});
            }
            
        }

        [HttpPost]
        public IActionResult Reserve(int? id)
        {
            if (User.Identity.Name != null)
            {
                reserve.Reserve(id, bookingContext, User.Identity.Name);
                return RedirectToAction("Index"); //TODO: Подумать куда рекдиректить пользователя
            }
            else
            {
                return RedirectToRoute(new { controller = "UsersAuth", action = "SignInIndex" });
            }

        }

        public IActionResult RoomsStatus(int hotelId)
        {

                return View(bookingContext.Rooms.Where(r => r.HId == hotelId).ToList());
        }
    }
}