using Booking.Models.UserWithBookingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services.ProfileService
{
    public interface IProfile
    {
        /// <summary>Метод для получения всех букингов в профиле
        /// <para name = "userName">Логин пользователя</para>
        /// /// <para name = "_bookingContext">Контекст данных</para>
        /// </summary>
        public UserBooking getBooking(bookingContext _bookingContext, string userName);
    }
}
