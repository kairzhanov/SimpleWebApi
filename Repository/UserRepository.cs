using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
    
        private readonly MySqlConnection _db;
        private readonly ILogger<UserRepository> _logger;
        
        public UserRepository(MySqlConnection db, ILogger<UserRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        
        
        public async Task<IEnumerable<User>> FindAll()
        {
            List<User> result = new List<User>();
            
            await _db.OpenAsync();

            using var command = new MySqlCommand("SELECT * FROM Users;", _db);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                User entity = GetModel(reader);
                result.Add(entity);
            }
            await _db.CloseAsync();
            return result;
        }

        public Task<IEnumerable<User>> FindByConditionAsync(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindById(int id)
        {
            await _db.OpenAsync();

            using var command = new MySqlCommand($"SELECT * FROM Users WHERE UserId={id};", _db);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                User entity = GetModel(reader);
                return entity;
            }
            await _db.CloseAsync();

            return null;
        }

        public Task<int> CreateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }
        
        private User GetModel(MySqlDataReader reader)
        {
            try
            {
                User entity= new User();
                entity.UserId = (int)reader["UserId"];
                entity.Name= (string)reader["Name"];
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}