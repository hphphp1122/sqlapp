using Microsoft.AspNetCore.SignalR;
using sqlapp.Models;
using System.Data.SqlClient;
namespace sqlapp.Services
{
    public class ProductService
    {



        private static string db_source = "hritwikdb.database.windows.net";
        private static string db_user = "sqladmin";
        private static string db_pass = "Q1w2e3r4t5!@#$%";
        private static string db_database = "demoDB";

        private SqlConnection GetConnection() {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_pass;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        
        }


        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> _product_list = new List<Product>();
            string statement = "Select ProductID, ProductName, Quantity from Products";
            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);
            using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = sqlDataReader.GetInt32(0),
                        ProductName = sqlDataReader.GetString(1),
                        Quantity = sqlDataReader.GetInt32(2)


                    };
                    _product_list.Add(product);

                }
            }
            conn.Close();
            return _product_list;

        }

    }
}
