using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models.MpgSql;

namespace WpfApp1.Models.Sql
{
    internal class Person
    {
        /// <summary>
        /// Номер записи
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string secondName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string firstName { get; set; }

        /// <summary>
        /// Оnчество
        /// </summary>
        public string lastName { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string phoneNumber { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Коллекция продуктов к записи
        /// </summary>
        public IEnumerable<Products> Products { get; set; }

    }
}
