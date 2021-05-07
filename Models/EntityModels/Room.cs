using System;
using System.Collections.Generic;

#nullable disable

namespace Booking
{
    public partial class Room
    {
        public int Id { get; set; }
        public int? HId { get; set; }
        public int Seats { get; set; }
        public double Price { get; set; }

        public virtual Hotel HIdNavigation { get; set; }
    }
}
