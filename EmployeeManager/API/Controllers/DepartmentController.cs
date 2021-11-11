using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration; 

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @" SELECT * FROM Department";
            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppConnection");
            SqlDataReader sqlDataReader;
            using (var connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using(var command = new SqlCommand(query, connection))
                {
                    sqlDataReader = command.ExecuteReader();
                    table.Load(sqlDataReader);
                    sqlDataReader.Close();
                    connection.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Department department)
        {
            string query = $"INSERT INTO Department VALUES ('{department.DepartmentName}')";
            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppConnection");
            SqlDataReader sqlDataReader;
            using (var connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    sqlDataReader = command.ExecuteReader();
                    table.Load(sqlDataReader);
                    sqlDataReader.Close();
                    connection.Close();
                }
            }
            return new JsonResult("Department added successfully!");
        }


        [HttpPut]
        public JsonResult Put(Department department)
        {
            string query = $"UPDATE Department " +
                $"SET DepartmentName= '{department.DepartmentName}' " +
                $"WHERE DepartmentId = {department.DepartmentId}";
            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppConnection");
            SqlDataReader sqlDataReader;
            using (var connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    sqlDataReader = command.ExecuteReader();
                    table.Load(sqlDataReader);
                    sqlDataReader.Close();
                    connection.Close();
                }
            }
            return new JsonResult("Department udpated successfully!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = $"DELETE FROM Department " +
                $"WHERE DepartmentId = {id}";
            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppConnection");
            SqlDataReader sqlDataReader;
            using (var connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    sqlDataReader = command.ExecuteReader();
                    table.Load(sqlDataReader);
                    sqlDataReader.Close();
                    connection.Close();
                }
            }
            return new JsonResult("Department deleted successfully!");
        }

    }
}
