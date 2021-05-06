using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services.SignUp
{
    public interface ISign
    {
        public void SignU(string name, string login, string password, bookingContext db);
    }
}
