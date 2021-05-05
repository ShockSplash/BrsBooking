using System;
using System.Collections.Generic;
using Booking.Models;

#nullable disable

namespace Booking
{
    public partial class JoinResult
    {
        public JoinResult(int id, string name, string city, string description, DateTime begindate, DateTime enddate, bool isfree)
        {
            Id = id; 
            Name = name; 
            City = city;
            Description = description;
            Begindate = begindate;
            Enddate = enddate;
            isFree = isfree;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public DateTime Begindate { get; set; }
        public DateTime Enddate { get; set; }
        public bool isFree { get; set; }
    }
}