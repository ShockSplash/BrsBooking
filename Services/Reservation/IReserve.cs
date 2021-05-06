using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services.Reservation
{
    public interface IReserve
    {
        public void Reserve(int? id, bookingContext db, string login);

        
    }
}
