 using System;
using System.Collections.Generic;
using Booking.Models;

#nullable disable

namespace Booking.Models
{
    public partial class UserBooking
    {
        public static List<Room> result = new List<Room>();

        public static DateTime bd;
        public static DateTime ed;

        public static int seats;
    }
}