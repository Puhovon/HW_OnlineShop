using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using WpfApp1.Models.MpgSql;
using WpfApp1.Models.Sql;
using WpfApp1.Services;

namespace WpfApp1.Data
{
    internal class PersonContext
    {
        IDataRepository<Products> _productRepository;

        public PersonContext(IDataRepository<Products> productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Person> GetAllPerson(SqlConnection db)
        {
            var person = db.Query<Person>("SELECT id, secondName, firstName, lastName, phoneNumber, email FROM Persones");

            using (var oleCon = new NpgsqlConnection(_productRepository.GetConnectionString()))
            {
                oleCon.Open();
                var product = oleCon.Query<Products>("SELECT id, email, productName, productcode FROM Products");
                foreach (Person p in person)
                {
                    p.Products = product.Where(x => x.email.Trim() == p.email);
                }
            }

            return person;
        }

        public void AddPersonToTable(SqlConnection db, Person person)
        {
            db.Query<Person>($"INSERT INTO Persones (secondName, firstName, lastName, phoneNumber, email) " +
                             $"VALUES (N'{person.secondName}', N'{person.firstName}', N'{person.lastName}','{person.phoneNumber}', N'{person.email}')");
        }


        public void UpdatePerson(SqlConnection db, Person person) =>
            db.Query<Person>($"UPDATE Persones " +
                             $"SET secondName = N'{person.secondName}'," +
                             $"firstName = N'{person.firstName}'," +
                             $"lastName = N'{person.lastName}'," +
                             $"phoneNumber = '{person.phoneNumber}'," +
                             $"email = '{person.email}'" +
                             $"WHERE id = '{person.id}'");

        public void RemovePerson(SqlConnection db, int id) =>
                        db.Query<Person>($"DELETE FROM Persones " +
                                         $"WHERE id = '{id}'");

        public void RemoveAllDataPerson(SqlConnection db) => db.Query<Person>($"DELETE FROM Persones");

    }
}
