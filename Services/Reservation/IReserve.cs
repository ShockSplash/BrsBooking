using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services.Reservation
{
    public interface IReserve
    {
        public booking Reserve(int? id, bookingContext db, string login);

        public bool Check(int? id, bookingContext db, booking book);
    }
}
