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

        private readonly bookingContext _bookingContext;

        private readonly IReserve _reserve;

        public BookingController(bookingContext context, IReserve reserve)
        {
            
            _bookingContext = context;
            _reserve = reserve;
        }
        // 
        // GET: /Booking/
        public IActionResult Index(string city, DateTime beginDate, DateTime endDate, int seats)
        {
            Date.beginDate = beginDate;
            Date.endDate = endDate;
            
            var query = from h in _bookingContext.Hotels.Where(h => h.City == city)
                join r in _bookingContext.Rooms.Where(r => r.Seats == seats) on h.Id equals r.HId
                join b in _bookingContext.Bookings.Where(b => b.Begindate <= Date.beginDate && b.Enddate >= Date.endDate) on r.Id equals b.Idofroom
                select new 
                {
                    Name = h.Name,
                    City = h.City,
                    Description = h.Description
                };
            return View(query);
        }
        // 
        // GET: /Booking/Details
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
                return NotFound();

                var hotel = await _bookingContext.Hotels.FirstOrDefaultAsync(m => m.Id == id);
                if(hotel == null)
                    return NotFound();
                return View(hotel);
        }


        public async Task<IActionResult> DetailsOfRoom(int? id)
        {
            if (id == null)
                return NotFound();

            var room = await _bookingContext.Rooms.FirstOrDefaultAsync(m => m.Id == id);
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


                    return View(_bookingContext.Hotels.FirstOrDefault(h => h.Id == id));
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
                _reserve.Reserve(id, _bookingContext, User.Identity.Name);
                return RedirectToAction("Index"); //TODO: Подумать куда рекдиректить пользователя
            }
            else
            {
                return RedirectToRoute(new { controller = "UsersAuth", action = "SignInIndex" });
            }

        }

        public IActionResult RoomsStatus(int hotelId)
        {

                return View(_bookingContext.Rooms.Where(r => r.HId == hotelId).ToList());
        }
    }
}