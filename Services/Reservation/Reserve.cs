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
                if (item.Idofroom == id && (item.Begindate <=
                     book.Enddate && item.Enddate >= book.Begindate))
                    return false;
            }

            db.Bookings.Add(book);
            db.SaveChanges();

            return true;
        }

        booking IReserve.Reserve(int? id, bookingContext db, string login, 
        DateTime beginDate, DateTime endDate)
        {
            User user = db.Users.FirstOrDefault(u => u.Login == login);
            Room room = db.Rooms.FirstOrDefault(r => r.Id == id);

            booking book = new()
            {
                Idofroom = id,
                Begindate = beginDate,
                Enddate = endDate,
                IdofroomNavigation = room,
                User = user,
                Idofhotel = room.HId,
                Id = db.Bookings.Max(a => a.Id) + 1
            };

            return book;
        }
    }
}
