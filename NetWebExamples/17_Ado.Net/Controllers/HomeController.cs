using _17_Ado.Net.DbService.Abstract;
using _17_Ado.Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace _17_Ado.Net.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDbService _dbService;
        public HomeController(IDbService dbService)
        {
            _dbService = dbService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddData()
        {
            string query = "Insert into Students (FirstName,LastName,Age)Values('Erkan','Türk',31)";
            _dbService.ExecuteNonQuery(query);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddDataSecure([FromForm] Student model)
        {
            string query = "Insert into Students (FirstName,LastName,Age)Values(@FirstName,@LastName,@Age)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@FirstName",model.FirstName),
                new SqlParameter("@LastName",model.LastName),
                new SqlParameter("@Age",model.Age)
            };
            _dbService.ExecuteNonQuery(query, parameters);
            return RedirectToAction("GetData");
        }
        [HttpGet]
        public IActionResult GetData()
        {
            string query = "Select * from Students";
            var data = _dbService.ExecuteReader(query);
            return View(data);
        }
        [HttpGet]
        public IActionResult GetDataCount()
        {
            string query = "Select Count(*) From Students";

            var count = _dbService.ExecuteScalar(query);
            return View(count);
        }
        [HttpPost]
        public IActionResult DeleteDataSecure([FromForm] int id)
        {
            string query = "Delete From Students where Id=@id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@id",id)
            };
            _dbService.ExecuteNonQuery(query, parameters);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateDataSecure([FromForm] Student model)
        {
            string query = "Update Students set FirstName=@p1,LastName=@p0,Age=@p2 where Id=@p3 ";
            SqlParameter[] parameters =
            {
                new SqlParameter("@p1",model.FirstName),
                new SqlParameter("@p0",model.LastName),
                new SqlParameter("@p2",model.Age),
                new SqlParameter("@p3",model.Id)
            };
            _dbService.ExecuteNonQuery(query, parameters);
            return RedirectToAction("Index");
        }

    }
}
