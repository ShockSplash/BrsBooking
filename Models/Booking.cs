using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Booking
{
    public partial class booking
    {
        public int Id { get; set; }
        public int? Idofroom { get; set; }
        public DateTime Begindate { get; set; }
        public DateTime Enddate { get; set; }
        public int? Userid { get; set; }

        public virtual Room IdofroomNavigation { get; set; }
        public virtual User User { get; set; }

    }
}
