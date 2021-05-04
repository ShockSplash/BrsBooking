using System;
using System.Collections.Generic;

#nullable disable

namespace Booking
{
    public partial class JoinResult
    {
        public JoinResult(int id, string name, string city, string desc)
        {
            Id = id; Name = name; City = city; Description = desc;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
    }
}