using _17_Ado.Net.Models;
using Microsoft.Data.SqlClient;

namespace _17_Ado.Net.DbService.Abstract
{
    public interface IDbService
    {
        void ExecuteNonQuery(string query);//Sql tarafında (Insert(Create),Update,Delete)
        void ExecuteNonQuery(string query, SqlParameter[] parameters);
        List<Student> ExecuteReader(string query);//Select sorgusunu çalıştırır.
        object ExecuteScalar(string query);//Tek bir değer döndüren sql sorguları Örneğin(Count,Sum,Avg,Max,Min) için kullanılır.


    }
}
