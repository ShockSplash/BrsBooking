using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services.Search_service
{
    public interface ISearch
    {

        List<Hotel> SearchFilter(bookingContext db, string city, DateTime beginDate, DateTime endDate, int seats);
    }
}
