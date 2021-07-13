using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;
using SimpleWebApi.Models;

namespace SimpleWebApi.Processors
{
    public class UserProductProcessor
    {
        private readonly MySqlConnection _db;
        
        public UserProductProcessor(MySqlConnection db)
        {
            _db = db;
        }

        public async Task<List<UserProductDto>> GetAllUserProductsAsync()
        {
            List<UserProductDto> result = new List<UserProductDto>();
            // using var connection = new MySqlConnection(yourConnectionString);
            await _db.OpenAsync();

            using var command = new MySqlCommand("SELECT up.UserProductId, up.ProductId, up.UserId, up.Quantity, u.Name as `UserName`, p.Name as `ProductName` FROM UserProducts up INNER JOIN Users u ON up.UserId=u.UserId INNER JOIN Products p ON p.ProductId=up.ProductId;", _db);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                UserProductDto entity = GetModel(reader);
                result.Add(entity);
            }
            return result;
        }
        
        public async Task<List<UserProductDto>> GetUserProductsAsync(int userId)
        {
            List<UserProductDto> result = new List<UserProductDto>();
            // using var connection = new MySqlConnection(yourConnectionString);
            await _db.OpenAsync();

            using var command = new MySqlCommand($"SELECT up.UserProductId, up.ProductId, up.UserId, up.Quantity, u.Name as `UserName`, p.Name as `ProductName` FROM UserProducts up INNER JOIN Users u ON up.UserId=u.UserId INNER JOIN Products p ON p.ProductId=up.ProductId WHERE up.UserId={userId};", _db);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                UserProductDto entity = GetModel(reader);
                result.Add(entity);
            }
            return result;
        }

        private UserProductDto GetModel(MySqlDataReader reader)
        {
            UserProductDto entity= new UserProductDto();
            entity.UserId = (int) reader["UserId"];
            // entity.Name= (string)reader["Name"];
            entity.ProductId = (int) reader["ProductId"];
            entity.UserProductId = (int) reader["UserProductId"];
            entity.Quantity = (int) reader["Quantity"];
            entity.UserName = (string) reader["UserName"];
            entity.ProductName = (string) reader["ProductName"];

            return entity;
        }
    }
}