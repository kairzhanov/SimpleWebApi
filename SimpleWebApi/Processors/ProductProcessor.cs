using System.Collections.Generic;
using System.Data;
using SimpleWebApi.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace SimpleWebApi.Processors
{
    public class ProductProcessor
    {
        private readonly MySqlConnection _db;
        
        public ProductProcessor(MySqlConnection db)
        {
            _db = db;
        }

        
        public async Task<List<User>> GetAllUsers()
        {
            List<User> result = new List<User>();
            // using var connection = new MySqlConnection(yourConnectionString);
            await _db.OpenAsync();

            using var command = new MySqlCommand("SELECT * FROM Users;", _db);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                User entity= new User();
                entity.UserId = (int)reader["UserId"];
                entity.Name= (string)reader["Name"];
                result.Add(entity);
            }
            return result;
        }
    }
}