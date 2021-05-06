using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services.Reservation
{
    public class Reserve : IReserve
    {
        public bool Check(int? id, bookingContext db, booking book)
        {
            foreach (var item in db.Bookings)
            {
                if (item.Idofroom == id && (item.Begindate == book.Begindate)
                    && (item.Enddate == book.Enddate))
                    return false;
            }

            db.Bookings.Add(book);
            db.SaveChanges();

            return true;
        }

        booking IReserve.Reserve(int? id, bookingContext db, string login)
        {
            User user = db.Users.FirstOrDefault(u => u.Login == login);
            Room room = db.Rooms.FirstOrDefault(r => r.Id == id);
            booking book = new booking();
            
            book.Idofroom = id;
            book.Begindate = UserBooking.bd;
            book.Enddate = UserBooking.ed;
            book.Userid = user.Id;
            book.IdofroomNavigation = room;
            book.User = user;
            book.Idofhotel = room.HId;
            book.Id = db.Bookings.Max(a => a.Id) + 1;

            return book;
        }
    }
}
