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

        public BookingController(bookingContext context)
        {
            bookingContext = context;
        }
        // 
        // GET: /Booking/
        public IActionResult Index(string city, DateTime beginDate, DateTime endDate, int seats)
        {
            var hotels = bookingContext.Hotels.AsEnumerable().Where
            (
                m => m.City == city && m.Id == bookingContext.Rooms.FirstOrDefault
                (
                    h => h.Seats == seats &&
                         (
                             null == bookingContext.Bookings.AsEnumerable().Where
                             (
                                 r => ((r.Begindate > beginDate && r.Enddate < endDate) || (r.Enddate < endDate && r.Enddate > beginDate) || (beginDate<r.Begindate && r.Enddate>endDate)) && r.Idofroom == h.Id
                             ).ToList()
                         )
                ).HId
            ).ToList();
            if (hotels == null)
                return NotFound();
            return View(hotels);
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
         public IActionResult RoomsStatus(int hotelId)
        {

                return View(bookingContext.Rooms.Where(r => r.HId == hotelId).ToList());
        }
    }
}