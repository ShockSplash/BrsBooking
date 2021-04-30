using System;
using Npgsql.EntityFrameworkCore.PostgreSQL.Design;

namespace Booking.Models
{
    public class Hotel
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Descritption { get; set; }
        public bool IsFreeRooms { get; set; }
    }
    public class Room
    {
        public int Id { get; set; }
        public int H_id { get ; set; }
        public int Seats { get ; set; }
        public float Price { get ; set; }
        public bool IsFree { get; set; }
    }
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}