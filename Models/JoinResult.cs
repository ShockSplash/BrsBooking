using System;
using System.Collections.Generic;

#nullable disable

namespace Booking
{
    public partial class JoinResult
    {
        public JoinResult(int id, string name, string city, string desc, DateTime b, DateTime e)
        {
            Id = id; 
            Name = name; 
            City = city;
            Description = desc;
            BegDate = b;
            EndDate = e;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public DateTime BegDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}