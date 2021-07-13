using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;
using SimpleWebApi.Models;

namespace SimpleWebApi.Processors
{
    public class UserProcessor
    {
        private readonly MySqlConnection _db;
        
        public UserProcessor(MySqlConnection db)
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
                User entity = GetModel(reader);
                result.Add(entity);
            }
            return result;
        }

        public async Task<User> GetUser(int userId)
        {
            await _db.OpenAsync();

            using var command = new MySqlCommand($"SELECT * FROM Users WHERE UserId={userId};", _db);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                User entity = GetModel(reader);
                return entity;
            }

            return null;
        }

        private User GetModel(MySqlDataReader reader)
        {
            User entity= new User();
            entity.UserId = (int)reader["UserId"];
            entity.Name= (string)reader["Name"];
            return entity;
        }
        
     }
}