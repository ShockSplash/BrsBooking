using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services.Reservation
{
    public interface IReserve
    {
        /// <summary>Метод для бронирования номера
        /// <para name = "id">Уникальный идентификатор комнаты</para>
        /// <para name = "db">Контекст данных</para>
        ///<para name = "login">Логин пользователя</para>
        /// <para name = "beginDate">Дата начала бронирования</para> 
        /// <para name = "endDate">Дата конца бронирования</para> 
        /// </summary>
        public booking Reserve(int? id, bookingContext db, string login, 
        DateTime beginDate, DateTime endDate);


        /// <summary>Метод для проверки на корректность брони
        /// <para name = "id">Уникальный идентификатор комнаты</para>
        /// <para name = "db">Контекст данных</para>
        ///<para name = "book">Конкретная бронь</para>
        /// </summary>
        public bool Check(int? id, bookingContext db, booking book);
    }
}
