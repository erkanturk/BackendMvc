using _17_Ado.Net.DbService.Abstract;
using _17_Ado.Net.Models;
using Microsoft.Data.SqlClient;

namespace _17_Ado.Net.DbService.Concrete
{
    public class DbService : IDbService
    {
        private readonly string _connection;

        public DbService(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine("Bağlantı yapıldı");
            //Appsettings.json yapısındaki Connection stringin içerisinde bulunan ismini bizim verdiğimiz defaultconnection yapısını oku.
        }
        public void ExecuteNonQuery(string query)
        {
            using (var connection = new SqlConnection(_connection))//Sql bağlantı dizisi
            {
                using (var command = new SqlCommand(query, connection))//Sql sorgusu ve bu sorgunun çalışacağı veri tabanı
                {
                    connection.Open();
                    command.ExecuteNonQuery();//Komut çalıştırılır etkilenen satır sayısı döndürülür.
                }
            }
        }

        public void ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connection))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    command.ExecuteNonQuery ();
                }
            }
        }

        public List<Student> ExecuteReader(string query)
        {
            var result = new List<Student>();
            using (var connection = new SqlConnection(_connection))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var model = new Student()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Age = Convert.ToInt32(reader["Age"])
                            };
                            result.Add(model);
                        }
                    }
                }
            }
            return result;
        }

        public object ExecuteScalar(string query)
        {
            using (var connection = new SqlConnection(_connection))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }
    }
}
