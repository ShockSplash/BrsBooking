using System;
using System.Collections.Generic;

#nullable disable

namespace Booking
{
    public partial class CompositeModel
    {
        public CompositeModel(DateTime bd, DateTime ed)
        {
            BeginDate = bd;
            EndDate = ed;
        }
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }
        public readonly DateTime BeginDate;
        public readonly DateTime EndDate;
        public int Seats;
    }
}
