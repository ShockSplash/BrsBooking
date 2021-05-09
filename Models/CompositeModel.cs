using System;
using System.Collections.Generic;


#nullable disable

namespace Booking
{
    public partial class CompositeModel
    {
        public CompositeModel(string bd, string ed)
        {
            BeginDate = bd;
            EndDate = ed;
        }
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public int Seats;
    }
}
