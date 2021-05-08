using Booking.Models;
using Booking.Services.Reservation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if(city == null)
                return View("ErrorMessage", new Error("Please enter the city name"));
            if(_bookingContext.Rooms.AsEnumerable().Select(r => r.Seats).DefaultIfEmpty(0).Max() < seats)
               return View("ErrorMessage", new Error("no hotels with such number of seats"));
            if(beginDate > endDate)
                return View("ErrorMessage", new Error("Please enter the right order of dates"));
            if(beginDate < DateTime.Now.Date)
                return View("ErrorMessage", new Error("Please enter the newest date"));
            if (seats <= 0 || seats > 4)
                return View("ErrorMessage", new Error("you entered the wrong number of seats"));

            UserBooking.seats = seats;

            var booking = new UserBooking(beginDate, endDate);
            UserBooking.bd = beginDate;
            UserBooking.ed = endDate;

            var hotels = _bookingContext.Hotels.Where(h => h.City == city).ToList();
            if (hotels == null)
                return View("ErrorMessage", new Error("No hotel on this city"));

            List<Room> needRooms = new List<Room>();

            foreach (var room in _bookingContext.Rooms)
            {
                foreach (var hotel in hotels)
                {
                    if (hotel.Id == room.HId && room.Seats == seats)
                        needRooms.Add(room);
                }
            }

            List<Room> result = new List<Room>();
            //a.start <= b.end AND a.end >= b.start
            foreach (var room in needRooms)
            {
                bool isInperiod = false;
                foreach (var item in _bookingContext.Bookings)
                {
                    if ((room.Id == item.Idofroom && (beginDate <= item.Enddate && endDate >= item.Begindate)))
                    {
                        isInperiod = true;
                    }

                }
                if (!isInperiod)
                    result.Add(room);
            }
            if (result.Count == 0)
                return View("ErrorMessage", new Error("No hotel on that time"));
            List<Hotel> hotelResult = new List<Hotel>();

            JoinResult.result = result;

            foreach (var item in result)
            {
                hotelResult.Add(_bookingContext.Hotels.FirstOrDefault(h => h.Id == item.HId));
            }
            return View(hotelResult.Distinct().ToList());
        }
        // 
        // GET: /Booking/Details
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
                return View("ErrorMessage", new Error("Id is null[System error]"));

                var hotel = await _bookingContext.Hotels.FirstOrDefaultAsync(m => m.Id == id);

                if(hotel == null)
                    return View("ErrorMessage", new Error("No hotels on criteria"));
                return View(hotel);
        }

        public async Task<IActionResult> DetailsOfRoom(int? id)
        {
            if (id == null)
                return View("ErrorMessage", new Error("Id is null[System error]"));

            var room = await _bookingContext.Rooms.FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
                return View("ErrorMessage", new Error("No such rooms"));
            return View(room);
        }

        [HttpPost]
        public IActionResult Reserve(int? id)
        {
            if (User.Identity.Name != null)
            {
                booking book = _reserve.Reserve(id, _bookingContext, User.Identity.Name);
                if (!_reserve.Check(id, _bookingContext, book))
                    return View("ErrorMessage", new Error("Oops: The room has already been booked for the selected date :("));
                return RedirectToRoute(new { controller = "UsersAuth", action = "Profile"}); 
            }
            else
            {
                return RedirectToRoute(new { controller = "UsersAuth", action = "SignInIndex" });
            }
        }

        public IActionResult RoomsStatus(int hotelId)
        {
            return View(JoinResult.result.Where(h=> h.Seats == UserBooking.seats).ToList());
        }
    }
}