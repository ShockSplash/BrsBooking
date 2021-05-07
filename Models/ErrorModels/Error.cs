 using System;
using System.Collections.Generic;
using Booking.Models;

#nullable disable

namespace Booking.Models
{
    public partial class Error
    {
        public Error(string message)
        {
            Message = message;
        }
        public string Message;
    }
}