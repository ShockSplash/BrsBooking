using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Services.Search_service
{
    public interface ISearch
    {
        /// <summary>Метод для фильтра данных по заданым параметрам
        /// <para name = "db">Контекст данных</para>
        /// <para name = "city">Город, выбранный пользователем</para>
        /// <para name = "beginDate">Дата начала бронирования</para> 
        /// <para name = "endDate">Дата конца бронирования</para> 
        ///<para name = "seats">Количество метс</para>
        /// </summary>
        List<Hotel> SearchFilter(bookingContext db, string city, DateTime beginDate, DateTime endDate, int seats);
    }
}
