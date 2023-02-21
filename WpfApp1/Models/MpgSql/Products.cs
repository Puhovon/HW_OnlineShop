using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.MpgSql
{
    internal class Products
    {
        /// <summary>
        /// Номер записи
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Код продукта
        /// </summary>
        public string productcode { get; set; }

        /// <summary>
        /// Название продукта
        /// </summary>
        public string productname { get; set; }

    }
}
