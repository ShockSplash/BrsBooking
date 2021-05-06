using System;
using System.Collections.Generic;
using Booking.Models;

#nullable disable

namespace Booking
{
    public partial class JoinResult
    {
        public JoinResult(int id, int idofroom, string name, string city, string description, DateTime begindate, DateTime enddate)
        {
            Id = id; 
            Idofroom = idofroom;
            Name = name; 
            City = city;
            Description = description;
            Begindate = begindate;
            Enddate = enddate;
        }

        public int Id { get; set; }
        public int Idofroom { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public DateTime Begindate { get; set; }
        public DateTime Enddate { get; set; }
    }
}