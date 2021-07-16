using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace Repository
{
    public class UserProductRepository : IUserProductRepository
    {
        private readonly MySqlConnection _db;
        private readonly ILogger<UserProductRepository> _logger;
        
        public UserProductRepository(MySqlConnection db, ILogger<UserProductRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IEnumerable<UserProductDto>> FindAll()
        {
            List<UserProductDto> result = new List<UserProductDto>();
            // using var connection = new MySqlConnection(yourConnectionString);
            await _db.OpenAsync();


            using var command = new MySqlCommand("sp_RetrieveAllUserProducts", _db);
            command.CommandType = CommandType.StoredProcedure;
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                UserProductDto entity = GetModel(reader);
                result.Add(entity);
            }
            await _db.CloseAsync();
            return result;
        }

        public Task<IEnumerable<UserProductDto>> FindByConditionAsync(Expression<Func<UserProductDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<UserProductDto> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(UserProductDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(UserProductDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(UserProductDto entity)
        {
            throw new NotImplementedException();
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
            await _db.CloseAsync();

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