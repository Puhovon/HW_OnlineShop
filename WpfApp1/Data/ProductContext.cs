using Dapper;
using Npgsql;
using System.Collections.Generic;
using WpfApp1.Models.MpgSql;

namespace WpfApp1.Data
{
    internal class ProductContext
    {
        public IEnumerable<Products> GetAllProducts(NpgsqlConnection db) =>
    db.Query<Products>("SELECT id, email, productcode, productname FROM Products");

        public void AddProductToTable(string conStr, Products product)
        {
            using (NpgsqlConnection db = new NpgsqlConnection(conStr))
            {
                string sqlQuerry = "INSERT INTO Products(email, productcode, productname) " +
                                   "VALUES (email, productCode,productName)";

                NpgsqlCommand cmd = new NpgsqlCommand(sqlQuerry, db);
                cmd.Parameters.AddWithValue("email", product.email);
                cmd.Parameters.AddWithValue("productCode", product.productcode);
                cmd.Parameters.AddWithValue("productName", product.productname);

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        public void UpdateProduct(string conStr, Products product)
        {
            using (NpgsqlConnection db = new NpgsqlConnection(conStr))
            {
                string sqlQuerry = "UPDATE Products " +
                                   "SET email = email, productcode = productCode, productname = productName " +
                                   "WHERE id = id";

                NpgsqlCommand cmd = new NpgsqlCommand(sqlQuerry, db);
                cmd.Parameters.AddWithValue("email", product.email);
                cmd.Parameters.AddWithValue("productCode", product.productcode);
                cmd.Parameters.AddWithValue("productName", product.productname);
                cmd.Parameters.AddWithValue("id", product.id);

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        public void RemoveProduct(string conStr, int id)
        {

            using (NpgsqlConnection db = new NpgsqlConnection(conStr))
            {
                string sqlQuerry = "DELETE FROM Products " +
                                   "WHERE id = id";

                NpgsqlCommand cmd = new NpgsqlCommand(sqlQuerry, db);
                cmd.Parameters.AddWithValue("id", id);

                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }


        public void RemoveAllDataProduct(NpgsqlConnection db) => db.Query<Products>($"DELETE FROM Products");

    }
}
