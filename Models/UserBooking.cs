 using System;
using System.Collections.Generic;
using Booking.Models;

#nullable disable

namespace Booking.Models
{
    public partial class UserBooking
    {
        public UserBooking(DateTime begindate, DateTime enddate)
        {
            Begindate = begindate;
            Enddate = enddate;
            Ids = new HashSet<KeyValuePair<int, int>>();
        }
        public DateTime Begindate { get; set; }
        public DateTime Enddate { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public HashSet<KeyValuePair<int , int>> Ids;
        public bool IsFree = true;
        public static DateTime bd;
        public static DateTime ed;
    }
}