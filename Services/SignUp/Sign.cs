using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services.SignUp
{
    public class Sign : ISign
    {
        public void SignU(string name, string login, string password, bookingContext db)
        {
            var id = db.Users.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            var user = new User { Id = id, Name = name, Login = login, Password = password };
            db.Users.Add(user);
            db.SaveChanges();
        }
    }
}
