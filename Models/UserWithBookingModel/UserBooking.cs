using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Models.UserWithBookingModel
{
    public class UserBooking
    {
        public UserBooking(User user, List<Room> rooms, List<Hotel> hotels, List<booking> bookings)
        {
            userBooking = user;
            this.rooms = rooms;
            this.hotels = hotels;
            Bookings = bookings;
        }

        public User userBooking { get; set; }

        public List<booking> Bookings;

        public List<Room> rooms { get; set; }

        public List<Hotel> hotels { get; set; }
    }
}
