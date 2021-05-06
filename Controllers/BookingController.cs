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

            if(beginDate > endDate)
                return NotFound("Please enter the right order of dates");
            if(beginDate < DateTime.Now.Date)
                return NotFound("Please enter the newest date");

            SeatsCheck.seats = seats;

            var booking = new UserBooking(beginDate, endDate);
            UserBooking.bd = beginDate;
            UserBooking.ed = endDate;

            var query = from h in _bookingContext.Hotels
                        .Where(h => h.City == city )
                join r in _bookingContext.Rooms
                        .Where(r => r.Seats == seats) 
                    on h.Id equals r.HId
                join b in _bookingContext.Bookings
                    on r.Id equals b.Idofroom
                select new JoinResult
                (
                    h.Id,
                    r.Id,
                    h.Name,
                    h.City,
                    h.Description,
                    b.Begindate,
                    b.Enddate
                );

            var result = query.AsEnumerable().Where(q => q.Id > 0).ToList();

            foreach(var item in result)
            {
                if( item.Begindate <= booking.Enddate && item.Enddate >= booking.Begindate)
                    booking.IsFree = false;
                else
                {
                    booking.Ids.Add(new KeyValuePair<int, int>(item.Id, item.Idofroom));
                }
            }
            

            if(booking.IsFree == false)
                return NotFound("No hotel on that time");
            else
            {
                var bookings = new List<UserBooking>();   
                foreach(var item in booking.Ids)
                {
                    Console.WriteLine("{0} {1}", item.Key, item.Value);
                    var b = new UserBooking(beginDate, endDate);
                    var bContext = _bookingContext.Hotels.FirstOrDefault(h => h.Id == item.Key);
                    b.Name = bContext.Name;
                    b.City = bContext.City;
                    b.Description = bContext.Description;
                    b.Id = item.Key;

                    bookings.Add(b);
                }
                return View(bookings);
            }
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

        [HttpPost]
        public IActionResult Reserve(int? id)
        {
            if (User.Identity.Name != null)
            {
                _reserve.Reserve(id, _bookingContext, User.Identity.Name);
                return RedirectToRoute(new { controller = "UsersAuth", action = "Profile"}); 
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