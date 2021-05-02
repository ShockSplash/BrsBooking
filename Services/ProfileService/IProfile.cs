using Booking.Models.UserWithBookingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services.ProfileService
{
    public interface IProfile
    {
        public UserBooking getBooking(bookingContext _bookingContext, string userName);
    }
}
