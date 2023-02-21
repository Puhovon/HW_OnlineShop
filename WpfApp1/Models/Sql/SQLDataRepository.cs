using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Data;
using WpfApp1.Services;

namespace WpfApp1.Models.Sql
{
    internal class SQLDataRepository : IDataRepository<Person>
    {
        PersonContext _context;

        public SQLDataRepository(PersonContext context) => _context = context;

        public IEnumerable<Person> GetAllData
        {
            get
            {
                using (SqlConnection sql = new SqlConnection(GetConnectionString()))
                    return _context.GetAllPerson(sql);
            }
        }

        public string GetConnectionString()
        {
            SqlConnectionStringBuilder conStr = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "Person",
                IntegratedSecurity = true,
            };

            return conStr.ConnectionString;
        }

        public string ConnectionsTest(string conStr)
        {
            try
            {
                SqlConnection conTest = new SqlConnection(conStr);
                conTest.Open();
                if (conTest.State.HasFlag(ConnectionState.Open))
                {
                    conTest.Close();
                    return "Успешное подключение к базе деннах SQL";
                }
            }
            catch
            {
                return "Ошибка подключения к базе деннах SQL";
            }
            return "Ошибка подключения к базе деннах SQL";
        }

        public Person Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Person item)
        {
            using (SqlConnection sql = new SqlConnection(GetConnectionString()))
            {
                _context.AddPersonToTable(sql, item);
            }
        }

        public void Update(Person item)
        {
            using (SqlConnection sql = new SqlConnection(GetConnectionString()))
            {
                _context.UpdatePerson(sql, item);
            }
        }

        public void Remove(int id)
        {
            using (SqlConnection sql = new SqlConnection(GetConnectionString()))
            {
                _context.RemovePerson(sql, id);
            }
        }

        public void RemoveAll()
        {
            using (SqlConnection sql = new SqlConnection(GetConnectionString()))
            {
                _context.RemoveAllDataPerson(sql);
            }
        }
    }
}
