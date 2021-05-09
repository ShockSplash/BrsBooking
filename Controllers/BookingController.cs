using Booking.Models;
using Booking.Services.Reservation;
using Booking.Services.Search_service;
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
        private readonly ISearch _search;

        public BookingController(bookingContext context, IReserve reserve, ISearch search)
        { 
            _bookingContext = context;
            _reserve = reserve;
            _search = search;
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
            if (seats <= 0 || seats > 5)
                return View("ErrorMessage", new Error("you entered the wrong number of seats"));


            var hotels = _search.SearchFilter(_bookingContext, city, beginDate, endDate, seats);
            if (hotels == null)
                return View("ErrorMessage", new Error("No rooms are available"));

            var result = new List<CompositeModel>();
            foreach(var item in hotels)
            {
                result.Add(new CompositeModel(beginDate, endDate){
                    Hotel = item,
                    Seats = seats
                    });
            }
            //Console.WriteLine("{0} {1}", beginDate, endDate);
            
            return View(result);
        }
       
        // GET: /Booking/Details
        public async Task<IActionResult> Details(int? id, DateTime bd, DateTime ed, int seats)
        {
            Console.WriteLine("{0} {1} {2} {3}", id, bd, ed, seats);
            if(id == null)
                return View("ErrorMessage", new Error("Id is null[System error]"));

                var hotel = await _bookingContext.Hotels.FirstOrDefaultAsync(m => m.Id == id);

                if(hotel == null)
                    return View("ErrorMessage", new Error("No hotels on criteria"));
                
                return View(new CompositeModel(bd, ed){
                    Hotel = hotel,
                    Seats = seats
                });
        }

        public async Task<IActionResult> DetailsOfRoom(int? id, DateTime beginDate, DateTime endDate)
        {
            if (id == null)
                return View("ErrorMessage", new Error("Id is null[System error]"));

            var room = await _bookingContext.Rooms.FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
                return View("ErrorMessage", new Error("No such rooms"));

            return View(new CompositeModel(beginDate, endDate){
                Room = room
            });
        }

        [HttpPost]
        public IActionResult Reserve(int? id, DateTime beginDate, DateTime endDate)
        {
            if (User.Identity.Name != null)
            {
                booking book = _reserve.Reserve(id, _bookingContext, User.Identity.Name, beginDate, endDate);
                if (!_reserve.Check(id, _bookingContext, book))
                    return View("ErrorMessage", new Error("Oops: The room has already been booked for the selected date :("));
                return RedirectToRoute(new { controller = "UsersAuth", action = "Profile"}); 
            }
            else
            {
                return RedirectToRoute(new { controller = "UsersAuth", action = "SignInIndex" });
            }
        }

        public IActionResult RoomsStatus(int hotelId, DateTime beginDate, DateTime endDate, int seats)
        {
            var rooms = _bookingContext.Rooms.Where(r => r.Seats == seats && r.HId == hotelId).ToList();
            var cmList = new List<CompositeModel>();
            foreach(var item in rooms)
            {
                cmList.Add(new CompositeModel(beginDate, endDate){
                    Room = item,
                    Seats = item.Seats
                });
            }
            return View(cmList);
        }
    }
}