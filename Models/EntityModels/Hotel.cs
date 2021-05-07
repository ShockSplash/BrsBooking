using System;
using System.Collections.Generic;

#nullable disable

namespace Booking
{
    public partial class Hotel
    {
        public Hotel()
        {
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public bool Isfreerooms { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
