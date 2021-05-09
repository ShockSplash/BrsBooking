using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services.Search_service
{
    public class Search : ISearch
    {
        public List<Hotel> SearchFilter(bookingContext db, string city, DateTime beginDate, DateTime endDate, int seats)
        {
            var hotels = db.Hotels.Where(h => h.City == city).ToList();
            if (hotels == null)
                return null;

            List<Room> needRooms = new List<Room>();

            foreach (var room in db.Rooms)
            {
                foreach (var hotel in hotels)
                {
                    if (hotel.Id == room.HId && room.Seats == seats)
                        needRooms.Add(room);
                }
            }

            List<Room> result = new List<Room>();
            //a.start <= b.end AND a.end >= b.start
            foreach (var room in needRooms)
            {
                bool isInperiod = false;
                foreach (var item in db.Bookings)
                {
                    if ((room.Id == item.Idofroom && (beginDate <= item.Enddate && endDate >= item.Begindate)))
                    {
                        isInperiod = true;
                    }

                }
                if (!isInperiod)
                    result.Add(room);
            }
            if (result.Count == 0)
                return null;
            List<Hotel> hotelResult = new List<Hotel>();

            foreach (var item in result)
            {
                hotelResult.Add(db.Hotels.FirstOrDefault(h => h.Id == item.HId));
            }
            return hotelResult.Distinct().ToList();
        }
    }
}
