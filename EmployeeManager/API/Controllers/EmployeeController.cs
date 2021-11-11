using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;

        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @" SELECT * FROM Employee";
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
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Employee employee)
        {
            string query = $"INSERT INTO Employee (EmployeeName,Department,DateOfJoining,PhotoFileName) VALUES " +
                $"('{employee.EmployeeName}','{employee.Department}','{employee.DateOfJoining}','{employee.PhotoFileName}')";
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
            return new JsonResult("Employee added successfully!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = $"DELETE FROM Employee " +
                $"WHERE EmployeeId = {id}";
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
            return new JsonResult("Employee deleted successfully!");
        }
    }
}
