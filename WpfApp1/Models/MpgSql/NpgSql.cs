using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Data;
using WpfApp1.Services;

namespace WpfApp1.Models.MpgSql
{
    internal class NpgSql : IDataRepository<Products>
    {
        ProductContext _context;

        public NpgSql(ProductContext context) => _context = context;

        public IEnumerable<Products> GetAllData
        {
            get
            {
                using (NpgsqlConnection sql = new NpgsqlConnection(GetConnectionString()))
                    return _context.GetAllProducts(sql);
            }
        }

        public string GetConnectionString()
        {
            NpgsqlConnectionStringBuilder conStr = new NpgsqlConnectionStringBuilder()
            {
                Database = "products",
                Host = "localhost",
                Port = 5432,
                Password = "123",
                Username = "postgres",

            };

            return conStr.ConnectionString;
        }

        public string ConnectionsTest(string conStr)
        {
            try
            {
                NpgsqlConnection conTest = new NpgsqlConnection(conStr);
                conTest.Open();
                if (conTest.State.HasFlag(ConnectionState.Open))
                {
                    conTest.CloseAsync();
                    return "Успешное подключение к базе деннах Access";
                }
            }
            catch
            {
                return "Ошибка подключения к базе деннах Access";
            }
            return "Ошибка подключения к базе деннах Access";
        }

        public Products Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Products item)
        {
            _context.AddProductToTable(GetConnectionString(), item);
        }

        public void Update(Products item)
        {
            _context.UpdateProduct(GetConnectionString(), item);
        }

        public void Remove(int id)
        {
            using (NpgsqlConnection sql = new NpgsqlConnection(GetConnectionString()))
            {
                _context.RemoveProduct(GetConnectionString(), id);
            }
        }

        public void RemoveAll()
        {
            using (NpgsqlConnection sql = new NpgsqlConnection(GetConnectionString()))
            {
                _context.RemoveAllDataProduct(sql);
            }
        }
    
    }
}
