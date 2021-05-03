using Booking.Models.UserWithBookingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services.ProfileService
{
    public class AddProfile : IProfile
    {
        public UserBooking getBooking(bookingContext _bookingContext, string userName)
        {
            Console.WriteLine(userName);
            if (userName != null && _bookingContext.Bookings != null )
            {
                User bookUs = _bookingContext.Users.FirstOrDefault(u => u.Login == userName);
                var book = _bookingContext.Bookings.Where(u => u.Userid == bookUs.Id).ToList();
                if (book.Count == 0)
                    return new UserBooking(bookUs,null,null,null);
                var rooms = _bookingContext.Rooms.Where(u => u.Id == book[0].Idofroom).ToList();
                for (int i = 1; i < book.Count; i++)
                {
                    rooms.Add(_bookingContext.Rooms.FirstOrDefault(u => u.Id == book[i].Idofroom));
                }
                if (rooms.Count == 0)
                    return new UserBooking(bookUs, null, null, null);
                var hotels = _bookingContext.Hotels.Where(u => u.Id == rooms[0].HId).ToList();
                for (int i = 1; i < rooms.Count; i++)
                {
                     hotels.Add(_bookingContext.Hotels.FirstOrDefault(u => u.Id == rooms[i].HId));
                }
                if (hotels == null)
                    return new UserBooking(bookUs, rooms, null, null);
                var bookings = _bookingContext.Bookings.Where(u => u.Userid == bookUs.Id).ToList();


                Console.WriteLine(_bookingContext.Users.FirstOrDefault(u => u.Login == userName).Name);
                UserBooking ub = new UserBooking(bookUs, rooms, hotels, bookings);

                return ub;
            }
                return null;
        }
    }
}
